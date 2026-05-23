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
        int[] kingMoves = c.GetValidMoves(4);
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

public class RookTests {
    [Test]
    public async Task RookMoves1() {
        Chessboard c = Chessboard.ReadFEN("8/8/8/4R3/8/8/8/8");
        await Assert.That(c.CheckSquare(36).HasValue).IsTrue();
        await Assert.That(c.CheckSquare(36)!.Value.Piece).IsEqualTo(ChessPiece.Rook);
        await Assert.That(c.CheckSquare(36)!.Value.White).IsEqualTo(true);
        int[] rookMoves = c.GetValidMoves(36);
        int[] expMoves = [28, 20, 12, 4, 37, 38, 39, 35, 34, 33, 32, 44, 52, 60];
        await Assert.That(rookMoves.Count).IsEqualTo(14);
        foreach (int item in expMoves) {
            await Assert.That(rookMoves.Contains(item)).IsTrue();
        }
    }

    [Test]
    public async Task RookMoves2() {
        Chessboard c = Chessboard.ReadFEN("8/4p3/5P2/3pR3/3p1P2/8/8/4K3");
        await Assert.That(c.CheckSquare(36).HasValue).IsTrue();
        await Assert.That(c.CheckSquare(36)!.Value.Piece).IsEqualTo(ChessPiece.Rook);
        await Assert.That(c.CheckSquare(36)!.Value.White).IsEqualTo(true);
        int[] rookMoves = c.GetValidMoves(36);
        int[] expMoves = [28, 20, 12, 37, 38, 39, 35, 44, 52];
        await Assert.That(rookMoves.Count).IsEqualTo(9);
        foreach (int item in expMoves) {
            await Assert.That(rookMoves.Contains(item)).IsTrue();
        }
    }

    [Test]
    public async Task RookMovesDefaultBoard() {
        Chessboard c = new Chessboard();
        int[] rookMoves = c.GetValidMoves(0);
        await Assert.That(rookMoves.Count).IsEqualTo(0);
    }
}

public class QueenTests {
    [Test]
    public async Task QueenMoves1() {
        Chessboard c = Chessboard.ReadFEN("8/8/8/4Q3/8/8/8/8");
        await Assert.That(c.CheckSquare(36).HasValue).IsTrue();
        await Assert.That(c.CheckSquare(36)!.Value.Piece).IsEqualTo(ChessPiece.Queen);
        await Assert.That(c.CheckSquare(36)!.Value.White).IsEqualTo(true);
        int[] queenMoves = c.GetValidMoves(36);
        int[] expMoves = [28, 20, 12, 4, 37, 38, 39, 35, 34, 33, 32, 44, 52, 60, 43, 50, 57, 45, 54, 63, 27, 18, 9, 0, 29, 22, 15];
        await Assert.That(queenMoves.Count).IsEqualTo(27);
        foreach (int item in expMoves) {
            await Assert.That(queenMoves.Contains(item)).IsTrue();
        }
    }

    [Test]
    public async Task QueenMoves2() {
        Chessboard c = Chessboard.ReadFEN("8/4p3/8/3pQ3/3p1P2/8/8/4K3");
        await Assert.That(c.CheckSquare(36).HasValue).IsTrue();
        await Assert.That(c.CheckSquare(36)!.Value.Piece).IsEqualTo(ChessPiece.Queen);
        await Assert.That(c.CheckSquare(36)!.Value.White).IsEqualTo(true);
        int[] queenMoves = c.GetValidMoves(36);
        int[] expMoves = [28, 27, 20, 12, 37, 38, 39, 35, 44, 52, 43, 50, 57, 45, 54, 63];
        await Assert.That(queenMoves.Count).IsEqualTo(16);
        foreach (int item in expMoves) {
            await Assert.That(queenMoves.Contains(item)).IsTrue();
        }
    }

