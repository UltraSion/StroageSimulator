using System;
using UnityEngine;

namespace StorageSimulateScripts
{
public class CameraPole : MonoBehaviour
{
    public GameObject cam;

    private ICameraAction CameraAction = new NeutralAction();

    public void Update()
    {
        CameraAction = CameraAction.Update(this);
    }
}

public interface ICameraAction
{
    public ICameraAction Update(CameraPole pole);
}

public class NeutralAction : ICameraAction
{
    public ICameraAction Update(CameraPole pole)
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
            return new PoleStretch();

        if (Input.GetMouseButtonDown(0))
            return new PoleRotate();

        if (Input.GetMouseButtonDown(1))
            return new FreeAspect(pole.cam);

        return this;
    }
}

public class PoleStretch : ICameraAction
{
    public ICameraAction Update(CameraPole pole)
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        float camDistance = pole.transform.localScale.z;
        pole.transform.localScale = new Vector3(1f, 0f, camDistance + scroll * -20f);

        return new NeutralAction();
    }
}

public class PoleRotate : ICameraAction
{
    public ICameraAction Update(CameraPole pole)
    {
        Vector3 currRotation = pole.transform.rotation.eulerAngles;
        float mouseX = currRotation.y;
        float mouseY = currRotation.x;

        mouseX += Input.GetAxisRaw("Mouse X");
        mouseY -= Input.GetAxisRaw("Mouse Y");
        mouseY = Math.Clamp(mouseY, 0f, 90f);

        Vector3 newRotation = new Vector3(mouseY, mouseX, 0f);
        pole.transform.rotation = Quaternion.Euler(newRotation);

        if (Input.GetMouseButtonUp(0))
            return new NeutralAction();

        return this;
    }
}

public class FreeAspect : ICameraAction
{
    private Vector3 originPos;
    private Vector3 originRotation;
    private GameObject cam;

    public FreeAspect(GameObject cam)
    {
        originPos = cam.transform.position;
        originRotation = cam.transform.rotation.eulerAngles;
        this.cam = cam;
    }

    public ICameraAction Update(CameraPole pole)
    {
        RotateCam();
        MoveCam();

        if (Input.GetMouseButtonUp(1))
        {
            pole.cam.transform.position = originPos;
            pole.cam.transform.rotation = Quaternion.Euler(originRotation);

            return new NeutralAction();
        }

        return this;
    }

    public void RotateCam()
    {
        Vector3 currRotation = cam.transform.rotation.eulerAngles;
        float mouseX = currRotation.y;
        float mouseY = currRotation.x;

        mouseX += Input.GetAxisRaw("Mouse X");
        mouseY -= Input.GetAxisRaw("Mouse Y");
        mouseY = Math.Clamp(mouseY, 0f, 90f);

        Vector3 newRotation = new Vector3(mouseY, mouseX, 0f);
        cam.transform.rotation = Quaternion.Euler(newRotation);
    }

    public void MoveCam()
    {
        Vector3 force;
        force = cam.transform.right * (Input.GetAxis("Horizontal") * Time.deltaTime * 5);
        force += cam.transform.forward * (Input.GetAxis("Vertical") * Time.deltaTime * 5);

        if (Input.GetKey(KeyCode.Q))
            force -= cam.transform.up * (Time.deltaTime * 5);

        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space))
            force += cam.transform.up * (Time.deltaTime * 5);

        if (Input.GetKey(KeyCode.LeftShift))
            force *= 5;

        cam.transform.position += force;
    }
}
}