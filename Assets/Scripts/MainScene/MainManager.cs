using UnitCodes;
using UnitCodes.LifterCodes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainScene
{
public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    
    public Dispenser dispenserPrefab;
    public Lifter lifterPrefab;
    public Storage storagePrefab;

    void Start()
    {
        instance = this;
    }

    public void Simulate()
    {
        SceneManager.LoadScene("SimulateSetting");
    }
    
    public void SetStorage()
    {
        SceneManager.LoadScene("StorageSetting");
    }

    public void SetLifter()
    {
        SceneManager.LoadScene("LifterSetting");
    }
}
}
