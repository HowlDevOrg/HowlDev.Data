using System.Text.RegularExpressions;

namespace HowlDev.Data.Structures.Games.Chess;

public readonly partial struct ChessMove : IEquatable<ChessMove> {
    public readonly int ToIndex { get; }
    public readonly ChessPiece Piece { get; }
    public readonly bool Captures { get; }
    public readonly IEnumerable<int> PossibleStartLocations { get; }
    public ChessMove(ReadOnlySpan<char> move) {
        Match match = ChessNotationRegex().Match(move.ToString());
        if (move.Contains('x')) Captures = true;
        GroupCollection groups = match.Groups;
        ToIndex = ChessHelpers.CharToIndex(groups[4].ValueSpan);
        Piece = GetPiece(groups[1].ValueSpan);
        if (groups[2].Success || groups[3].Success) {
            PossibleStartLocations = [.. GetPossibleIndexes(ToIndex, groups[2].Value, groups[3].Value, Piece)];
        } else {
            PossibleStartLocations = [];
        }
    }

    private static ChessPiece GetPiece(ReadOnlySpan<char> piece) {
        return piece switch {
            "" => ChessPiece.Pawn,
            _ => throw new Exception("Invalid calling piece")
        };
    }

    private static IEnumerable<int> GetPossibleIndexes(int targetIndex, string col, string row, ChessPiece piece) {
        (int targetRow, int targetCol) = ChessHelpers.IndexToRowCol(targetIndex);
        (int intRow, int intCol) = (targetRow, targetCol); // Want to remove this line
        if (col != "") intCol = ChessHelpers.GetColumn(col[0]);
        if (row != "") intRow = ChessHelpers.GetRow(row[0]);
        switch (piece) {
            case ChessPiece.Pawn:
                if (Math.Abs(targetCol - intCol) != 1) throw new InvalidOperationException("Pawns can only take diagonally in a next-to row.");

                yield return ChessHelpers.RowColToIndex(intRow - 1, intCol);
                yield return ChessHelpers.RowColToIndex(intRow + 1, intCol);

                break;
            default:
                throw new Exception("Invalid piece argument.");
        }

        yield break;
    }


    [GeneratedRegex(@"^([NBRQK])?([a-h])?([1-8])?x?([a-h][1-8])=?([NBRQK])?([+#])?$", RegexOptions.Singleline)]
    public static partial Regex ChessNotationRegex();

    public static bool operator !=(ChessMove left, ChessMove right) {
        return !left.Equals(right);
    }

    public static bool operator ==(ChessMove left, ChessMove right) {
        return left.Equals(right);
    }

    public bool Equals(ChessMove other) {
        return other.ToIndex == ToIndex;
    }

    public override int GetHashCode() {
        return HashCode.Combine(ToIndex);
    }

    public override bool Equals(object? obj) {
        if (obj is not null && obj is ChessMove move)
            return Equals(move);
        return false;
    }
}
