using System.Collections.Generic;
using Script;
using UnityEngine;

namespace PathCodes
{
public class PathMap
{
    private VectorDictionary<PathNodeStack> pathDictionary = new();
    
    public bool[,] obstacles;

    private PathNodeStack GetPathNodeStack(Vector3 position)
    {
        if (!pathDictionary.TryGetValue(position, out var value))
        {
            value = new();
            pathDictionary.AddValue(position, value);
        }

        return value;
    }
    
    public Path GetPath(Vector3 p1, Vector3 p2)
    {
        Vector3 pos1 = Hub.ClearVector(p1);
        Vector3 pos2 = Hub.ClearVector(p2);

    }

    public void AddPath(Path path)
    {
        foreach (PathNode node in path)
        {
            var nodeStack = GetPathNodeStack(node.position);
            nodeStack.AddPathNode(node);
        }
    }
}
}