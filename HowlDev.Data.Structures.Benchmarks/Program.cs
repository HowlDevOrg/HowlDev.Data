using HowlDev.Quality.Benchmarking;

namespace HowlDev.Data.Structures.Benchmarks;

public class Program {
    public static void Main(string[] args) {
        BenchmarkValidator.For<ChessboardGetValueAtSquareBench>()
            .Expect("GetSquareReadonlyInlineStart", BenchmarkExpectations.ExpectedBytes(0).WithNanoseconds(0.9).WithMarginOfError(1.25))
            .Expect("GetSquareReadonlyInlineEnd", BenchmarkExpectations.ExpectedBytes(0).WithNanoseconds(0.9).WithMarginOfError(1.25))
            .Run();

        BenchmarkValidator.For<ChessboardHelpersBench>()
            .Expect("GetIndex", BenchmarkExpectations.ExpectedNanosecondsLessThan(0.1).WithBytes(0))
            .Expect("GetRowCol", BenchmarkExpectations.ExpectedNanosecondsLessThan(0.1).WithBytes(0))
            .Run();
    }
}
