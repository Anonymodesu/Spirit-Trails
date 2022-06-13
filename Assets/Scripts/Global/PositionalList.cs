using System.Collections.Generic;
using System.Collections;
using System;

namespace Global {

// Defines a fixed-length list, initialised with default values
public class PositionalList<T> : IEnumerable<T> {
    private Func<int, T> defaultItemGenerator;
    private List<T> list;
    private HashSet<int> notableIndices;
    public T this[int i] {
        get => list[i];
        set {
            notableIndices.Add(i);
            list[i] = value;
        }
    }

    public PositionalList(Func<int, T> defaultItemGenerator, int length) {
        this.defaultItemGenerator = defaultItemGenerator;
        this.notableIndices = new HashSet<int>();
        this.list = new List<T>();
        for(int i = 0; i < length; i++) {
            this.list.Add(defaultItemGenerator(i));
        }
    }

    public PositionalList(T defaultItem, int length) : this((_) => defaultItem, length) {}

    public void Reset(int i) {
        list[i] = defaultItemGenerator(i);
        notableIndices.Remove(i);
    }

    public bool IsNotable(int i) => notableIndices.Contains(i);

    public IEnumerator<T> GetEnumerator() {
        foreach(int i in notableIndices) {
            yield return list[i];
        }
    }

    public IEnumerable<T> GetAll() {
        foreach(var item in list) {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}
}