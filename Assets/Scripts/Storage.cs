using System.Collections.Generic;
using DefaultNamespace;
using PathCodes;
using Script;
using UnitCodes.LifterCodes;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public PathMap PathMap = new();
    private bool[,] obstacles;

    public LifterManager LifterManager = new();
    public Vector3 offset => transform.position;
    public WaitZone WaitZone = new();
    public StorageSetting StorageSetting;
    public List<Dispenser> Dispensers;

    public Path GetToDispenserPath(Vector3 position)
    {
        float minTime = float.MaxValue;
        Path minPath = null;
        foreach (Dispenser dispenser in Dispensers)
        {
            Vector3 output = dispenser.outputPos;
            Path path = PathMap.GetPath(position, output);
            float time = path.Time;
            if (minTime > time)
            {
                minTime = time;
                minPath = path;
            }
        }

        return minPath;
    }
    
    void Start()
    {
        transform.localScale = StorageSetting.Scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
