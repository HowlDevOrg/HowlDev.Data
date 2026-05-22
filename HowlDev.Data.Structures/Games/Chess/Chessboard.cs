using System.Runtime.CompilerServices;

namespace HowlDev.Data.Structures.Games.Chess;

// CHESS KEY: 
// Positioning is white along the top.
// 0 = Empty Cell
// 8 = Out of Bounds
// 1 = White King
// 2 = White Queen
// 3 = White Bishop
// 4 = White Knight
// 5 = White Rook
// 6 = White Pawn
// 9 = Black King
// a/10 = Black Queen
// b/11 = Black Bishop
// c/12 = Black Knight
// d/13 = Black Rook
// e/14 = Black Pawn

public class Chessboard : IEquatable<Chessboard> {
    private byte[] board;
    private HashSet<(ChessPiece Piece, int index)> whitePieces = [];
    private HashSet<(ChessPiece Piece, int index)> blackPieces = [];

    public Chessboard() {
        board = DefaultBoard();
        RecalculateChessLists();
    }

    private Chessboard(byte[] newBoard) {
        board = newBoard;
        RecalculateChessLists();
    }

    public (ChessPiece Piece, bool White)? CheckSquare(int index) {
        return GetPiece(GetByteAtIndex(index));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public (ChessPiece Piece, bool White)? CheckSquare(int row, int col) {
        return CheckSquare(ChessHelpers.RowColToIndex(row, col));
    }

    /// <summary>
    /// Gets all pieces with an associated color and their index. 
    /// </summary>
    public IEnumerable<(ChessPiece Piece, int index)> GetChessPieces(bool white) {
        return white ? whitePieces : blackPieces;
    }

    /// <summary>
    /// Lazily returns the moves for a given index. If the piece is an empty square, 
    /// simply returns an empty array. 
    /// </summary>
    public int[] GetValidMoves(int index, bool white) {
        (ChessPiece Piece, bool White)? piece = CheckSquare(index);
        if (piece is null) return [];

        (int row, int col) = ChessHelpers.IndexToRowCol(index);
        switch (piece.Value.Piece) {
            case ChessPiece.King:
                return GetKingSpaces(row, col, white);
            case ChessPiece.Queen:
                break;
            case ChessPiece.Rook:
                break;
            case ChessPiece.Bishop:
                break;
            case ChessPiece.Knight:
                return GetKnightSpaces(row, col, white);
            case ChessPiece.Pawn:
                break;
        }

        return [];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int[] GetValidMoves(int row, int col, bool white) {
        return GetValidMoves(ChessHelpers.RowColToIndex(row, col), white);
    }

    public static Chessboard ReadFEN(string fen) {
        byte[] newBoard = new byte[32];
        byte currentPiece = 0x00;
        bool offset = false;
        int index = 0;
        foreach ((ChessPiece Piece, bool White)? item in ChessHelpers.ParseFENNotation(fen)) {
            byte newPiece = item.HasValue ? GetByte(item.Value.Piece, item.Value.White) : (byte)0x00;
            if (offset) {
                currentPiece = (byte)(currentPiece << 4);
                currentPiece |= newPiece;
                newBoard[index] = currentPiece;
                index++;
                offset = false;
            } else {
                currentPiece = newPiece;
                offset = true;
            }
        }

        return new Chessboard(newBoard);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void RecalculateChessLists() {
        whitePieces = [.. CalculateChessLists(true)];
        blackPieces = [.. CalculateChessLists(false)];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsValidRowCol(int newRow, int newCol) {
        return newRow > 0 && newRow < 9 && newCol > 0 && newCol < 9;
    }

    private IEnumerable<(ChessPiece Piece, int index)> CalculateChessLists(bool white) {
        for (int i = 0; i < 64; i++) {
            (ChessPiece Piece, bool White)? option = CheckSquare(i);
            if (option.HasValue && option.Value.White == white) {
                yield return (option.Value.Piece, i);
            }
        }
    }

    private int[] GetKingSpaces(int row, int col, bool white) {
        (int, int)[] checks = [
            (1, -1),  (1, 0),  (1, 1),
            (0, -1),           (0, 1),
            (-1, -1), (-1, 0), (-1, 1),
            ];
        return ValidateChecks(checks, row, col, white);
    }

    private int[] GetKnightSpaces(int row, int col, bool white) {
        (int, int)[] checks = [
            (2, 1),  (1, 2),
            (-2, 1),  (-1, 2),
            (2, -1),  (1, -2),
            (-2, -1),  (-1, -2),
            ];
        return ValidateChecks(checks, row, col, white);

    }

    private int[] ValidateChecks((int, int)[] checks, int row, int col, bool white) {
        List<int> outputs = [];
        foreach ((int, int) check in checks) {
            (int newRow, int newCol) = (check.Item1 + row, check.Item2 + col);
            if (!IsValidRowCol(newRow, newCol)) continue;
            int index = ChessHelpers.RowColToIndex(newRow, newCol);
            byte piece = GetByteAtIndex(index);
            if (piece == 0) {
                outputs.Add(index);
            } else {
                bool pieceColor = (piece & 8) != 0;
                if (pieceColor != white) {
                    outputs.Add(index);
                }
            }
        }

        return [.. outputs];
    }

    private byte GetByteAtIndex(int index) {
        if (index < 0 || index > 63) ThrowIndexException();
        (int quot, int rem) = Math.DivRem(index, 2); // Calculates arrayIndex and modulo in one go
        if (rem == 0) {
            return ByteAdjustment.LeftHalf(board[quot]);
        } else {
            return ByteAdjustment.RightHalf(board[quot]);
        }
    }

    private static (ChessPiece Piece, bool White)? GetPiece(byte piece) {
        int checks = piece & 0x07;
        bool color = (piece & 0x08) != 0;
        return checks switch {
            1 => (ChessPiece.King, color),
            2 => (ChessPiece.Queen, color),
            3 => (ChessPiece.Bishop, color),
            4 => (ChessPiece.Knight, color),
            5 => (ChessPiece.Rook, color),
            6 => (ChessPiece.Pawn, color),
            _ => null
        };
    }

    private static byte GetByte(ChessPiece piece, bool white) {
        byte newPiece = piece switch {
            ChessPiece.King => 1,
            ChessPiece.Queen => 2,
            ChessPiece.Bishop => 3,
            ChessPiece.Knight => 4,
            ChessPiece.Rook => 5,
            ChessPiece.Pawn => 6,
            _ => throw new Exception("Unknown piece.")
        };
        return white ? (byte)(newPiece | 0x08) : newPiece;
    }

    private static byte[] DefaultBoard() =>
        [
            0xdc, 0xba, 0x9b, 0xcd,
            0xee, 0xee, 0xee, 0xee,
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,
            0x66, 0x66, 0x66, 0x66,
            0x54, 0x32, 0x13, 0x45
        ];

    public static bool operator !=(Chessboard left, Chessboard right) {
        return !left.Equals(right);
    }

    public static bool operator ==(Chessboard left, Chessboard right) {
        return left.Equals(right);
    }

    public override bool Equals(object? obj) {
        if (obj is not null && obj is Chessboard output)
            return Equals(output);
        return false;
    }

    public override int GetHashCode() {
        return board.GetHashCode();
    }

    public bool Equals(Chessboard? other) {
        if (other is null) return false;
        for (int i = 0; i < 32; i++) {
            if (board[i] != other.board[i]) return false;
        }

        return true;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowIndexException() => throw new ArgumentException("Chess index is out of bounds.");
}
