
using System;
using UnityEngine;


public class pickuponplayer : MonoBehaviour
{
    public GameObject Rodonplayer;
    public GameObject PickUpText;
    public GameObject HUtcam;
    public GameObject pickupObject;
    public GameObject SitText;
    public bool applyRootMotion;
    public GameObject camaim;
    public GameObject camfollow;
    public GameObject Player;
    public GameObject PierLoc;
    public GameObject CamFollowLoc;
    public GameObject SitCoronaLoc;

    public CollisiononWater objwtr;
    public throwhook obj;

    private bool isSitting;

    private bool situi;

    Animator anim;

    void Start()
    {
        Rodonplayer.SetActive(false);
        PickUpText.SetActive(false);
        SitText.SetActive(false);
        
        anim = GetComponent<Animator>();
        camaim.SetActive(false);
        
        isSitting = false;

        


    }

    void Update()
    {
        
        if (PickUpText.activeSelf && Input.GetKey(KeyCode.E))
        {

            anim.SetTrigger("keypressE");

        }
        if (Rodonplayer.activeSelf && SitText.activeSelf && Input.GetKeyDown(KeyCode.Q) && isSitting==false)
        {




            Player.transform.SetPositionAndRotation(PierLoc.transform.position, PierLoc.transform.rotation);
             
            
            anim.SetBool("KeypressQ", true);
            
            
            
            this.GetComponent<CharacterController>().enabled = false;
            
           camfollow.SetActive(false);
           camaim.SetActive (true);
            

        }
        if(Rodonplayer.activeSelf && SitText.activeSelf && Input.GetKeyDown(KeyCode.Q) && isSitting == true)
        {
            
            anim.SetBool("KeypressQ", false);

            isSitting = false;
            
            camfollow.SetActive(true);
            camaim.SetActive(false);


        }
        


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "fishingrod")
        {
            PickUpText.SetActive(true);
        }
        if (other.gameObject.tag == "door")
        {
            HUtcam.SetActive(true);
            camaim.SetActive(false);
            
        }
        if (other.gameObject.tag == "Pier" && Rodonplayer.activeSelf)
        {
            SitText.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Pier" && Rodonplayer.activeSelf)
        {
            SitText.SetActive(true);
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "fishingrod")
        {
            PickUpText.SetActive(false);
        }
        if (other.gameObject.tag == "Pier")
        {
            SitText.SetActive(false);
        }
        if (other.gameObject.tag == "door")
        {

            
            HUtcam.SetActive(false);
            camfollow.SetActive(true);
            camaim.SetActive(false);
        }
    }
    


    private void PickupObject()
    {

        pickupObject.SetActive(false);
        Rodonplayer.SetActive(true);
        PickUpText.SetActive(false);
        anim.ResetTrigger("keypressE");
    }
    private void Resetsit()
    {

        isSitting = true;
        obj.SITCONF();
         
        

    }
    private void EnableController()
    {
        
        this.GetComponent<CharacterController>().enabled = true;
        
        
    }
    public void SitTextenable()
    {
        SitText.SetActive(true);
        SitCoronaLoc.SetActive(true);
        

    }
    private void SitTextdisable()
    {
        SitText.SetActive(false);
        
        SitCoronaLoc.SetActive(false);
    }
    public void ReelingSound() 
    {
        FindObjectOfType<SoundManager>().Play("Reelin");
    }
}