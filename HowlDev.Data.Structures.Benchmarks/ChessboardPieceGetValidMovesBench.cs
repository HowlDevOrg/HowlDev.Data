using BenchmarkDotNet.Attributes;
using HowlDev.Data.Structures.Games.Chess;

namespace HowlDev.Data.Structures.Benchmarks;

[MemoryDiagnoser]
[ShortRunJob]
public class ChessboardPieceGetValidMovesBench {
    private Chessboard board = Chessboard.ReadFEN("7r/8/4P3/4K3/3p4/2N5/6Q1/4B3");
    [Benchmark]
    public int[] GetKingMoves() => board.GetValidMoves(36);
    [Benchmark]
    public int[] GetKnightMoves() => board.GetValidMoves(18);
    [Benchmark]
    public int[] GetBishopMoves() => board.GetValidMoves(4);
    [Benchmark]
    public int[] GetRookMoves() => board.GetValidMoves(63);
    [Benchmark]
    public int[] GetQueenMoves() => board.GetValidMoves(14);
    [Benchmark]
    public int[] GetPawnMoves() => board.GetValidMoves(44);
}
