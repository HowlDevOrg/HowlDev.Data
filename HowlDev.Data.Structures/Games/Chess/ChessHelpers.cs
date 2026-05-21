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

    public static int RowColToIndex(int row, int col) {
        if (row < 1 || row > 8) throw new InvalidDataException($"Row must be between 1 and 8 inclusive. {row}");
        if (col < 1 || col > 8) throw new InvalidDataException($"Column must be between 1 and 8 inclusive. {col}");

        return (8 - row) * 8 + col - 1;
    }

    public static (int row, int col) IndexToRowCol(int index) {
        if (index < 0 || index > 63) throw new InvalidDataException($"Index must be between 0 and 63 inclusive. {index}");
        (int quot, int rem) = Math.DivRem(index, 8);
        return (8 - quot, rem + 1);
    }
}
