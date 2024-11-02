using UnityEngine;

namespace DefaultNamespace
{
public class StorageSetting
{
    public readonly int width;
    public readonly int  height;
    
    public StorageSetting(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public Vector3 Scale => new Vector3(width, 5f, height);
}
}