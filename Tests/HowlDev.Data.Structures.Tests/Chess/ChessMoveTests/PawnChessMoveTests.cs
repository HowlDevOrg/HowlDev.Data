using HowlDev.Data.Structures.Games.Chess;
namespace HowlDev.Data.Structures.Tests.Chess.ChessMoveTests;

public class PawnChessMoveTests {
    [Test]
    [MethodDataSource(nameof(PawnMovementSource))]
    public async Task PawnMovement(string input, int expIndex, int[] possibleStartLocations) {
        ChessMove move = new ChessMove(input);
        await Assert.That(move.ToIndex).IsEqualTo(expIndex);
        await Assert.That(move.Piece).IsEqualTo(ChessPiece.Pawn);
        await Assert.That(move.Captures).IsFalse();
        await Assert.That(move.PossibleStartLocations).IsEquivalentTo(possibleStartLocations);
    }

    [Test]
    [MethodDataSource(nameof(PawnCaptureSource))]
    public async Task PawnCapture(string input, int expIndex, int[] possibleStartLocations) {
        ChessMove move = new ChessMove(input);
        await Assert.That(move.ToIndex).IsEqualTo(expIndex);
        await Assert.That(move.Piece).IsEqualTo(ChessPiece.Pawn);
        await Assert.That(move.Captures).IsTrue();
        await Assert.That(move.PossibleStartLocations).IsEquivalentTo(possibleStartLocations);
    } 

    [Test]
    [Arguments("c3", KingStatus.None)]
    [Arguments("c4", KingStatus.None)]
    [Arguments("h5+", KingStatus.Check)]
    [Arguments("c6+", KingStatus.Check)]
    [Arguments("g2#", KingStatus.Checkmate)]
    [Arguments("a3#", KingStatus.Checkmate)]
    public async Task PawnMovementWithKingStatus(string input, KingStatus expStatus) {
        ChessMove move = new ChessMove(input);
        await Assert.That(move.KingStatus).IsEqualTo(expStatus);
    }

    [Test]
    [Arguments("dxc6", KingStatus.None)]
    [Arguments("bxc6", KingStatus.None)]
    [Arguments("axb3+", KingStatus.Check)]
    [Arguments("bxa3+", KingStatus.Check)]
    [Arguments("gxh5#", KingStatus.Checkmate)]
    [Arguments("hxg5#", KingStatus.Checkmate)]
    public async Task PawnCaptureWithKingStatus(string input, KingStatus expStatus) {
        ChessMove move = new ChessMove(input);
        await Assert.That(move.KingStatus).IsEqualTo(expStatus);
    }

    [Test]
    [Arguments("axc3")]
    [Arguments("cxc4")]
    [Arguments("axh5")]
    [Arguments("exc6")]
    [Arguments("exg2")]
    [Arguments("cxa3")]
    [Arguments("exc6+")]
    [Arguments("exg2+")]
    [Arguments("cxa3#")]
    public async Task PawnCaptureThrowsErrorsWhenNotNextTo(string input) {
        await Assert.That(() => new ChessMove(input).PossibleStartLocations.ToList())
            .Throws<InvalidOperationException>();
    }

    public static IEnumerable<Func<(string, int, int[])>> PawnMovementSource() {
        yield return () => ("c6", 42, [50, 34]);
        yield return () => ("a3", 16, [8, 24]);
        yield return () => ("a2", 8, [16]);
        yield return () => ("h7", 55, [47]);
        yield return () => ("g5", 38, [46, 30, 54]);
        yield return () => ("b4", 25, [9, 17, 33]);
    }

    public static IEnumerable<Func<(string, int, int[])>> PawnCaptureSource() {
        yield return () => ("dxc6", 42, [35, 51]);
        yield return () => ("bxc6", 42, [33, 49]);
        yield return () => ("axb3", 17, [8, 24]);
        yield return () => ("bxa3", 16, [9, 25]);
        yield return () => ("gxh5", 39, [30, 46]);
        yield return () => ("hxg5", 38, [31, 47]);
    }
}
