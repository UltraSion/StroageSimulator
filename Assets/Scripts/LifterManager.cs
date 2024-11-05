using System.Collections.Generic;
using Script;
using UnityEngine;

namespace UnitCodes.LifterCodes
{
public class LifterManager
{
    private List<Lifter> _workingLifters;
    private List<Lifter> _waitingLifters;
    private LifterSetting _lifterSetting;
    
    public void AddLifter(Vector3 position, Storage storage)
    {
        Lifter lifterPrefab = Hub.LifterPrefab;
        var lifter = GameObject.Instantiate(lifterPrefab, position, Quaternion.identity);
        lifter.allocatedStorage = storage;
        _waitingLifters.Add(lifter);
    }
}
}