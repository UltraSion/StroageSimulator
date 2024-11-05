using UnityEngine;

namespace StorageSettingScripts.MouseControl
{
public class StorageSizeSetterEnter : IMouseState
{
    public IMouseState GetState()
    {
        if (Input.GetMouseButtonDown(1))
            return new NeutralState();

        if (Input.GetMouseButtonDown(0)) 
            return new StorageSizeSetterWork();

        return this;
    }

    public void Update()
    {
    }
    

    
    
}

public class StorageSizeSetterWork : IMouseState
{
    private Storage _storage = StorageSettingManager.Storage;
    private Vector3 storageScale;
    
    public IMouseState GetState()
    {
        if (Input.GetMouseButtonUp(0))
            return new StorageSizeSetterEnd(storageScale);

        return this;
    }

    public void Update()
    {
        Vector3 pos = ScreenToGroundPoint();
        storageScale = pos;
        _storage.transform.localScale = pos;
    }
    
    public Vector3 ScreenToGroundPoint()
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = Mathf.Ceil(position.x);
        float z = Mathf.Ceil(position.z);
        Vector3 result = new Vector3(x, 50f, z);
        return result;
    }
}

public class StorageSizeSetterEnd : IMouseState
{
    private Vector3 cameraPos;
    
    public StorageSizeSetterEnd(Vector3 storageScale)
    {
        this.cameraPos = new Vector3(storageScale.x / 2, 10f, storageScale.z / 2);
    }
    
    public IMouseState GetState()
    {
        Camera.main.transform.position = cameraPos;
        return new StorageSizeSetterEnter();
    }

    public void Update()
    {
    }
}
}