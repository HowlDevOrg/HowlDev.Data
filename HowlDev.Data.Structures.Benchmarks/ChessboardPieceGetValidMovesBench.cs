using BenchmarkDotNet.Attributes;
using HowlDev.Data.Structures.Games.Chess;

namespace HowlDev.Data.Structures.Benchmarks;

[MemoryDiagnoser]
[ShortRunJob]
public class ChessboardPieceGetValidMovesBench {
    private Chessboard board = Chessboard.ReadFEN("8/8/4P3/4K3/3p4/8/8/8");
    [Benchmark]
    public List<int> GetKingMoves() => [.. board.GetValidMoves(36, true)];
}
