using System.Collections;

namespace Oreru.Domain.Containers;
public class SortedList<T> : ICollection<T>
{
    private readonly List<T> _items = [];
    private readonly IComparer<T> _comparer;

    public int Count => _items.Count;
    public bool IsReadOnly => false;

    public SortedList() => _comparer = Comparer<T>.Default;

    public SortedList(IComparer<T> comparer) => _comparer = comparer;

    public void Add(T item) => _items.Insert(_items.BinarySearch(item), item);

    public void AddRange(IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            Add(item);
        }
    }

    public void Clear() => _items.Clear();

    public bool Contains(T item) => _items.IndexOf(item) >= 0;

    public void CopyTo(T[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

    public bool Remove(T item) => _items.Remove(item);
    
    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

    public int IndexOf(T item)
    {
        var index = _items.BinarySearch(item, _comparer);
        return index >= 0 && _comparer.Compare(_items[index], item) == 0 ? index : -1;
    }

    public T this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }
}
