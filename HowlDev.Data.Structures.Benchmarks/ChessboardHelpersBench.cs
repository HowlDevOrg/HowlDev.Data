using BenchmarkDotNet.Attributes;
using HowlDev.Data.Structures.Games.Chess;

namespace HowlDev.Data.Structures.Benchmarks;
#pragma warning disable CA1822

[MemoryDiagnoser]
[ShortRunJob]
public class ChessboardHelpersBench {
    [Benchmark]
    public int GetIndex() => ChessHelpers.RowColToIndex(1, 1);
    [Benchmark]
    public int GetIndexThrowError() {
        try {
            return ChessHelpers.RowColToIndex(0, 9);
        } catch {
            return 0;
        }
    }
    [Benchmark]
    public (int row, int col) GetRowCol() => ChessHelpers.IndexToRowCol(45);
}
