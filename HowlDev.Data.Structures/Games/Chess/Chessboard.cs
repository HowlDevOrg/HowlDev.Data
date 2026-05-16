using System.Runtime.CompilerServices;

namespace HowlDev.Data.Structures.Games.Chess;

// CHESS KEY: 
// Positioning is white along the top.
// 0 = Empty Cell
// 8 = Out of Bounds
// 1 = White King
// 2 = White Queen
// 3 = White Bishop
// 4 = White Knight
// 5 = White Rook
// 6 = White Pawn
// 9 = Black King
// a = Black Queen
// b = Black Bishop
// c = Black Knight
// d = Black Rook
// e = Black Pawn

// This makes an array that.. 
[InlineArray(32)]
internal struct ChessboardData {
    private byte square;
}

/// <summary>
/// This struct allows you to play chess with all the game rules implemented. 
/// 
/// This holds an internal byte array for optimized Chess checking. 
/// </summary>
public readonly struct Chessboard : IEquatable<Chessboard> {
    private readonly ChessboardData board;
    public Chessboard() {
        ChessboardData data = new ChessboardData();
        data[0] = 0x54;
        data[1] = 0x32;
        data[2] = 0x13;
        data[3] = 0x45;
        data[4] = 0x66;
        data[5] = 0x66;
        data[6] = 0x66;
        data[7] = 0x66;
        for (int i = 8; i < 24; i++) {
            data[i] = 0x00;
        }

        data[24] = 0xee;
        data[25] = 0xee;
        data[26] = 0xee;
        data[27] = 0xee;
        data[28] = 0xdc;
        data[29] = 0xba;
        data[30] = 0x9b;
        data[31] = 0xcd;
        board = data;
    }

    private Chessboard(ChessboardData data) {
        board = data;
    }

    public readonly (ChessPiece? Piece, bool Color) CheckSquare(int index) {
        if (index < 0 || index > 63) throw new ArgumentException("Chess index is out of bounds.");
        int arrayIndex = index / 2;
        if (index % 2 == 0) {
            return GetPiece(ByteAdjustment.LeftHalf(board[arrayIndex]));
        } else {
            return GetPiece(ByteAdjustment.RightHalf(board[arrayIndex]));
        }
    }

    private static (ChessPiece? Piece, bool Color) GetPiece(byte piece) {
        int checks = piece & 0x07;
        bool color = (piece & 0x08) == 0;
        return checks switch {
            1 => (ChessPiece.King, color),
            2 => (ChessPiece.Queen, color),
            3 => (ChessPiece.Bishop, color),
            4 => (ChessPiece.Knight, color),
            5 => (ChessPiece.Rook, color),
            6 => (ChessPiece.Pawn, color),
            _ => (null, false)
        };
    }

    public static bool operator !=(Chessboard left, Chessboard right) {
        return !left.Equals(right);
    }

    public static bool operator ==(Chessboard left, Chessboard right) {
        return left.Equals(right);
    }

    public readonly override bool Equals(object? obj) {
        if (obj is not null && obj is Chessboard)
            return Equals(obj);
        return false;
    }

    public readonly override int GetHashCode() {
        return board.GetHashCode();
    }

    public readonly bool Equals(Chessboard other) {
        for (int i = 0; i < 32; i++) {
            if (board[i] != other.board[i]) return false;
        }

        return true;
    }
}
