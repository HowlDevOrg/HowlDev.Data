using System.Runtime.CompilerServices;

namespace HowlDev.Data.Structures.Games.Chess;

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
        return ChessPieceConversion.GetPiece(GetByteAtIndex(index));
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

    public int[] GetValidMoves(int index) {
        (ChessPiece Piece, bool White)? piece = CheckSquare(index);
        if (piece is null) return [];

        (int row, int col) = ChessHelpers.IndexToRowCol(index);
        switch (piece.Value.Piece) {
            case ChessPiece.King:
                return GetKingSpaces(row, col, piece.Value.White);
            case ChessPiece.Queen:
                break;
            case ChessPiece.Rook:
                return GetRookSpaces(row, col, piece.Value.White);
            case ChessPiece.Bishop:
                return GetBishopSpaces(row, col, piece.Value.White);
            case ChessPiece.Knight:
                return GetKnightSpaces(row, col, piece.Value.White);
            case ChessPiece.Pawn:
                break;
        }

        return [];
    }

    public static Chessboard ReadFEN(string fen) {
        return new Chessboard(ChessHelpers.GetBoardFromFEN(fen));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int[] GetValidMoves(int row, int col) {
        return GetValidMoves(ChessHelpers.RowColToIndex(row, col));
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

    private int[] GetBishopSpaces(int row, int col, bool white) {
        return [
          ..SearchUntil(row, col,
          [(-1, -1), (1, -1), (-1, 1), (1, 1)],
          !white)
        ];
    }

    private int[] GetRookSpaces(int row, int col, bool white) {
        return [
          ..SearchUntil(row, col,
          [(-1, 0), (1, 0), (0, -1), (0, 1)],
          !white)
        ];
    }

    /// <summary>
    /// Returns a list of possible moves in the given direction(s). 
    /// </summary>
    private List<int> SearchUntil(int startRow, int startCol, (int rowOffset, int colOffset)[] offsets, bool opposingColor) {
        List<int> possibleIndexes = new(7 * offsets.Length);
        foreach ((int rowOffset, int colOffset) in offsets) {
            int newStartRow = startRow;
            int newStartCol = startCol;
            while (true) {
                (int newRow, int newCol) = (newStartRow += rowOffset, newStartCol += colOffset); // += is intentional here
                if (!IsValidRowCol(newRow, newCol)) break;
                int index = ChessHelpers.RowColToIndex(newRow, newCol);
                byte piece = GetByteAtIndex(index);
                if (piece == 0x00) {
                    possibleIndexes.Add(index);
                } else {
                    bool white = ChessPieceConversion.PieceColor(piece);
                    if (white == opposingColor) possibleIndexes.Add(index);
                    break;
                }
            }
        }

        return possibleIndexes;
    }

    private int[] ValidateChecks((int, int)[] checks, int row, int col, bool white) {
        List<int> outputs = new(8);
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
