using HowlDev.Quality.Benchmarking;

namespace HowlDev.Data.Structures.Benchmarks;

public class Program {
    public static void Main(string[] _) {
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
            .Expect("GetKingMoves", BenchmarkExpectations.ExpectedNanoseconds(35).WithBytes(144))
            .Expect("GetKnightMoves", BenchmarkExpectations.ExpectedNanoseconds(35).WithBytes(144))
            .Expect("GetBishopMoves", BenchmarkExpectations.ExpectedNanoseconds(52).WithBytes(200))
            .Expect("GetRookMoves", BenchmarkExpectations.ExpectedNanoseconds(90).WithBytes(240))
            .Expect("GetQueenMoves", BenchmarkExpectations.ExpectedNanoseconds(130).WithBytes(312))
            .Expect("GetPawnMoves", BenchmarkExpectations.ExpectedNanoseconds(25).WithBytes(104))
            .Run();
    }
}
