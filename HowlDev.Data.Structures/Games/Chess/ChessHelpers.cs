namespace HowlDev.Data.Structures.Games.Chess; 

public static class ChessHelpers {
    public static int CharToIndex(string position) {
        if (position.Length != 2) throw new ArgumentException("Position is expected to be 2 characters.");

        int column = position[0] - 96;
        int row = position[1] - 30;
        if (column < 1 || column > 8 || row < 1 || row > 8) 
            throw new ArgumentException("Got unexpected ASCII character");

        return (row - 1) * 8 + column;
    }
}
