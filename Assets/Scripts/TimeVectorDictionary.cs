using System.Collections.Generic;
using UnityEngine;

public class TimeVectorDictionary<T>
{
    private Dictionary<int, VectorDictionary<T>> _timeVectorDictionary = new Dictionary<int, VectorDictionary<T>>();

    public VectorDictionary<T> GetValueOrDefaultAtTime(float time)
    {
        int t = (int)time;
        return _timeVectorDictionary.GetValueOrDefault(t);
    }

    public void AddValue(float time, Vector3 position, T value)
    {
        int t = (int)time;
        if (!_timeVectorDictionary.TryGetValue(t, out var dict))
        {
            dict = new();
            _timeVectorDictionary.Add(t, dict);
        }
        
        dict.AddValue(position, value);
    }

    public T GetValueOrDefault(float time, Vector3 position)
    {
        var dict = GetValueOrDefaultAtTime(time);
        if (dict == null) return default;

        return dict.GetValueOrDefault(position);
    }
}