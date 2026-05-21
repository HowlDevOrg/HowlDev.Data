using HowlDev.Quality.Benchmarking;

namespace HowlDev.Data.Structures.Benchmarks;

public class Program {
    public static void Main(string[] args) {
        BenchmarkValidator.For<ChessboardGetValueAtSquareBench>()
            .Expect("GetSquareReadonlyInlineStart", BenchmarkExpectations.ExpectedBytes(0).WithNanosecondsLessThan(1))
            .Expect("GetSquareReadonlyInlineReturnsNull", BenchmarkExpectations.ExpectedBytes(0).WithNanosecondsLessThan(1))
            .Expect("GetSquareReadonlyInlineEnd", BenchmarkExpectations.ExpectedBytes(0).WithNanosecondsLessThan(1))
            .Run();

        BenchmarkValidator.For<ChessboardHelpersBench>()
            .Expect("GetIndex", BenchmarkExpectations.ExpectedNanosecondsLessThan(0.1).WithBytes(0))
            .Expect("GetIndexThrowError", BenchmarkExpectations.ExpectedMicroseconds(1.5).WithMarginOfError(1.25).WithBytes(320)) // :O
            .Expect("GetRowCol", BenchmarkExpectations.ExpectedNanosecondsLessThan(0.1).WithBytes(0))
            .Run();
    }
}
