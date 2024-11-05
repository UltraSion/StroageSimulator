using System.Collections.Generic;
using PathCodes;
using Script;
using UnitCodes;
using UnitCodes.LifterCodes;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public PathTimeTable PathTimeTable = new();
    public LifterManager LifterManager = new();
    public Vector3 offset => transform.position;
    public List<Dispenser> Dispensers;
    public LifterSetting LifterSetting;
    
    void Start()
    {
    }

    public void Init()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
