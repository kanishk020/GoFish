
using UnityEngine;

public class HUTcam : MonoBehaviour
{
    public Transform player;

    float xRotation = 0f;
    

    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * 175 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 175 * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);

    }
}
