using UnityEngine;

public class PrefabMOV : MonoBehaviour
{
    private float rotateSpeed;
    private float moveSpeed;
    private float rotation;
    private float rotTime;
    private float maxrotTime;

    public void Initialize(float Rotspeed, float Movspeed, float Rotate, float Rottime)
    {
        this.rotateSpeed = Rotspeed;
        this.moveSpeed = Movspeed;
        this.rotation = Rotate;
        this.rotTime = Rotate;
        this.maxrotTime = Rottime;
    }

    private void Update()
    {
        // Rotate the object towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, rotation, 0f), rotateSpeed * Time.deltaTime);

        // Decrement the rotation timer
        rotTime -= Time.deltaTime;

        // If the rotation timer is up, set a new random target rotation
        if (rotTime <= 0f)
        {
            rotation = Random.Range(0f, 360f);
            rotTime = Random.Range(0f, maxrotTime);
        }

        // Move the object
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
