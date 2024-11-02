using System;
using System.Collections.Generic;
using PathCodes;
using Script.DataStructDef;
using UnitCodes.LifterCodes;
using UnityEngine;

public class Node
{
    public int x, z;
    public int gCost; // 시작점에서 이 노드까지의 비용
    public int hCost; // 목표점까지의 추정 비용
    public int fCost { get { return gCost + hCost; } } // g + h
    public int time;
    public Node parent; // 부모 노드를 추적

    public Node(int x, int z)
    {
        this.x = x;
        this.z = z;
        this.gCost = int.MaxValue; // 초기 비용을 매우 크게 설정
        this.hCost = 0;
        this.parent = null;
        this.time = 0;
    }
}

public class AStar
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

    public AStar(Storage storage)
    {
        this._storage = storage;
        this.pathMap = storage.PathMap;
    }

    public List<Node> FindPath(LifterSetting setting, Vector3 dest, Node start, Node goal)
    {
        InsertSortQueue<Node> toOpenList = new InsertSortQueue<Node>((a, b) => a.fCost < b.fCost); // 우선순위 큐
        HashSet<Node> visitedList = new HashSet<Node>();

        start.gCost = 0;
        start.hCost = GetManhattanDistance(start, goal);
        toOpenList.Enqueue(start);

        while (toOpenList.Count > 0)
        {
            Node currentNode = toOpenList.Dequeue();

            if (currentNode.x == goal.x && currentNode.z == goal.z)
            {
                return RetracePath(start, goal);
            }

            visitedList.Add(currentNode);

            for (int i = 0; i < directions.Length; i++)
            {
                Vector3 neighborPos = new Vector3(currentNode.x, 0f, currentNode.z) + directions[i]; 

                if (!IsInBounds(neighborPos) || pathMap.obstacles[(int)(neighborPos.x), (int)neighborPos.z])
                    continue;

                Node neighbor = new Node((int)(neighborPos.x), (int)neighborPos.z);

                if (visitedList.Contains(neighbor))
                    continue;

                int newGCost = currentNode.gCost + GetManhattanDistance(currentNode, neighbor);

                if (newGCost < neighbor.gCost)
                {
                    neighbor.gCost = newGCost;
                    neighbor.hCost = GetManhattanDistance(neighbor, goal);
                    neighbor.parent = currentNode;

                    if (!toOpenList.Contains(neighbor))
                    {
                        toOpenList.Enqueue(neighbor);
                    }
                }
            }
        }

        return null; // 경로를 찾을 수 없을 때
    }

    private List<Node> RetracePath(Node start, Node goal)
    {
        List<Node> path = new List<Node>();
        Node currentNode = goal;

        while (currentNode.parent != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    private int GetManhattanDistance(Node a, Node b)
    {
        return Math.Abs(a.x - b.x) + Math.Abs(a.z - b.z);
    }

    private bool IsInBounds(Vector3 position)
    {
        float width = _storage.StorageSetting.width;
        float height = _storage.StorageSetting.height;
        
        return position.x >= 0 && position.x < width && position.z >= 0 && position.z < height;
    }
}

