using System.Collections.Generic;
using MainScene;
using Script;
using SimulationSettingScripts;
using StorageSimulateScripts;
using UnitCodes;
using UnitCodes.LifterCodes;
using UnityEngine;

public static class Hub
{
    private static List<StorageBlueprint> storageSettings;
    public static List<StorageBlueprint> StorageBlueprints
    {
        get
        {
            if (storageSettings != null) return storageSettings;
            storageSettings = new();
            return storageSettings;

        }
    }

    private static List<LifterSetting> lifterSettings;
    public static List<LifterSetting> LifterSettings
    {
        get
        {
            if (lifterSettings == null)
            {
                lifterSettings = new();
                return lifterSettings;
            }

            return lifterSettings;
        }
    }
    
    public static MainManager MainManager => MainManager.instance;
    public static Lifter LifterPrefab => MainManager.lifterPrefab;
    public static Storage StoragePrefab => MainManager.storagePrefab;
    public static Dispenser DispenserPrefab => MainManager.dispenserPrefab;

    public static SimulationSettingManager SimulationSettingManager => SimulationSettingManager.instance;
    public static List<StorageBlueprint> ToSimulateList => SimulationSettingManager.smList;
    
    public static SimulateManager SimulateManager => SimulateManager.instance;
    public static ScriptInvoker ScriptInvoker => SimulateManager.ScriptInvoker;
    public static List<Storage> Storages => SimulateManager.storages;
    public static float ProcessTime => SimulateManager.GetProcessTime;
    
    public static Vector3 ClearVector(Vector3 input)
        => new(Mathf.Floor(input.x) + 0.5f, 0f, Mathf.Floor(input.z) + 0.5f);
}