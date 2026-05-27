using System.Runtime.CompilerServices;

namespace HowlDev.Data.Structures.Games.Chess;

public static class ChessHelpers {
    /// <summary>
    /// Expects a string with a lowercase letter from a-h and then a number
    /// from 1-8 inclusive. 
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidDataException"></exception>
    public static int CharToIndex(ReadOnlySpan<char> position) {
        if (position.Length != 2) throw new ArgumentException("Position is expected to be 2 characters.");

        int column = GetColumn(position[0]);
        int row = GetRow(position[1]);
        return RowColToIndex(row, column);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int RowColToIndex(int row, int col) {
        if (RowOrColIsOutOfRange(row)) Throw.RowException();
        if (RowOrColIsOutOfRange(col)) Throw.ColException();

        return (row - 1) * 8 + col - 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (int row, int col) IndexToRowCol(int index) {
        if (index < 0 || index > 63) Throw.IndexException();
        (int quot, int rem) = Math.DivRem(index, 8);
        return (quot + 1, rem + 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetColumn(char col) {
        return col - 96;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetRow(char row) {
        return row - 48;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool RowOrColIsOutOfRange(int value) {
        return value < 1 || value > 8;
    }

    // Not optimized/benchmarked in any way, just returns an enumerable. 
    public static IEnumerable<(ChessPiece Piece, bool White)?> ParseFENNotation(string notation) {
        string[] rows = [.. notation.Split('/').Reverse()]; // Reversed for the way they come in.
        if (rows.Length != 8) throw new InvalidDataException("FEN should split into 8 strings via '/'");

        for (int i = 0; i < 8; i++) {
            foreach (char item in rows[i]) {
                if (int.TryParse(item.ToString(), out int result)) {
                    for (int k = 0; k < result; k++) {
                        yield return null;
                    }
                } else {
                    yield return CharToPiece(item);
                }
            }
        }
    }

    public static byte[] GetBoardFromFEN(string fen) {
        byte[] newBoard = new byte[32];
        byte currentPiece = 0x00;
        bool offset = false;
        int index = 0;
        foreach ((ChessPiece Piece, bool White)? item in ParseFENNotation(fen)) {
            byte newPiece = item.HasValue ? ChessPieceConversion.GetByte(item.Value.Piece, item.Value.White) : (byte)0x00;
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

        return newBoard;
    }

    public static (ChessPiece Piece, bool White) CharToPiece(char piece) {
        return piece switch {
            'K' => (ChessPiece.King, true),
            'Q' => (ChessPiece.Queen, true),
            'B' => (ChessPiece.Bishop, true),
            'N' => (ChessPiece.Knight, true),
            'R' => (ChessPiece.Rook, true),
            'P' => (ChessPiece.Pawn, true),
            'k' => (ChessPiece.King, false),
            'q' => (ChessPiece.Queen, false),
            'b' => (ChessPiece.Bishop, false),
            'n' => (ChessPiece.Knight, false),
            'r' => (ChessPiece.Rook, false),
            'p' => (ChessPiece.Pawn, false),
            _ => Throw.InvalidPieceType(piece)
        };
    }
}
