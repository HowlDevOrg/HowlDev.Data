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

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowRowException() => throw new InvalidDataException($"Row must be between 1 and 8 inclusive.");
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowColException() => throw new InvalidDataException($"Column must be between 1 and 8 inclusive.");
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowIndexException() => throw new InvalidDataException($"Index must be between 0 and 63 inclusive.");
}
