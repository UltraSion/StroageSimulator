using System.Collections.Generic;
using Script;
using SimulationSettingScripts;
using UnityEngine;
using UnityEngine.UI;

namespace StorageSimulateScripts
{
public class SimulateManager : MonoBehaviour
{
    public static SimulateManager instance;

    public static ScriptInvoker ScriptInvoker = new();

    public List<Storage> storages;
    public int currStorage = 0;

    private float time;

    public float GetProcessTime => time;

    public GameObject cameraPole;
    
    public GameObject simulListContent;
    public GameObject button;
    
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null) 
            instance = this;
        
        storages = new();
        BuildStorage();
        MoveCameraPoleTo(storages[0]);
    }

    // Update is called once per frame
    void Update()
    {
        ScriptInvoker.ExecuteCommands();
        time += Time.deltaTime;
    }

    public void Init()
    {
        time = 0;
    }

    public void BuildStorage()
    {
        var storagePrefab = Hub.StoragePrefab;
        var toBuildList = Hub.ToSimulateList;

        for (int i = 0; i < toBuildList.Count; i++)
        {
            StorageBlueprint sbp = toBuildList[i];
            Storage storage = Instantiate(storagePrefab);
            storage.transform.localScale = sbp.Scale;

            Vector3 storagePosition = new Vector3(i * 1000f, 0f, 0f);
            storage.transform.position = storagePosition - new Vector3(0.5f, 0f, 0.5f);

            foreach (var bp in sbp.toPlaces)
            {
                Instantiate(bp.prefab, bp.position + storagePosition, bp.rotation);
            }

            var b = Instantiate(button, simulListContent.transform).GetComponent<ScrollViewButton>();
            b.SetName(sbp.storageName);
            b.AddEvent(() => MoveCameraPoleTo(storage));
            storages.Add(storage);
        }
    }

    public void MoveCameraPoleTo(Storage st)
    {
        Vector3 storagePosition = st.transform.position;
        Vector3 storageScale = st.transform.localScale;

        float poleLength = storageScale.x / 2 * 1.414f;
        Vector3 polePos = storagePosition + new Vector3(storageScale.x / 2, 0f, storageScale.z / 2);

        cameraPole.transform.position = polePos;
        cameraPole.transform.localScale = new Vector3(1f, 1f, poleLength);
        cameraPole.transform.rotation = Quaternion.Euler(45f, 0f, 0f);
    }
}
}
