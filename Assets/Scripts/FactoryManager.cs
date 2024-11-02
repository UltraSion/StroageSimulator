using System.Collections;
using System.Collections.Generic;
using Script.Units.Script;
using UnitCodes.LifterCodes;
using UnityEngine;
using UnityEngine.Serialization;

public class FactoryManager : MonoBehaviour
{
    public static FactoryManager instance;

    public static ScriptInvoker ScriptInvoker = new();

    public List<Storage> storages;

    public Lifter lifterPrefab;

    public Storage storagePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        ScriptInvoker.ExecuteCommands();   
    }
}
