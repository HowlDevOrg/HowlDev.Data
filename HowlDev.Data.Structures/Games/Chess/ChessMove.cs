using System.Text.RegularExpressions;

namespace HowlDev.Data.Structures.Games.Chess;

public readonly partial struct ChessMove : IEquatable<ChessMove> {
    public readonly int ToIndex { get; }
    public readonly ChessPiece Piece { get; }
    public readonly ChessPiece? PromotionPiece { get; }
    public readonly bool Captures { get; }
    public readonly KingStatus KingStatus { get; }
    public readonly IEnumerable<int> PossibleStartLocations { get; }
    public ChessMove(ReadOnlySpan<char> move) {
        Match match = ChessNotationRegex().Match(move.ToString());
        GroupCollection groups = match.Groups;
        Captures = !string.IsNullOrWhiteSpace(groups[4].Value);
        ToIndex = ChessHelpers.CharToIndex(groups[5].ValueSpan);
        Piece = GetPiece(groups[1].ValueSpan);
        PossibleStartLocations = GetPossibleIndexes(groups[2].Value, groups[3].Value);
        if (groups[6].Value != "") {
            PromotionPiece = ChessHelpers.CharToPiece(groups[6].ValueSpan[0]).Piece;
        } else {
            PromotionPiece = null;
        }

        KingStatus = GetKingStatus(groups[7].ValueSpan);
    }

    private IEnumerable<int> GetPossibleIndexes(string col, string row) {
        (int targetRow, int targetCol) = ChessHelpers.IndexToRowCol(ToIndex);
        (int intRow, int intCol) = (targetRow, targetCol); // Want to remove this line
        if (col != "") intCol = ChessHelpers.GetColumn(col[0]);
        if (row != "") intRow = ChessHelpers.GetRow(row[0]);
        switch (Piece) {
            case ChessPiece.Pawn:
                if (Captures) {
                    if (Math.Abs(targetCol - intCol) != 1) Throw.PawnDiagonalizedError();
                    yield return ChessHelpers.RowColToIndex(intRow - 1, intCol);
                    yield return ChessHelpers.RowColToIndex(intRow + 1, intCol);
                } else {
                    if (intRow == 7) {
                        yield return ChessHelpers.RowColToIndex(6, intCol);
                    } else if (intRow == 2) {
                        yield return ChessHelpers.RowColToIndex(3, intCol);
                    } else {
                        if (intRow == 4) yield return ChessHelpers.RowColToIndex(2, intCol);
                        if (intRow == 5) yield return ChessHelpers.RowColToIndex(7, intCol);
                        yield return ChessHelpers.RowColToIndex(intRow + 1, intCol);
                        yield return ChessHelpers.RowColToIndex(intRow - 1, intCol);
                    }
                }

                yield break;
            default:
                Throw.InvalidPieceError();
                yield break;
        }
    }

    private static ChessPiece GetPiece(ReadOnlySpan<char> piece) {
        return piece switch {
            "" => ChessPiece.Pawn,
            _ => Throw.InvalidPieceError()
        };
    }

    private static KingStatus GetKingStatus(ReadOnlySpan<char> status) {
        return status switch {
            "" => KingStatus.None,
            "+" => KingStatus.Check,
            "#" => KingStatus.Checkmate,
            _ => Throw.InvalidKingStatus(status)
        };
    }

    [GeneratedRegex(@"^([NBRQK])?([a-h])?([1-8])?(x)?([a-h][1-8])=?([NBRQK])?([+#])?$", RegexOptions.Singleline)]
    private static partial Regex ChessNotationRegex();

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
