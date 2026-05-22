using HowlDev.Data.Structures.Games.Chess;
namespace HowlDev.Data.Structures.Tests.Chess;

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
            await Assert.That(c.CheckSquare(i)).IsNull();
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

    [Test]
    public async Task BoardCorrectlyCalculatesColorLists() {
        Chessboard c = new Chessboard();
        await Assert.That(c.GetChessPieces(true).Count()).IsEqualTo(16);
        await Assert.That(c.GetChessPieces(false).Count()).IsEqualTo(16);
    }

    [Test]
    [Arguments("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR", 16, 16)]
    [Arguments("rnbqkbnr/pppppppp/8/8/8/7P/PPPPPPPP/RNBQKBNR", 17, 16)]
    public async Task BoardCanBeBuiltWithFEN(string fen, int expWhite, int expBlack) {
        Chessboard c = Chessboard.ReadFEN(fen);
        await Assert.That(c.GetChessPieces(true).Count()).IsEqualTo(expWhite);
        await Assert.That(c.GetChessPieces(false).Count()).IsEqualTo(expBlack);
    }

    [Test]
    public async Task BoardBuiltWithFENIsEquivalent() {
        Chessboard c = new Chessboard();
        Chessboard c2 = Chessboard.ReadFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");
        await Assert.That(c.Equals(c2)).IsTrue();
    }

    [Test]
    public async Task BoardBuiltWithDifferentFENIsNotEquivalent() {
        Chessboard c = new Chessboard();
        Chessboard c2 = Chessboard.ReadFEN("rnbqkbnr/pppppppp/8/8/8/7p/PPPPPPPP/RNBQKBNR");
        await Assert.That(c.Equals(c2)).IsFalse();
    }
}
