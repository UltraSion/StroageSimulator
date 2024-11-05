using System.Collections.Generic;
using UnityEngine;

namespace UnitCodes
{
public class Dispenser : MonoBehaviour
{
    public List<GameObject> toDispense = new();
    
    public Vector3 direction;
    public int legth;

    public Vector3 outputPos => transform.position + direction * legth;
}
}
