using BenchmarkDotNet.Attributes;
using HowlDev.Data.Structures.Games.Chess;

namespace HowlDev.Data.Structures.Benchmarks;
#pragma warning disable CA1822

public class ChessboardHelpersBench {
    [Benchmark]
    public int GetIndex() => ChessHelpers.RowColToIndex(1, 1);
    [Benchmark]
    public (int row, int col) GetRowCol() => ChessHelpers.IndexToRowCol(45);
}
