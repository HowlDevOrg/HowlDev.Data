using System.Runtime.CompilerServices;
namespace HowlDev.Data.Structures.Games;

public static class ByteAdjustment {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte RightHalf(byte value) {
        return (byte)(value & 0x0f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte LeftHalf(byte value) {
        return (byte)(value >> 4);
    }
}
