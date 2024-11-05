using System.Collections.Generic;
using System.Linq;
using Script;
using UnitCodes.LifterCodes;
using UnityEngine;

namespace PathCodes
{
public class JPSPathFinding
{
    public LifterSetting _lifterSetting;
    private Storage _storage;

    public JPSPathFinding(LifterSetting _lifterSetting, Storage _storage)
    {
        this._storage = _storage;
        this._lifterSetting = _lifterSetting;
    }

    private class JPSNode
    {
        public Path path;

        public int time;

        public Vector3 position;
        public Vector3 direction => path.MoveDirection;

        public float gCost => path.EndTime;
        public float hCost;

        public float fCost => gCost + hCost;

        public JPSNode(Vector3 position, int time)
        {
            this.position = position;

            this.time = time;
        }

        public void Set(Path path, float hCost)
        {
            this.path = path;
            this.hCost = hCost;
        }

        public JPSNode parent;

        public override int GetHashCode()
        {
            return new Vector3Int((int)position.x, time, (int)position.z).GetHashCode();
        }
    }

    private Vector3[] directions = new Vector3[]
    {
        new Vector3(1f, 0f, 0f),
        new Vector3(0f, 0f, -1f),
        new Vector3(-1f, 0f, 0f),
        new Vector3(0f, 0f, 1f)
    };

    public List<Path> FindPath(Vector3 from, Vector3 dest, Vector3 startDirection)
    {
        PathTimeTable storagePathTimeTable = _storage.PathTimeTable;

        InsertSortQueue<JPSNode> openList = new InsertSortQueue<JPSNode>((a, b) => a.fCost < b.fCost);
        HashSet<JPSNode> closedList = new HashSet<JPSNode>();

        float cTime = Hub.ProcessTime;
        Path startPath = new Path(
            from,
            startDirection,
            cTime,
            _lifterSetting
        ).CompletePath(from);

        float hCost = _lifterSetting.GetTimeAccelAndStop(from, dest);
        JPSNode startNode = new JPSNode(from, 0);
        startNode.Set(startPath, hCost);
        openList.Enqueue(startNode);

        while (openList.Count > 0)
        {
            JPSNode currentNode = openList.Dequeue();

            if (currentNode.position == dest) return RetraceNode(currentNode);
            
            closedList.Add(currentNode);
            for (int i = 0; i < directions.Length; i++)
            {
                Vector3 direction = directions[i];
                Vector3 currPos = currentNode.position + direction;

                int currTime = currentNode.time;
                while (CanPass(currTime, currPos))
                {
                    currTime = currentNode.time +
                               Mathf.CeilToInt(
                                   _lifterSetting.GetTimeAccelAndStop(
                                       currentNode.position, 
                                       currPos)
                                   );

                    JPSNode newNode = new JPSNode(currPos, currTime);
                    if (closedList.Contains(newNode))
                        continue; //break?

                    Path newPath = currentNode.path.CompletePath(currPos);
                    hCost = _lifterSetting.GetTimeAccelAndStop(currPos, dest);
                    newNode.Set(newPath, hCost);
                    newNode.parent = currentNode;
                    
                    openList.Enqueue(newNode);
                    
                    currPos += direction;
                }
            }
            //제자리에서 기다리는 노드 추가
        }

        return null;
    }

    private bool CanPass(int time, Vector3 position)
    {
        //float maxT = 120f;
        //if(time > matT)
        //  return false;
        int maxX = (int)_storage.transform.localScale.x;
        int maxZ = (int)_storage.transform.localScale.z;

        if (position.x > maxX) return false;
        if (position.z > maxZ) return false;
        return _storage.PathTimeTable.CanPass(time, position);
    }

    private List<Path> RetraceNode(JPSNode node)
    {
        List<Path> result = new();
        JPSNode temp = node;
        while (temp != null)
        {
            result.Add(temp.path);
            temp = temp.parent;
        }

        result.Reverse();
        return result;
    }
}
}