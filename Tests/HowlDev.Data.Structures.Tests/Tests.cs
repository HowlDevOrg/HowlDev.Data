using DSALibrary.Dependency;
using System.Threading.Tasks;
using TUnit.Assertions.AssertConditions.Throws;

namespace HowlDev.Data.Structures.Tests;

public class Tests {
    [Test]
    public async Task CanCreateDependency() {
        DependencyChecking<int> d = new DependencyChecking<int>([]);
        await Assert.That(true).IsEqualTo(true);
    }

    [Test]
    public async Task CanCreateSingleDependency() {
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>> {
            { 1, new List<int>() }
        };
        DependencyChecking<int> d = new DependencyChecking<int>(dict);

        await Assert.That(d.Count).IsEqualTo(1);
    }

    [Test]
    public async Task CanCreateTwoDependencies() {
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>> {
            { 1, new List<int>() },
            { 2, new List<int>() { 1 } }
        };

        DependencyChecking<int> d = new DependencyChecking<int>(dict);

        await Assert.That(d.Count).IsEqualTo(2);
    }

    [Test]
    public async Task GeneratingDuplicateKeyThrowsException() {
        await Assert.That(() =>
        {
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>> {
                { 1, new List<int>() },
                { 1, new List<int>() { 1 } }
            };

            DependencyChecking<int> d = new DependencyChecking<int>(dict);
        })
        .ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task CanGetMaximumDepth1() {
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>> {
            { 1, new List<int>() },
            { 2, new List<int>() { 1 } }
        };

        DependencyChecking<int> d = new DependencyChecking<int>(dict);

        await Assert.That(d.MaximumDepth).IsEqualTo(1);
    }


    [Test]
    public async Task CanGetMaximumDepth2() {
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>> {
            { 1, new List<int>() },
            { 2, new List<int>() { 1 } },
            { 3, new List<int>() { 2 } }
        };

        DependencyChecking<int> d = new DependencyChecking<int>(dict);

        await Assert.That(d.MaximumDepth).IsEqualTo(2);
    }

    [Test]
    public async Task CanGetMaximumDepth3() {
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>> {
            { 1, new List<int>() },
            { 2, new List<int>() { 1 } },
            { 3, new List<int>() { 1 } }
        };

        DependencyChecking<int> d = new DependencyChecking<int>(dict);

        await Assert.That(d.MaximumDepth).IsEqualTo(1);
    }

    [Test]
    public async Task CanGetDependentValues1() {
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>> {
            { 1, new List<int>() },
            { 2, new List<int>() { 1 } }
        };

        DependencyChecking<int> d = new DependencyChecking<int>(dict);

        await Assert.That(d.DependentValues(1)).IsEquivalentTo(new List<int>() { 2 });
    }

    [Test]
    public async Task CanGetDependentValues2() {
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>> {
            { 1, new List<int>() },
            { 2, new List<int>() { 1 } },
            { 3, new List<int>() { 1 } }
        };

        DependencyChecking<int> d = new DependencyChecking<int>(dict);

        await Assert.That(d.DependentValues(1)).IsEquivalentTo(new List<int>() { 2, 3 });
    }

    [Test]
    public async Task CanGetDependentValues3() {
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>> {
            { 1, new List<int>() },
            { 2, new List<int>() { 1 } },
            { 3, new List<int>() { 2 } }
        };

        DependencyChecking<int> d = new DependencyChecking<int>(dict);

        await Assert.That(d.DependentValues(1)).IsEquivalentTo(new List<int>() { 2 });
    }

    //[Test]
    //public async Task CanGetMaximumDepth4() {
    //    Dictionary<int, List<int>> dict = new Dictionary<int, List<int>> {
    //        { 1, new List<int>() },
    //        { 2, new List<int>() },
    //        { 3, new List<int>() { 1, 2 } }
    //    };

    //    DependencyChecking<int> d = new DependencyChecking<int>(dict);

    //    await Assert.That(d.MaximumDepth).IsEqualTo(1);
    //}
}
public class DependencySpecificDepthTests {
    [Test]
    public async Task CanGetIndependentDepth1() {
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>> {
            { 1, new List<int>() },
            { 2, new List<int>() { 1 } },
            { 3, new List<int>() { 2 } }
        };

        DependencyChecking<int> d = new DependencyChecking<int>(dict);

        await Assert.That(d.Depth(1)).IsEquivalentTo(0);
        await Assert.That(d.Depth(2)).IsEquivalentTo(1);
        await Assert.That(d.Depth(3)).IsEquivalentTo(2);
    }

    [Test]
    public async Task CanGetIndependentDepth2() {
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>> {
            { 1, new List<int>() },
            { 2, new List<int>() { 1 } },
            { 3, new List<int>() { 2 } },
            { 4, new List<int>() { 2, 3 } }
        };

        DependencyChecking<int> d = new DependencyChecking<int>(dict);

        await Assert.That(d.Depth(4)).IsEquivalentTo(3);
    }

    [Test]
    public async Task CanGetIndependentDepth3() {
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>> {
            { 1, new List<int>() },
            { 2, new List<int>() { 1 } },
            { 3, new List<int>() { 2 } },
            { 4, new List<int>() { 2, 3 } }
        };

        DependencyChecking<int> d = new DependencyChecking<int>(dict);

        await Assert.That(d.Depth(4)).IsEquivalentTo(3);
    }
}