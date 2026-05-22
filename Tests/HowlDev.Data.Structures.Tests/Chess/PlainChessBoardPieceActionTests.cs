using HowlDev.Data.Structures.Games.Chess;
namespace HowlDev.Data.Structures.Tests.Chess;

public class PlainChessBoardPieceActionTests {
    [Test]
    public async Task KingMovesCheck1() {
        Chessboard c = Chessboard.ReadFEN("8/8/8/4K3/8/8/8/8");
        await Assert.That(c.CheckSquare(36).HasValue).IsTrue();
        await Assert.That(c.CheckSquare(36)!.Value.Piece).IsEqualTo(ChessPiece.King);
        await Assert.That(c.CheckSquare(36)!.Value.White).IsEqualTo(true);
        HashSet<int> kingMoves = [.. c.GetValidMoves(36, true)];
        int[] expMoves = [43, 44, 45, 35, 37, 27, 28, 29];
        await Assert.That(kingMoves.Count).IsEqualTo(8);
        foreach (int item in expMoves) {
            await Assert.That(kingMoves.Contains(item)).IsTrue();
        }
    }

    [Test]
    public async Task KingMovesCheck2() {
        Chessboard c = Chessboard.ReadFEN("8/8/4P3/4K3/3p4/8/8/8");
        HashSet<int> kingMoves = [.. c.GetValidMoves(36, true)];
        int[] expMoves = [43, 45, 35, 37, 27, 28, 29];
        await Assert.That(kingMoves.Count).IsEqualTo(7);
        foreach (int item in expMoves) {
            await Assert.That(kingMoves.Contains(item)).IsTrue();
        }
    }
}
