using System;

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
}
