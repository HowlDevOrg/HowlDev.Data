using BenchmarkDotNet.Attributes;
using HowlDev.Data.Structures.Games.Chess;

namespace HowlDev.Data.Structures.Benchmarks;

[MemoryDiagnoser]
[ShortRunJob]
public class ChessboardGetValueAtSquareBench {
    private Chessboard board;
    public ChessboardGetValueAtSquareBench() {
        board = new Chessboard();
    }

    [Benchmark]
    public (ChessPiece Piece, bool Color)? GetSquareReadonlyInlineStart() => board.CheckSquare(0);
    [Benchmark]
    public (ChessPiece Piece, bool Color)? GetSquareReadonlyInlineReturnsNull() => board.CheckSquare(32);
    [Benchmark]
    public (ChessPiece Piece, bool Color)? GetSquareReadonlyInlineEnd() => board.CheckSquare(63);
}
