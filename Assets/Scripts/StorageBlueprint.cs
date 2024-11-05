using System.Collections.Generic;
using UnityEngine;

public class StorageBlueprint
{
    public string storageName = "defalut";

    public Vector3 Scale;

    public List<BlueprintToPlace> toPlaces = new();

    public static StorageBlueprint Default
    {
        get
        {
            StorageBlueprint bp = new StorageBlueprint();
            bp.storageName = "default";
            bp.Scale = new Vector3(100f, 50f, 100f);
            bp.toPlaces.Add(new BlueprintToPlace(
                Hub.DispenserPrefab.gameObject,
                new Vector3(0f, 0f, 10f),
                Quaternion.identity
            ));

            for (int i = 0; i < 10; i++)
            {
                bp.toPlaces.Add(new BlueprintToPlace(
                    Hub.LifterPrefab.gameObject,
                    new Vector3(i, 0f, 0f),
                    Quaternion.identity
                ));
            }

            return bp;
        }
    }
}

public class BlueprintToPlace
{
    public readonly GameObject prefab;

    public readonly Vector3 position;

    public readonly Quaternion rotation;

    public BlueprintToPlace(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        this.prefab = prefab;
        this.position = position;
        this.rotation = rotation;
    }
}