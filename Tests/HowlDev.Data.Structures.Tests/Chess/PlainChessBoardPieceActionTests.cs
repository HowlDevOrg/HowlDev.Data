using HowlDev.Data.Structures.Games.Chess;
namespace HowlDev.Data.Structures.Tests.Chess;

public class KingTests {
    [Test]
    public async Task KingMoves1() {
        Chessboard c = Chessboard.ReadFEN("8/8/8/4K3/8/8/8/8");
        await Assert.That(c.CheckSquare(36).HasValue).IsTrue();
        await Assert.That(c.CheckSquare(36)!.Value.Piece).IsEqualTo(ChessPiece.King);
        await Assert.That(c.CheckSquare(36)!.Value.White).IsEqualTo(true);
        int[] kingMoves = c.GetValidMoves(36);
        int[] expMoves = [43, 44, 45, 35, 37, 27, 28, 29];
        await Assert.That(kingMoves.Count).IsEqualTo(8);
        foreach (int item in expMoves) {
            await Assert.That(kingMoves.Contains(item)).IsTrue();
        }
    }

    [Test]
    public async Task KingMoves2() {
        Chessboard c = Chessboard.ReadFEN("8/8/4P3/4K3/3p4/8/8/8");
        int[] kingMoves = c.GetValidMoves(36);
        int[] expMoves = [43, 45, 35, 37, 27, 28, 29];
        await Assert.That(kingMoves.Count).IsEqualTo(7);
        foreach (int item in expMoves) {
            await Assert.That(kingMoves.Contains(item)).IsTrue();
        }
    }

    [Test]
    public async Task KingMovesDefaultBoard() {
        Chessboard c = new Chessboard();
        int[] kingMoves = c.GetValidMoves(5);
        await Assert.That(kingMoves.Count).IsEqualTo(0);
    }
}

public class KnightTests {
    [Test]
    public async Task KnightMoves1() {
        Chessboard c = Chessboard.ReadFEN("8/8/8/4N3/8/8/8/8");
        await Assert.That(c.CheckSquare(36).HasValue).IsTrue();
        await Assert.That(c.CheckSquare(36)!.Value.Piece).IsEqualTo(ChessPiece.Knight);
        await Assert.That(c.CheckSquare(36)!.Value.White).IsEqualTo(true);
        int[] knightMoves = c.GetValidMoves(36);
        int[] expMoves = [42, 51, 53, 46, 30, 21, 19, 26];
        await Assert.That(knightMoves.Count).IsEqualTo(8);
        foreach (int item in expMoves) {
            await Assert.That(knightMoves.Contains(item)).IsTrue();
        }
    }

    [Test]
    public async Task KnightMoves2() {
        Chessboard c = Chessboard.ReadFEN("8/8/2p5/4N3/4P3/5P2/8/8");
        await Assert.That(c.CheckSquare(36).HasValue).IsTrue();
        await Assert.That(c.CheckSquare(36)!.Value.Piece).IsEqualTo(ChessPiece.Knight);
        await Assert.That(c.CheckSquare(36)!.Value.White).IsEqualTo(true);
        int[] knightMoves = c.GetValidMoves(36);
        int[] expMoves = [42, 51, 53, 46, 30, 19, 26];
        await Assert.That(knightMoves.Count).IsEqualTo(7);
        foreach (int item in expMoves) {
            await Assert.That(knightMoves.Contains(item)).IsTrue();
        }
    }

    [Test]
    public async Task KnightMovesDefaultBoard() {
        Chessboard c = new Chessboard();
        int[] knightMoves = c.GetValidMoves(1);
        await Assert.That(knightMoves.Count).IsEqualTo(2);
        int[] expMoves = [16, 18];
        foreach (int item in expMoves) {
            await Assert.That(knightMoves.Contains(item)).IsTrue();
        }
    }
}

public class BishopTests {
    [Test]
    public async Task BishopMoves1() {
        Chessboard c = Chessboard.ReadFEN("8/8/8/4B3/8/8/8/8");
        await Assert.That(c.CheckSquare(36).HasValue).IsTrue();
        await Assert.That(c.CheckSquare(36)!.Value.Piece).IsEqualTo(ChessPiece.Bishop);
        await Assert.That(c.CheckSquare(36)!.Value.White).IsEqualTo(true);
        int[] bishopMoves = c.GetValidMoves(36);
        int[] expMoves = [43, 50, 57, 45, 54, 63, 27, 18, 9, 0, 29, 22, 15];
        await Assert.That(bishopMoves.Count).IsEqualTo(13);
        foreach (int item in expMoves) {
            await Assert.That(bishopMoves.Contains(item)).IsTrue();
        }
    }

    [Test]
    public async Task BishopMoves2() {
        Chessboard c = Chessboard.ReadFEN("8/8/3p1P2/4B3/3p1P2/8/8/8");
        await Assert.That(c.CheckSquare(36).HasValue).IsTrue();
        await Assert.That(c.CheckSquare(36)!.Value.Piece).IsEqualTo(ChessPiece.Bishop);
        await Assert.That(c.CheckSquare(36)!.Value.White).IsEqualTo(true);
        int[] bishopMoves = c.GetValidMoves(36);
        int[] expMoves = [43, 27];
        await Assert.That(bishopMoves.Count).IsEqualTo(2);
        foreach (int item in expMoves) {
            await Assert.That(bishopMoves.Contains(item)).IsTrue();
        }
    }

    [Test]
    public async Task BishopMoves3() {
        Chessboard c = Chessboard.ReadFEN("7P/8/8/4B3/8/8/8/p7");
        await Assert.That(c.CheckSquare(36).HasValue).IsTrue();
        await Assert.That(c.CheckSquare(36)!.Value.Piece).IsEqualTo(ChessPiece.Bishop);
        await Assert.That(c.CheckSquare(36)!.Value.White).IsEqualTo(true);
        int[] bishopMoves = c.GetValidMoves(36);
        int[] expMoves = [43, 50, 57, 45, 54, 27, 18, 9, 0, 29, 22, 15];
        await Assert.That(bishopMoves.Count).IsEqualTo(12);
        foreach (int item in expMoves) {
            await Assert.That(bishopMoves.Contains(item)).IsTrue();
        }
    }

    [Test]
    public async Task BishopMovesDefaultBoard() {
        Chessboard c = new Chessboard();
        int[] bishopMoves = c.GetValidMoves(2);
        await Assert.That(bishopMoves.Count).IsEqualTo(0);
    }
}
