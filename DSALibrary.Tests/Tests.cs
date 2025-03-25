using DSALibrary.Dependency;
using System.Threading.Tasks;
using TUnit.Assertions.AssertConditions.Throws;

namespace DSALibrary.Tests;

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

        await Assert.That(d.DependentValues(1)).IsEqualTo(new List<int>() { 2 });
    }
}