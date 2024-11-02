using System.Collections.Generic;
using Script;
using Script.DataStructDef;
using UnityEngine;

namespace PathCodes
{
public class Navigator
{
    
}

public abstract class NavigatorCommand
{
    public Vector3 direction;
}

public class AstarNode
{
    public readonly Vector3 position;
    public float currTime;
    public float ArriveTimeToGoal;
    public float fCost => currTime + ArriveTimeToGoal;
    public AstarNode parent;

    public AstarNode(Vector3 position)
    {
        this.position = position;
    }

    public override int GetHashCode()
    {
        return position.GetHashCode(); // 위치에 대한 해시코드 반환
    }
}

public class AStar2
{
    private Vector3[] directions = new Vector3[]
    {
        new Vector3(1f, 0f, 0f),
        new Vector3(0f, 0f, -1f),
        new Vector3(-1f, 0f, 0f),
        new Vector3(0f, 0f, 1f)
    };
    
    private Storage _storage;
    private PathMap pathMap;

    public AStar2(Storage storage)
    {
        this._storage = storage;
        this.pathMap = storage.PathMap;
    }

    public List<AstarNode> FindPath(Vector3 from, Vector3 dest, LifterSetting setting)
    {
        InsertSortQueue<AstarNode> toOpenList = new InsertSortQueue<AstarNode>((a, b) => a.fCost < b.fCost); // 우선순위 큐
        HashSet<AstarNode> visitedList = new HashSet<AstarNode>();

        AstarNode start = new AstarNode(from);
        AstarNode goal = new AstarNode(dest);

        start.currTime = 0;
}
}