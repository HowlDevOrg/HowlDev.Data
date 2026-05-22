using HowlDev.Quality.Benchmarking;

namespace HowlDev.Data.Structures.Benchmarks;

public class Program {
    public static void Main(string[] args) {
        BenchmarkValidator.For<ChessboardHelpersBench>()
            .Expect("GetIndex", BenchmarkExpectations.ExpectedNanosecondsLessThan(0.1).WithBytes(0))
            .Expect("GetRowCol", BenchmarkExpectations.ExpectedNanosecondsLessThan(0.1).WithBytes(0))
            .Run();

        BenchmarkValidator.For<ChessboardGetValueAtSquareBench>()
            .Expect("GetSquareReadonlyInlineStart", BenchmarkExpectations.ExpectedBytes(0).WithNanosecondsLessThan(1))
            .Expect("GetSquareReadonlyInlineReturnsNull", BenchmarkExpectations.ExpectedBytes(0).WithNanosecondsLessThan(1))
            .Expect("GetSquareReadonlyInlineEnd", BenchmarkExpectations.ExpectedBytes(0).WithNanosecondsLessThan(1))
            .Run();

        BenchmarkValidator.For<ChessboardPieceGetValidMovesBench>()
            .Expect("GetKingMoves", BenchmarkExpectations.ExpectedNanoseconds(45).WithBytes(184))
            .Expect("GetKnightMoves", BenchmarkExpectations.ExpectedNanoseconds(47).WithBytes(184))
            .Expect("GetBishopMoves", BenchmarkExpectations.ExpectedNanoseconds(75).WithBytes(464))
            .Run();
    }
}
