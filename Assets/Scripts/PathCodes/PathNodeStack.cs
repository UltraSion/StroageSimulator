using System;
using System.Collections.Generic;
using UnitCodes.LifterCodes;

namespace PathCodes
{
public class PathNodeStack
{
    private List<PathNode> PathNodes = new();

    public IEnumerator<PathNode> GetEnumerator() => PathNodes.GetEnumerator();

    public bool CanInsert(float time, float timeSize)
    {
        for (int i = 0; i < PathNodes.Count; i++)
        {
            if(PathNodes[i].leftTime < time) continue;
            if (PathNodes[i].arriveTime < time + timeSize) return false;

            return true;
        }

        return true;
    }

    public float GetTime(int index)
    {
        if (index >= PathNodes.Count) return float.MaxValue;

        if (index == 0)
        {
            Lifter owner = PathNodes[0].Owner;
            return PathNodes[0].arriveTime;
        }
        
        return PathNodes[index + 1].arriveTime - PathNodes[index].leftTime;
    }
    
    public int GetWaitingNum(PathNode toInsertNode)
    {
        float requiredTime = toInsertNode.leftTime - toInsertNode.arriveTime;

        int i;
        for (i = 0; i < PathNodes.Count; i++)
        {
            float gapTime = GetTime(i);
            if (gapTime >= requiredTime)
                return i;
        }

        return i;
    }

    public void AddPathNode(int index, PathNode pathNode)
    {
        PathNodes.Insert(index, pathNode);
    }

    public void DeletePathNode(PathNode pathNode)
    {
        PathNodes.Remove(pathNode);
    }
}
}