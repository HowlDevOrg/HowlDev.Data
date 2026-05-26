using HowlDev.Data.Structures.Games.Chess;
namespace HowlDev.Data.Structures.Tests.Chess;

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
        await Assert.That(move.PossibleStartLocations.Count()).IsEqualTo(possibleStartLocations.Length);
        for (int i = 0; i < possibleStartLocations.Length; i++) {
            await Assert.That(move.PossibleStartLocations.Contains(possibleStartLocations[i])).IsTrue();
        }
    }

    public static IEnumerable<Func<(string, int, int[])>> GetPawnCaptureSource() {
        yield return () => ("dxc6", 42, [35]);
        yield return () => ("bxc6", 42, [33]);
    }
}
