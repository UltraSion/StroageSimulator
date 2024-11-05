using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SimulationSettingScripts
{
public class SimulationSettingManager : MonoBehaviour
{
    public static SimulationSettingManager instance;
    
    public GameObject blueprintContent;
    public GameObject toSimulateContent;
    public GameObject button;

    public List<StorageBlueprint> smList;

    public void Start()
    {
        if (instance == null) instance = this;
        
        AddBlueprint(StorageBlueprint.Default);
        AddBlueprint(StorageBlueprint.Default);

        var storageBlueprints = Hub.StorageBlueprints;
        foreach (var bp in storageBlueprints)
        {
            AddBlueprint(bp);
        }
        
        smList = new();

    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Simulate()
    {
        SceneManager.LoadScene("SimulateScene");
    }

    public void AddBlueprint(StorageBlueprint bp)
    {
        var b = Instantiate(button, blueprintContent.transform).GetComponent<ScrollViewButton>();
        b.SetName(bp.storageName);
        b.AddEvent(() =>
        {
            AddSimulate(bp);
            Destroy(b.gameObject);
        });
    }

    public void AddSimulate(StorageBlueprint bp)
    {
        var b = Instantiate(button, toSimulateContent.transform).GetComponent<ScrollViewButton>();
        b.SetName(bp.storageName);
        b.AddEvent(() =>
        {
            AddBlueprint(bp);
            smList.Remove(bp);
            Destroy(b.gameObject);
        });
        smList.Add(bp);
    }
}
}