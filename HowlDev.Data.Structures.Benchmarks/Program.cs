using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HowlDev.Data.Structures.Games.Chess;

namespace HowlDev.Data.Structures.Benchmarks;

[MemoryDiagnoser]
[MediumRunJob]
public class ChessboardGetValueAtSquare {
    private Chessboard board = new Chessboard();

    [Benchmark]
    public (ChessPiece? Piece, bool Color) GetSquareReadonlyInline() => board.CheckSquare(0);
}

public class Program {
    public static void Main(string[] args) {
        BenchmarkRunner.Run<ChessboardGetValueAtSquare>();
    }
}
