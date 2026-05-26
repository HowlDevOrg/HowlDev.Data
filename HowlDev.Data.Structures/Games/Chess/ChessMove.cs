using System.Text.RegularExpressions;

namespace HowlDev.Data.Structures.Games.Chess;

public readonly partial struct ChessMove : IEquatable<ChessMove> {
    public readonly int ToIndex { get; }
    public readonly ChessPiece Piece { get; }
    public readonly bool Captures { get; }
    public readonly IEnumerable<int> PossibleStartLocations { get; }
    public ChessMove(string move) {
        Match match = ChessNotationRegex().Match(move);
        if (move.Contains('x')) Captures = true;
        ToIndex = ChessHelpers.CharToIndex(match.Groups[4].Value);
        Piece = GetPiece(match.Groups[1].ValueSpan);
        PossibleStartLocations = [
            35
        ];
    }

    public static ChessPiece GetPiece(ReadOnlySpan<char> piece) {
        return piece switch {
            "" => ChessPiece.Pawn, 
            _ => throw new Exception("Invalid calling piece")
        };
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
