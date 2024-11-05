using System;
using System.Collections.Generic;
using System.Linq;

public class InsertSortQueue<T>
{
    private readonly Func<T, T, bool> compare;

    private readonly LinkedList<T> list = new();

    public int Count => list.Count;
    
    public InsertSortQueue(Func<T, T, bool> comp) => compare = comp;

    public List<T> ToList() => list.ToList();

    public void Enqueue(T item) {
        var node = list.First;
        while (node != default) {
            if (compare(item, node.Value)) break;
            node = node.Next;
        }

        if (node == default) list.AddLast(item);
        else list.AddBefore(node, new LinkedListNode<T>(item));
    }

    public T Dequeue() {
        var item = list.First.Value;
        list.RemoveFirst();
        return item;
    }

    public bool Contains(T t) => list.Contains(t);
}