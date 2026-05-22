using System.Runtime.CompilerServices;

namespace HowlDev.Data.Structures.Games.Chess; 

// CHESS KEY: 
// 0 = Empty Cell
// 8 = Out of Bounds
// 1 = Black King
// 2 = Black Queen
// 3 = Black Bishop
// 4 = Black Knight
// 5 = Black Rook
// 6 = Black Pawn
// 9 = White King
// a/10 = White Queen
// b/11 = White Bishop
// c/12 = White Knight
// d/13 = White Rook
// e/14 = White Pawn

public static class ChessPieceConversion {
    public static (ChessPiece Piece, bool White)? GetPiece(byte piece) {
        int checks = piece & 0x07;
        bool color = (piece & 0x08) != 0;
        return checks switch {
            1 => (ChessPiece.King, color),
            2 => (ChessPiece.Queen, color),
            3 => (ChessPiece.Bishop, color),
            4 => (ChessPiece.Knight, color),
            5 => (ChessPiece.Rook, color),
            6 => (ChessPiece.Pawn, color),
            _ => null
        };
    }

    public static char? GetPieceRepresentation(byte piece) {
        int checks = piece & 0x07;
        bool color = PieceColor(piece);
        return checks switch {
            1 => color ? 'K' : 'k', 
            2 => color ? 'Q' : 'q', 
            3 => color ? 'B' : 'b', 
            4 => color ? 'N' : 'n', 
            5 => color ? 'R' : 'r', 
            6 => color ? 'P' : 'p', 
            _ => null
        };
    }

    public static byte GetByte(ChessPiece piece, bool white) {
        byte newPiece = piece switch {
            ChessPiece.King => 1,
            ChessPiece.Queen => 2,
            ChessPiece.Bishop => 3,
            ChessPiece.Knight => 4,
            ChessPiece.Rook => 5,
            ChessPiece.Pawn => 6,
            _ => throw new Exception("Unknown piece.")
        };
        return white ? (byte)(newPiece | 0x08) : newPiece;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool PieceColor(byte piece) {
        return (piece & 0x08) != 0;
    }
}
