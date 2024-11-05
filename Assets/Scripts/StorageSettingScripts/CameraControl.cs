using UnityEngine;

namespace StorageSettingScripts
{
public class CameraControl : MonoBehaviour {
    [SerializeField] private float speed = 3f;
    [SerializeField] private Camera cam;


    void Update()
    {
        Move();
        Zoom();
    }

    private void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var position = transform.position;
        if (horizontal != 0) position += Vector3.right * (speed * Time.deltaTime * horizontal * cam.orthographicSize);
        if (vertical != 0) position += Vector3.forward * (speed * Time.deltaTime * vertical * cam.orthographicSize);

        transform.position = position;
    }
    
    private void Zoom()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll == 0) return;
        cam.orthographicSize -= scroll * cam.orthographicSize / 5;
    }
}
}