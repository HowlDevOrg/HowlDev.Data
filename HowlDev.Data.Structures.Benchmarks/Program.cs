using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HowlDev.Data.Structures.Games;

namespace HowlDev.Data.Structures.Benchmarks;

[ShortRunJob]
public class ChessboardGetValueAtSquare {
    private Chessboard board = new Chessboard();

    [Benchmark]
    public (ChessPiece? Piece, bool Color) GetSquare() => board.CheckSquare(0);
}

public class Program {
    public static void Main(string[] args) {
        BenchmarkRunner.Run<ChessboardGetValueAtSquare>();
    }
}
