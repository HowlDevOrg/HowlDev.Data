using System.Diagnostics;
using HowlDev.Data.Structures.Games.Chess;

namespace HowlDev.Data.Structures.Tests.Chess;

public class ChessHelpersRowColTests {
    [Test]
    [Arguments(1, 1, 0)]
    [Arguments(1, 8, 7)]
    [Arguments(2, 1, 8)]
    [Arguments(2, 8, 15)]
    [Arguments(3, 1, 16)]
    [Arguments(3, 8, 23)]
    [Arguments(8, 1, 56)]
    [Arguments(8, 8, 63)]
    public async Task RowColConvertsCorrectly(int row, int col, int exp) {
        await Assert.That(ChessHelpers.RowColToIndex(row, col)).IsEqualTo(exp);
    }

    [Test]
    [Arguments(8, 0)]
    [Arguments(9, 8)]
    [Arguments(0, 1)]
    [Arguments(7, 9)]
    public async Task RowColThrowsErrors(int row, int col) {
        await Assert.That(() => ChessHelpers.RowColToIndex(row, col)).Throws<InvalidDataException>();
    }

    [Test]
    [Arguments(0, 1, 1)]
    [Arguments(7, 1, 8)]
    [Arguments(8, 2, 1)]
    [Arguments(15, 2, 8)]
    [Arguments(16, 3, 1)]
    [Arguments(23, 3, 8)]
    [Arguments(56, 8, 1)]
    [Arguments(63, 8, 8)]
    public async Task IndexConvertsCorrectly(int index, int expRow, int expCol) {
        await Assert.That(ChessHelpers.IndexToRowCol(index)).IsEqualTo((expRow, expCol));
    }

    [Test]
    [Arguments(64)]
    [Arguments(-1)]
    [Arguments(6500)]
    public async Task IndexThrowsErrors(int index) {
        await Assert.That(() => ChessHelpers.IndexToRowCol(index)).Throws<InvalidDataException>();
    }

    [Test]
    [MethodDataSource(nameof(IndexSource))]
    public async Task IndexToRowColAndBack(int index) {
        (int row, int col) = ChessHelpers.IndexToRowCol(index);
        await Assert.That(ChessHelpers.RowColToIndex(row, col)).IsEqualTo(index);
    }

    public static IEnumerable<Func<int>> IndexSource() {
        for (int i = 0; i < 64; i++) {
            yield return () => i;
        }
    }
}
public class ChessHelpersCharIndexTests {
    [Test]
    [MethodDataSource(nameof(ChessPositionSource))]
    public async Task RowColConvertsCorrectly(string val, int exp) {
        Debug.Assert(exp > -1 && exp < 64);
        await Assert.That(ChessHelpers.CharToIndex(val)).IsEqualTo(exp);
    }

    public static IEnumerable<Func<(string, int)>> ChessPositionSource() {
        int i = 0;
        char[] col = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'];
        for (int j = 1; j <= 8; j++) {
            foreach (char item in col) {
                yield return () => (item + j.ToString(), i);
                i++;
            }
        }
    }
}
