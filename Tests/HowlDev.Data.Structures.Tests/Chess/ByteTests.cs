using HowlDev.Data.Structures.Games;
namespace HowlDev.Data.Structures.Tests.Chess;

public class ByteTests {
    [Test]
    [Arguments(0xff, 0x0f)]
    [Arguments(0xfa, 0x0a)]
    [Arguments(0x4a, 0x0a)]
    [Arguments(0x90, 0x00)]
    [Arguments(0xa3, 0x03)]
    public async Task CanGetRightHalf(byte input, byte exp) {
        await Assert.That(ByteAdjustment.RightHalf(input)).IsEqualTo(exp);
    }

    [Test]
    [Arguments(0xff, 0x0f)]
    [Arguments(0xfa, 0x0f)]
    [Arguments(0x4a, 0x04)]
    [Arguments(0x90, 0x09)]
    [Arguments(0xa3, 0x0a)]
    public async Task CanGetLeftHalf(byte input, byte exp) {
        await Assert.That(ByteAdjustment.LeftHalf(input)).IsEqualTo(exp);
    }
}
