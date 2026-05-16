namespace HowlDev.Data.Structures.Games.Chess;

public record struct ChessMove(int From, int To, int? Remove, ChessPiece Piece);
