using System;
using System.Collections.Generic;
using PathCodes;
using Script;
using UnityEngine;

public class PathTimeTable
{
    private Dictionary<int, VectorDictionary<Path>> timeTable = new();

    private VectorDictionary<Path> getDictionary(float time)
    {
        if (!timeTable.TryGetValue((int)time, out var value))
        {
            value = new VectorDictionary<Path>();
            timeTable.Add((int)time, value);
        }

        return value;
    }

    public bool CanPass(int time, Vector3 position)
    {
        if (!timeTable.TryGetValue(time, out var value))
            return true;

        if (!value.TryGetValue(position, out var path))
        {
            return true;
        }

        return false;
    }

    public void AddPath(Path path)
    {
        int t1 = (int)path.StartTime;
        int t2 = (int)path.EndTime;
        
        for (int t = t1; t <= t2; t++)
        {
            var dict = getDictionary(t);
            Vector3 p1 = path.GetPositionAtTime(t);
            Vector3 p2;

            if (t == t2)
            {
                p2 = path.dest;
            }
            else
            {
                p2 = path.GetPositionAtTime(t + 1);
            }

            int xMin = (int)Mathf.Min(p1.x, p2.x);
            int xMax = (int)Mathf.Max(p1.x, p2.x);
            int zMin = (int)Mathf.Min(p1.z, p2.z);
            int zMax = (int)Mathf.Max(p1.z, p2.z);

            for (int x = xMin; x <= xMax; x++)
            {
                for (int z = zMin; z < zMax; z++)
                {
                    Vector3 k3 = new Vector3(x, 0f, z);
                    dict.AddValue(k3, path);
                }
            }
        }
    }

    public void Optimize()
    {
        float currTime = Hub.ProcessTime;

        List<int> garbageTime = new();
        foreach (var t in timeTable)
        {
            int time = t.Key;
            if (time < currTime)
                garbageTime.Add(time);
        }

        foreach (int time in garbageTime)
        {
            timeTable.Remove(time);
        }
    }
}