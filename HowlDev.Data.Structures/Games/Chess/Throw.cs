using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace HowlDev.Data.Structures.Games.Chess;

public static class Throw {
    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static void PawnDiagonalizedError() {
        throw new InvalidOperationException("Pawns can only take diagonally in a next-to row.");
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static ChessPiece InvalidPieceError() {
        throw new InvalidDataException("Invalid piece argument.");
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static KingStatus InvalidKingStatus(ReadOnlySpan<char> status) {
        throw new UnreachableException($"Unknown king status: {status}");
    }
}
