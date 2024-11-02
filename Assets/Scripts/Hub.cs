using System.Collections.Generic;
using Script.Units.Script;
using UnitCodes.LifterCodes;
using UnityEngine;

namespace Script
{
public static class Hub
{
    public static FactoryManager FactoryManager => FactoryManager.instance;
    public static ScriptInvoker ScriptInvoker => FactoryManager.ScriptInvoker;
    public static List<Storage> Storages => FactoryManager.storages;

    public static Lifter LifterPrefab = FactoryManager.lifterPrefab;
    public static Storage StoragePrefab = FactoryManager.storagePrefab;

    public static Vector3 ClearVector(Vector3 input) 
        => new(Mathf.Floor(input.x) + 0.5f, 0f, Mathf.Floor(input.z) + 0.5f);
}
}