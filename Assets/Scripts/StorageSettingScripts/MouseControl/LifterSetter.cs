using UnitCodes.LifterCodes;
using UnityEngine;

namespace StorageSettingScripts.MouseControl
{
public class LifterSetterEnter : IMouseState
{
    private Lifter lifterPrefab;
    private StorageBlueprint _blueprint;

    public LifterSetterEnter(StorageBlueprint blueprint)
    {
        this._blueprint = blueprint;
        Vector3 pos = ScreenToGroundPoint();
        lifterPrefab = GameObject.Instantiate(Hub.LifterPrefab, pos, Quaternion.identity);
    }
    
    public IMouseState GetState()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject.DestroyImmediate(lifterPrefab.gameObject);
            return new NeutralState();
        }

        if (Input.GetMouseButtonDown(0))
            return new LifterSetterWork(_blueprint, lifterPrefab);

        return this;
    }

    public void Update()
    {
        lifterPrefab.transform.position = ScreenToGroundPoint();
        
        if(Input.GetKeyDown(KeyCode.R)) 
            lifterPrefab.transform.Rotate(new Vector3(0f, 90f, 0f));
    }
    
    
    private void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var position = Camera.main.transform.position;
        if (horizontal != 0) position += Vector3.right * (3f * Time.deltaTime * horizontal * Camera.main.orthographicSize);
        if (vertical != 0) position += Vector3.forward * (3f * Time.deltaTime * vertical * Camera.main.orthographicSize);

        Camera.main.transform.position = position;
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

public class LifterSetterWork : IMouseState
{
    private Lifter lifterPrefab;
    private StorageBlueprint _storageBlueprint;
    
    public LifterSetterWork(StorageBlueprint storageBlueprint, Lifter lifterPrefab)
    {
        this._storageBlueprint = storageBlueprint;
        this.lifterPrefab = lifterPrefab;
    }
    
    public IMouseState GetState()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var prefab = GameObject.Instantiate(lifterPrefab);
            
            Vector3 position = prefab.transform.position;
            Quaternion rotation = prefab.transform.rotation;
            BlueprintToPlace blueprintToPlace = new BlueprintToPlace(Hub.LifterPrefab.gameObject, position, rotation);
            _storageBlueprint.toPlaces.Add(blueprintToPlace);
            
            GameObject.DestroyImmediate(lifterPrefab.gameObject);
            return new LifterSetterEnter(_storageBlueprint);
        }

        return this;
    }

    public void Update()
    {
        lifterPrefab.transform.position = ScreenToGroundPoint();
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