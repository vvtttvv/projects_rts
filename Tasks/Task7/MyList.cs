using System.Collections;

namespace Task7;

public class MyList<T> : IEnumerable<T>
{
    private int _length;
    private T[] _items;
    
    public MyList()
    {
        _items = new T[4];
    }
    public MyList(int capacity)
    {
        _items = new T[capacity];
    }

    public MyList(List<T> collection)
    {
        _items = new T[collection.Count];
        foreach (var el in collection)
        {
            Add(el);
        }
    }
    
    // It is in task, but should it really be?
    public int Capacity => _items.Length;
    public int Length => _length;

    public void Add(T el)
    {
        if (Length < _items.Length)
        {
            _items[Length] = el;
        }
        else
        {
            Resize();
            _items[Length] = el;
        }
        _length++;
    }
    
    private void Resize()
    {
        T[] newItems = new T[_items.Length * 2];
        Array.Copy(_items, newItems, _length);
        _items = newItems;
    }
    
    public void AddRange(IEnumerable<T> collection)
    {
        foreach (var el in collection)
            Add(el);
    }

    public void Clear()
    {
        _items = new T[4];
        _length = 0;
    }

    public int IndexOf(T item)
    {
        for (int i = 0; i < _length; i++)
            if (Equals(_items[i], item)) return i;
        return -1;
    }

    public void Remove(int index)
    {
        if (index < 0 || index >= _length)
            Console.WriteLine("You made a mistake, please try again");
        else
        {
            for (int i = index; i < _length - 1; i++)
                _items[i] = _items[i + 1];
            _length--;
        }
    }

    public void RemoveAll(T item)
    {
        int i = 0;
        while (i < _length)
        {
            if (Equals(_items[i], item)) Remove(i);
            else i++;
        }
    }

    public void Reverse()
    {
        for (int i = 0; i < _length / 2; i++)
            (_items[i], _items[_length - 1 - i]) = (_items[_length - 1 - i], _items[i]);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        for (int i = 0; i < Length; i++)
        {
            yield return _items[i];
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<T>)this).GetEnumerator();
    }
}