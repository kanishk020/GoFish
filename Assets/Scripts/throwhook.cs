using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class throwhook : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public GameObject objectToThrow;
    public GameObject water;
    public GameObject RHand;
    private Rigidbody objectthrow;
    public GameObject RightBW;
    private int layercoll = 1 << 6;
    

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    [Header("Line Rend")]
    public Transform sourcepoint;
    public GameObject player;
    public float reelspeed;
    
    private LineRenderer lineRenderer;
    

    Animator animator;
    



    
    private bool readyToThrow;
    private bool isSit;

    public void Start()
    {
        RightBW.SetActive(false);
        
        readyToThrow = true;
        objectthrow = objectToThrow.GetComponent<Rigidbody>();
        objectthrow.useGravity = false;
        isSit = false;
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        
        animator = player.GetComponent<Animator>();
        
    }

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Mouse0 ) && readyToThrow ==true  && isSit== true)
        {


            animator.SetBool("Castreel", true);
            lineRenderer.enabled = true;


        }
        if(Input.GetKey(KeyCode.Mouse1) && readyToThrow == false  && isSit == true )
           
        {
            animator.SetBool("Castreel", false);

            animator.SetBool("IdleReel", false);
            
            Timer.StopTimer("bite");
            Reeling();
            RightBW.SetActive(true);


        }
        if(Input.GetKeyUp(KeyCode.Mouse1) && readyToThrow == false && isSit == true)
        {
            animator.SetBool("IdleReel", true);
            FindObjectOfType<SoundManager>().Stop("Reelin");
            RightBW.SetActive(false);
        }
        
        
        
        
       
        

        lineRenderer.SetPosition(0, sourcepoint.position);
        lineRenderer.SetPosition(1, objectthrow.position);

    }

    public void Throw()
    {
            readyToThrow = false;
            objectthrow.useGravity = true;


        Vector3 forceDirection;

        RaycastHit hit;

        Physics.Raycast(cam.position, cam.forward, out hit, 100f, layercoll);
            
                forceDirection = (hit.point - sourcepoint.position).normalized;
            

            
            Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

            objectthrow.AddForce(forceToAdd, ForceMode.Impulse);
            objectToThrow.transform.SetParent(water.transform);
            


    }
    
    public void ResetThrow()
    {
        readyToThrow = true;
    }
    
    
    public void Reeling()
    {
        
        
        objectthrow.useGravity = false;
        objectthrow.constraints = RigidbodyConstraints.None;
        objectToThrow.transform.SetParent(null);
        objectthrow.position = Vector3.MoveTowards(objectthrow.transform.position, sourcepoint.position, reelspeed*Time.deltaTime);

        


    }
    public void SITCONF()
    {
        isSit = true;
    }
    
}