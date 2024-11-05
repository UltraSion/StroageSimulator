using StorageSettingScripts.MouseControl;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StorageSettingScripts
{
public class StorageSettingManager : MonoBehaviour
{
    public static Storage Storage;

    public TextMeshProUGUI sizeText;

    public TMP_InputField wareHoueName;

    private IMouseState mouseState = new NeutralState();

    private StorageBlueprint _storageBlueprint;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        mouseState = mouseState.GetState();
        mouseState.Update();
        
        UpdateSizeText();
    }

    public void Init()
    {
        Storage = Instantiate(Hub.StoragePrefab, Vector3.zero, Quaternion.identity);
        _storageBlueprint = new StorageBlueprint();
    }

    public void UpdateSizeText()
    {git
        int width = (int)(Storage.transform.localScale.x);
        int height = (int)(Storage.transform.localScale.z);
        
        sizeText.text = "width: " + width + " height: " + height;

    }

    public void SetMouseSizeSettingMode()
    {
        mouseState = new StorageSizeSetterEnter();
    }

    public void SetMouseDispenserSettingMode()
    {
        mouseState = new DispenserSetterEnter(_storageBlueprint);
    }

    public void SetMouseLifterSettingMode()
    {
        mouseState = new LifterSetterEnter(_storageBlueprint);
    }

    public void EndStorageSetting()
    {
        _storageBlueprint.Scale = Storage.transform.localScale;
        _storageBlueprint.storageName = wareHoueName.text;
        
        Hub.StorageBlueprints.Add(_storageBlueprint);

        SceneManager.LoadScene("MainScene");
    }
    
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}
}