    [Test]
    public async Task QueenMovesDefaultBoard() {
        Chessboard c = new Chessboard();
        int[] queenMoves = c.GetValidMoves(3);
        await Assert.That(queenMoves.Count).IsEqualTo(0);
    }
}

public class Tests {
    [Test]
    public async Task WhitePawnMovesFreeBoard() {
        Chessboard c = Chessboard.ReadFEN("8/8/8/4P3/8/8/8/8");
        await Assert.That(c.CheckSquare(36).HasValue).IsTrue();
        await Assert.That(c.CheckSquare(36)!.Value.Piece).IsEqualTo(ChessPiece.Pawn);
        await Assert.That(c.CheckSquare(36)!.Value.White).IsEqualTo(true);
        int[] pawnMoves = c.GetValidMoves(36);
        await Assert.That(pawnMoves.Count).IsEqualTo(1);
            await Assert.That(pawnMoves.Contains(44)).IsTrue();
    }

    [Test]
    public async Task BlackPawnMovesFreeBoard() {
        Chessboard c = Chessboard.ReadFEN("8/8/8/4p3/8/8/8/8");
        await Assert.That(c.CheckSquare(36).HasValue).IsTrue();
        await Assert.That(c.CheckSquare(36)!.Value.Piece).IsEqualTo(ChessPiece.Pawn);
        await Assert.That(c.CheckSquare(36)!.Value.White).IsEqualTo(false);
        int[] pawnMoves = c.GetValidMoves(36);
        await Assert.That(pawnMoves.Count).IsEqualTo(1);
            await Assert.That(pawnMoves.Contains(28)).IsTrue();
    }

    [Test]
    public async Task WhitePawnMovesBlocked() {
        Chessboard c = Chessboard.ReadFEN("8/8/4P3/4P3/8/8/8/8");
        int[] pawnMoves = c.GetValidMoves(36);
        await Assert.That(pawnMoves.Count).IsEqualTo(0);
    }

    [Test]
    public async Task BlackPawnMovesBlocked() {
        Chessboard c = Chessboard.ReadFEN("8/8/8/4p3/4P3/8/8/8");
        int[] pawnMoves = c.GetValidMoves(36);
        await Assert.That(pawnMoves.Count).IsEqualTo(0);
    }

    [Test]
    public async Task WhitePawnMovesTakes() {
        Chessboard c = Chessboard.ReadFEN("8/8/3p1p2/4P3/8/8/8/8");
        int[] pawnMoves = c.GetValidMoves(36);
        int[] expMoves = [43, 44, 45];
        await Assert.That(pawnMoves.Count).IsEqualTo(3);
        foreach (int item in expMoves) {
            await Assert.That(pawnMoves.Contains(item)).IsTrue();
        }
    }

    [Test]
    public async Task BlackPawnMovesTakes() {
        Chessboard c = Chessboard.ReadFEN("8/8/8/4p3/3P1P2/8/8/8");
        int[] pawnMoves = c.GetValidMoves(36);
        int[] expMoves = [27, 28, 29];
        await Assert.That(pawnMoves.Count).IsEqualTo(3);
        foreach (int item in expMoves) {
            await Assert.That(pawnMoves.Contains(item)).IsTrue();
        }
    }

    [Test]
    public async Task WhitePawnMovesDefaultBoard() {
        Chessboard c = new Chessboard();
        int[] pawnMoves = c.GetValidMoves(50);
        int[] expMoves = [42, 34];
        await Assert.That(pawnMoves.Count).IsEqualTo(2);
        foreach (int item in expMoves) {
            await Assert.That(pawnMoves.Contains(item)).IsTrue();
        }
    }

    [Test]
    public async Task BlackPawnMovesDefaultBoard() {
        Chessboard c = new Chessboard();
        int[] pawnMoves = c.GetValidMoves(10);
        int[] expMoves = [18, 26];
        await Assert.That(pawnMoves.Count).IsEqualTo(2);
        foreach (int item in expMoves) {
            await Assert.That(pawnMoves.Contains(item)).IsTrue();
        }
    }
}
