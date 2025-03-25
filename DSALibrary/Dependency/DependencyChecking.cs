using System;
using System.Runtime.CompilerServices;

namespace DSALibrary.Dependency;

public class DependencyChecking<T> where T : IEquatable<T> {
    private readonly Dictionary<T, List<T>> values;

    public DependencyChecking(Dictionary<T, List<T>> values) {
        this.values = values;
    }

    public int Count { get => values.Count; }
    public int MaximumDepth {
        get {
            List<T> distinct = new();
            foreach (KeyValuePair<T, List<T>> pair in values) {
                foreach (T value in pair.Value) {
                    if (!distinct.Contains(value)) {
                        distinct.Add(value);
                    }
                }
            }
            return distinct.Count;
        }
    }

    public List<T> DependentValues(T value) {
        List<T> dependents = new();
        foreach (KeyValuePair<T, List<T>> pair in values) {
            if (pair.Value.Contains(value)) {
                dependents.Add(pair.Key);
            }
        }
        return dependents;
    }

    public int Depth(T value) {
        int max = 0;
        foreach (T dependent in values[value]) {
            int newDepth = 1 + Depth(dependent);
            if (max < newDepth) max = newDepth;
        }
        return max;
    }
}
