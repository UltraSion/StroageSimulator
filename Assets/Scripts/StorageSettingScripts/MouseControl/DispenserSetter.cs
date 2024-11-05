using UnitCodes;
using UnityEngine;

namespace StorageSettingScripts.MouseControl
{
public class DispenserSetterEnter : IMouseState
{
    private Dispenser dispenserPrefab;
    private StorageBlueprint _blueprint;

    public DispenserSetterEnter(StorageBlueprint blueprint)
    {
        this._blueprint = blueprint;
        Vector3 pos = ScreenToGroundPoint();
        dispenserPrefab = GameObject.Instantiate(Hub.DispenserPrefab, pos, Quaternion.identity);
    }
    
    public IMouseState GetState()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject.DestroyImmediate(dispenserPrefab.gameObject);
            return new NeutralState();
        }

        if (Input.GetMouseButtonDown(0))
            return new DispenserSetterWork(_blueprint, dispenserPrefab);

        return this;
    }

    public void Update()
    {
        dispenserPrefab.transform.position = ScreenToGroundPoint();
        
        if(Input.GetKeyDown(KeyCode.R)) 
            dispenserPrefab.transform.Rotate(new Vector3(0f, 90f, 0f));
    }
    
    public Vector3 ScreenToGroundPoint()
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = Mathf.Ceil(position.x) - 0.5f;
        float z = Mathf.Ceil(position.z) - 0.5f;
        Vector3 result = new Vector3(x, 0f, z);
        return result;
    }
}

public class DispenserSetterWork : IMouseState
{
    private Dispenser dispenserPrefab;
    private StorageBlueprint _storageBlueprint;
    
    public DispenserSetterWork(StorageBlueprint storageBlueprint, Dispenser dispenserPrefab)
    {
        this._storageBlueprint = storageBlueprint;
        this.dispenserPrefab = dispenserPrefab;
    }
    
    public IMouseState GetState()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var prefab = GameObject.Instantiate(dispenserPrefab);
            Vector3 position = prefab.transform.position;
            Quaternion rotation = prefab.transform.rotation;
            BlueprintToPlace blueprintToPlace = new BlueprintToPlace(Hub.DispenserPrefab.gameObject, position, rotation);
            
            _storageBlueprint.toPlaces.Add(blueprintToPlace);
            GameObject.DestroyImmediate(dispenserPrefab.gameObject);
            return new DispenserSetterEnter(_storageBlueprint);
        }

        return this;
    }

    public void Update()
    {
        dispenserPrefab.transform.position = ScreenToGroundPoint();
    }
    
    public Vector3 ScreenToGroundPoint()
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = Mathf.Ceil(position.x) - 0.5f;
        float z = Mathf.Ceil(position.z) - 0.5f;
        Vector3 result = new Vector3(x, 0f, z);
        return result;
    }
}
}