using System;
using UnitCodes.LifterCodes;
using UnityEngine;

namespace PathCodes
{
public class PathNode
{
    public Lifter Owner;

    public readonly Vector3 position;
    
    public float arriveTime { get => throw new Exception(); }
    public float leftTime { get => throw new Exception(); }
    
    public PathNode(Vector3 position)
    {
        this.position = position;
    }
}
}