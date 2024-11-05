using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VectorDictionary<T>

{
    private readonly Dictionary<Vector3Int, T> dict = new();

    private static Vector3Int Floor(Vector3 key)
        => new(Mathf.FloorToInt(key.x), 0, Mathf.FloorToInt(key.z));

    public void AddValue(Vector3 key, T value)
    {
        var tKey = Floor(key);
        // Debug.Log(tKey);

        dict.Add(tKey, value);
    }

    public void DeleteValue(Vector3 key)
    {
        var tKey = Floor(key);
        dict.Remove(tKey);
    }

    public T GetValueOrDefault(Vector3 key)
    {
        var tKey = Floor(key);
        return dict.GetValueOrDefault(tKey);
    }

    public bool TryGetValue(Vector3 key, out T value)
    {
        var tKey = Floor(key);
        return dict.TryGetValue(tKey, out value);
    }

    public List<KeyValuePair<Vector3Int, T>> ToList()
    {
        return dict.ToList();
    }
}