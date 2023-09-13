
using UnityEngine;

public class movementplayer : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 6f;
    public GameObject MainCam;
    public GameObject CamHut;
    private Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public GameObject rodplayer;
    Animator animator;
    public throwhook obj;

    public LayerMask surfaceLayerDirt;
    public LayerMask surfaceLayerWood;
    
    
    void Start()
    {
        
        animator = GetComponent<Animator>();
        cam = MainCam.GetComponent<Transform>();
        CamHut.SetActive(false);
        MainCam.SetActive(true);
        

    }

    

    void Update()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        

        if (direction.magnitude >= 0.1f && MainCam.activeSelf && !CamHut.activeSelf)
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f,targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            controller.SimpleMove(Vector3.up);
            
            if (rodplayer.activeSelf)
            {
                animator.SetBool("walkinhand", true);
                

            }
            else 
            { 
                animator.SetBool("isWalking", true);
            }

        }
        else if (direction.magnitude >=0.1f && CamHut.activeSelf)
        {
            
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move *speed *Time.deltaTime);
            
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("walkinhand", false);

        }
    }
    

    private void thrower()
    {
        obj.Throw();
        FindObjectOfType<SoundManager>().Play("Whip");
    }
    
    private void StepsWood()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0.2f, surfaceLayerDirt))

        {
            
            FindObjectOfType<SoundManager>().Play("FootHill");
        }
        if(Physics.Raycast(ray, out hit,0.2f, surfaceLayerWood))
        {
            FindObjectOfType<SoundManager>().Play("FootWood");
        }
        else
        {
            return;
        }
    }
    
    
    
}
