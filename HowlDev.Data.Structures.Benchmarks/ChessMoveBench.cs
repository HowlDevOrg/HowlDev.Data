using BenchmarkDotNet.Attributes;
using HowlDev.Data.Structures.Games.Chess;

namespace HowlDev.Data.Structures.Benchmarks; 

public class ChessMoveBench {
    [Benchmark]
    public ChessMove DefaultMove() => new ChessMove("c6");
}
