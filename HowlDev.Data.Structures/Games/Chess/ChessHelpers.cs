using System.Runtime.CompilerServices;

namespace HowlDev.Data.Structures.Games.Chess;

public static class ChessHelpers {
    /// <summary>
    /// Expects a string with a lowercase letter from a-h and then a number
    /// from 1-8 inclusive. 
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidDataException"></exception>
    public static int CharToIndex(string position) {
        if (position.Length != 2) throw new ArgumentException("Position is expected to be 2 characters.");

        int column = position[0] - 96;
        int row = position[1] - 48;
        return RowColToIndex(row, column);
    }

    // [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int RowColToIndex(int row, int col) {
        if (row < 1 || row > 8) ThrowRowException();
        if (col < 1 || col > 8) ThrowColException();

        return (row - 1) * 8 + col - 1;
    }

    // [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (int row, int col) IndexToRowCol(int index) {
        if (index < 0 || index > 63) ThrowIndexException();
        (int quot, int rem) = Math.DivRem(index, 8);
        return (quot + 1, rem + 1);
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

    private static (ChessPiece Piece, bool White) CharToPiece(char piece) {
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
            _ => throw new InvalidDataException($"Can't read piece type {piece}.")
        };
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowRowException() => throw new InvalidDataException($"Row must be between 1 and 8 inclusive.");
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowColException() => throw new InvalidDataException($"Column must be between 1 and 8 inclusive.");
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowIndexException() => throw new InvalidDataException($"Index must be between 0 and 63 inclusive.");
}
