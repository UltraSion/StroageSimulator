using UnityEngine;

namespace UnitCodes.LifterCodes
{
public class Pad : MonoBehaviour
{
    private GameObject onLift;
    public bool IsEmpty => onLift == null;

    public void Lift(GameObject gameObject)
    {
        gameObject.transform.position = transform.position;
        gameObject.transform.parent = transform;
        onLift = gameObject;
    }
}
}