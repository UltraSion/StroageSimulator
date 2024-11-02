using System.Collections.Generic;
using UnityEngine;

namespace Script
{
public class StorageManger
{
    public List<Storage> Storages;

    public Storage this[int index] => Storages[index];

    public void AddStorage(Vector3 position)
    {
        var storagePrefab = Hub.StoragePrefab;
        var storage = GameObject.Instantiate(storagePrefab, position, Quaternion.identity);
        Storages.Add(storage);
    }
}
}