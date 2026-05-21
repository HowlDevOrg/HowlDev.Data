using System.Diagnostics;
using HowlDev.Data.Structures.Games.Chess;

namespace HowlDev.Data.Structures.Tests.Chess;

public class ChessHelpersRowColTests {
    [Test]
    [Arguments(8, 1, 0)]
    [Arguments(8, 8, 7)]
    [Arguments(7, 1, 8)]
    [Arguments(7, 8, 15)]
    [Arguments(6, 1, 16)]
    [Arguments(6, 8, 23)]
    [Arguments(1, 1, 56)]
    [Arguments(1, 8, 63)]
    public async Task RowColConvertsCorrectly(int row, int col, int exp) {
        await Assert.That(ChessHelpers.RowColToIndex(row, col)).IsEqualTo(exp);
    }

    [Test]
    [Arguments(0, 8, 1)]
    [Arguments(7, 8, 8)]
    [Arguments(8, 7, 1)]
    [Arguments(15, 7, 8)]
    [Arguments(16, 6, 1)]
    [Arguments(23, 6, 8)]
    [Arguments(56, 1, 1)]
    [Arguments(63, 1, 8)]
    public async Task IndexConvertsCorrectly(int index, int expRow, int expCol) {
        await Assert.That(ChessHelpers.IndexToRowCol(index)).IsEqualTo((expRow, expCol));
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
        for (int j = 8; j >= 1; j--) {
            foreach (char item in col) {
                yield return () => (item + j.ToString(), i);
                i++;
            }
        }
    }
}
