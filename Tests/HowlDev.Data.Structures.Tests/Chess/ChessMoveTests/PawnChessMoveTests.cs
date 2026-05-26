using HowlDev.Data.Structures.Games.Chess;
namespace HowlDev.Data.Structures.Tests.Chess.ChessMoveTests;

public class PawnChessMoveTests {
    [Test]
    [Arguments("c6", 42)]
    [Arguments("a3", 16)]
    [Arguments("h7", 55)]
    [Arguments("g5", 38)]
    public async Task PawnMovement(string input, int expIndex) {
        ChessMove move = new ChessMove(input);
        await Assert.That(move.ToIndex).IsEqualTo(expIndex);
        await Assert.That(move.Piece).IsEqualTo(ChessPiece.Pawn);
    }

    [Test]
    [MethodDataSource(nameof(GetPawnCaptureSource))]
    public async Task PawnCapture(string input, int expIndex, int[] possibleStartLocations) {
        ChessMove move = new ChessMove(input);
        await Assert.That(move.ToIndex).IsEqualTo(expIndex);
        await Assert.That(move.Piece).IsEqualTo(ChessPiece.Pawn);
        await Assert.That(move.PossibleStartLocations).IsEquivalentTo(possibleStartLocations);
    }

    [Test]
    [Arguments("axc3")]
    [Arguments("cxc4")]
    [Arguments("axh5")]
    [Arguments("exc6")]
    [Arguments("exg2")]
    [Arguments("cxa3")]
    public async Task PawnCaptureThrowsErrorsWhenNotNextTo(string input) {
        await Assert.That(() => new ChessMove(input))
            .Throws<InvalidOperationException>();
    }

    public static IEnumerable<Func<(string, int, int[])>> GetPawnCaptureSource() {
        yield return () => ("dxc6", 42, [35, 51]);
        yield return () => ("bxc6", 42, [33, 49]);
        yield return () => ("axb3", 17, [8, 24]);
        yield return () => ("bxa3", 16, [9, 25]);
        yield return () => ("gxh5", 39, [30, 46]);
        yield return () => ("hxg5", 38, [31, 47]);
    }
}
