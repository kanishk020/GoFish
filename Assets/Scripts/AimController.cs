using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimController : MonoBehaviour
{
   
    public Vector2 turn;
    
    void Start()
    {
        
        turn.x = -90;
    }

   
    private void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * 175*Time.deltaTime;
        turn.y += Input.GetAxis("Mouse Y") * 175*Time.deltaTime;

        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
}
