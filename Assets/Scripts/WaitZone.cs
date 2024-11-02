using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
public class WaitZone
{
    private Queue<WaitPoint> _waitPoints = new();

    public void AddWaitPoint(WaitPoint waitPoint) => _waitPoints.Enqueue(waitPoint);
    public Vector3 GetWaitPoint() => _waitPoints.Dequeue().pos;
}
}