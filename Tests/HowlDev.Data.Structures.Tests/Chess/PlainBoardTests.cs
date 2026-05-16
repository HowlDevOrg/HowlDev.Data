using HowlDev.Data.Structures.Games;
namespace HowlDev.Data.Structures.Tests;

public class PlainChessBoardTests {
    [Test]
    public async Task DefaultBoardCheck() {
        Chessboard c = new Chessboard();
        await Assert.That(c.CheckSquare(0)).IsEqualTo((ChessPiece.Rook, true));
        await Assert.That(c.CheckSquare(1)).IsEqualTo((ChessPiece.Knight, true));
        await Assert.That(c.CheckSquare(2)).IsEqualTo((ChessPiece.Bishop, true));
        await Assert.That(c.CheckSquare(3)).IsEqualTo((ChessPiece.Queen, true));
        await Assert.That(c.CheckSquare(4)).IsEqualTo((ChessPiece.King, true));
        await Assert.That(c.CheckSquare(5)).IsEqualTo((ChessPiece.Bishop, true));
        await Assert.That(c.CheckSquare(6)).IsEqualTo((ChessPiece.Knight, true));
        await Assert.That(c.CheckSquare(7)).IsEqualTo((ChessPiece.Rook, true));
        for (int i = 8; i < 16; i++) {
            await Assert.That(c.CheckSquare(i)).IsEqualTo((ChessPiece.Pawn, true));
        }
        for (int i = 16; i < 48; i++) {
            await Assert.That(c.CheckSquare(i)).IsEqualTo((null, false));
        }
        for (int i = 48; i < 56; i++) {
            await Assert.That(c.CheckSquare(i)).IsEqualTo((ChessPiece.Pawn, false));
        }
        await Assert.That(c.CheckSquare(56)).IsEqualTo((ChessPiece.Rook, false));
        await Assert.That(c.CheckSquare(57)).IsEqualTo((ChessPiece.Knight, false));
        await Assert.That(c.CheckSquare(58)).IsEqualTo((ChessPiece.Bishop, false));
        await Assert.That(c.CheckSquare(59)).IsEqualTo((ChessPiece.Queen, false));
        await Assert.That(c.CheckSquare(60)).IsEqualTo((ChessPiece.King, false));
        await Assert.That(c.CheckSquare(61)).IsEqualTo((ChessPiece.Bishop, false));
        await Assert.That(c.CheckSquare(62)).IsEqualTo((ChessPiece.Knight, false));
        await Assert.That(c.CheckSquare(63)).IsEqualTo((ChessPiece.Rook, false));
    }
}