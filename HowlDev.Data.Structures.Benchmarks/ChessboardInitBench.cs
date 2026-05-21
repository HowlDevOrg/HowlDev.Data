using BenchmarkDotNet.Attributes;
using HowlDev.Data.Structures.Games.Chess;

namespace HowlDev.Data.Structures.Benchmarks; 
#pragma warning disable CA1822

[MemoryDiagnoser]
[ShortRunJob]
public class ChessboardInitBench {
    [Benchmark]
    public Chessboard NewBoard() => new Chessboard();
}
