using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace HowlDev.Data.Structures.Games.Chess;

internal static class Throw {
    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static void PawnDiagonalizedError() => throw new InvalidOperationException("Pawns can only take diagonally in a next-to row.");


    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static ChessPiece InvalidPieceError() => throw new InvalidDataException("Invalid piece argument.");

    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static KingStatus InvalidKingStatus(ReadOnlySpan<char> status) => throw new UnreachableException($"Unknown king status: {status}");

    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static void RowException() => throw new InvalidDataException($"Row must be between 1 and 8 inclusive.");

    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static void ColException() => throw new InvalidDataException($"Column must be between 1 and 8 inclusive.");

    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static void IndexException() => throw new InvalidDataException($"Index must be between 0 and 63 inclusive.");

    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static (ChessPiece, bool) InvalidPieceType(char piece) => throw new InvalidDataException($"Can't read piece type {piece}.");
}
