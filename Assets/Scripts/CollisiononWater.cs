using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class CollisiononWater : MonoBehaviour
{
    public Rigidbody HookRb;
    public GameObject splashpos;
    public Animator SplashAnim;
    public GameObject School;
    

    
    
    public Animator uiAnim;
    public Animator SlideAnimator;
    public GameObject biteui;
    public GameObject noCatchUI;
    public GameObject catchSystem;
    public GameObject fishslide;
    public GameObject CaughtUI;
   
    public GameObject leftC;
    public GameObject rightC;
    public GameObject rightCW;
    
    

    public List<GameObject> fishlist;
    
    public GameObject Fishshow;
    
   
    private Image fishui;
    private int biteanim;
    private int slideanim;
    private int noCatchAnim;
    private int FishRotAnim;
    
    private bool CatchsysBool;
    private GameObject InsOBJonhook;
    private GameObject InsOBJonUI;



    public GameObject Nib;
    private int defaultanim;
    

    public GameObject watersurface;
    public GameObject handrod;
    public throwhook fnreset;


    private float catchtime;
    private float catchspeed;

    public TextMeshProUGUI Score_txt;
    public List<TextMeshPro> Frame_Text;
    public List<GameObject> FishOnFrame;

    public  int[] FishSaved = new int[7];

    
   


    public pickuponplayer objpickup;


    public int TotalFish ;
    
    private bool Cont=false;


    private void Awake()
    {
        defaultanim = Animator.StringToHash("Base Layer.Splash");
        biteanim = Animator.StringToHash("Base Layer.Fishbite");
        slideanim = Animator.StringToHash("Base Layer.Fishbite_move");
        noCatchAnim = Animator.StringToHash("Base Layer.No Fish");
        FishRotAnim = Animator.StringToHash("Base Layer.UI_FIsh_Rot");

        leftC.SetActive(false);
        rightC.SetActive(false);
        Nib.GetComponent<CapsuleCollider>().enabled = false;
        fishui = fishslide.GetComponent<Image>();
        splashpos.SetActive(false);
        biteui.SetActive(false);
        catchSystem.SetActive(false);
        noCatchUI.SetActive(false);
        CatchsysBool = false;
        CaughtUI.SetActive(false);
        School.SetActive(false);

        Cont=  Main_MEnu.continues;

        if(Cont) 
        {
            string path = Application.persistentDataPath + "/saved.fish";
            if(File.Exists(path)) 
            {
                loadfish();
                Fishenable();
            }
            else
            {
                savefish();
            }

        }


    }
   
    private void Update()
    {
        int i = Random.Range(0, 7);
        catchtime = Random.Range(4f, 12f);
        float fishpos = fishslide.transform.localPosition.x;
        if (biteui.activeInHierarchy && Input.GetKey(KeyCode.Mouse2))
           
        {
            CatchsysBool = true;

            catchSystem.SetActive(true);
            
            catchspeed = RandomFish(i);
            SlideAnimator.speed = catchspeed; 
            SlideAnimator.Play(slideanim);
            


            
            biteui.SetActive(false);
            

        }
        if (fishui.color == Color.green && Input.GetKey(KeyCode.Mouse0) && CatchsysBool==true) 
        {
            
            InsOBJonhook = Instantiate(fishlist[i], HookRb.position,Fishshow.transform.rotation, HookRb.transform);
            InsOBJonUI   = Instantiate(fishlist[i], Fishshow.transform.position, Fishshow.transform.rotation, Fishshow.transform);
            InsOBJonUI.transform.localScale = 1250*InsOBJonUI.transform.localScale;
            foreach (Transform t in InsOBJonUI.GetComponentsInChildren<Transform>())
            {
                t.gameObject.layer = 7;
            }
            uiAnim.Play(FishRotAnim);
            School.SetActive(true);
            FindObjectOfType<SoundManager>().Play("Caught");  

            FindObjectOfType<SoundManager>().Play("FishFlaps");

            CaughtUI.SetActive(true);

            catchSystem.SetActive(false);
            CatchsysBool=false;
            
            StoreFish(i);
            
         
        }
        if(Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            catchSystem.SetActive(false);
        }

        



        Slidebar(fishpos);

        

        


        Fishenable();
        

        



    }
     

    public void Fishenable()
    {
        for(int i = 0;i<7;i++) 
        {
            Frame_Text[i].text = FishSaved[i] + " / 10";
            
            if (FishSaved[i] >= 10)
            {
                FishOnFrame[i].SetActive(true);
                Frame_Text[i].text = " ";

            }

            
        }
        
        Score_txt.text = " = " + TotalFish;
    }
    

    public void savefish()
    {
        Debug.Log("Game Saved");
        SaveSystem.SaveFish(this);
        

    }
    public void loadfish()
    {
        Debug.Log("Game Loaded");

        FishDat data = SaveSystem.LoadFish();

        
        
        for(int i  = 0; i < 7; i++)
        {
            FishSaved[i] = data.fishdata[i];

            
        }

        TotalFish = data.totalFish;
    }




    private void StoreFish(int i)
    {
        
        
        FishSaved[i]++;
        TotalFish++;

    }
    




    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")

        {
            HookRb.constraints = RigidbodyConstraints.FreezePosition;
            splashpos.transform.position = HookRb.position;
            splashpos.SetActive(true);
            SplashAnim.Play(defaultanim);
            FindObjectOfType<SoundManager>().Play("Drop");
            HookRb.transform.SetParent(null);
            Nib.GetComponent<CapsuleCollider>().enabled = true;
            Timer.Create(enablebite, catchtime,"bite");
            leftC.SetActive(false);
            rightC.SetActive(true);
            rightCW.SetActive(false);

        }
        if (other.gameObject.tag.Equals("nib"))
        {
            splashpos.SetActive(false);

            HookRb.transform.SetParent(handrod.transform);
            fnreset.ResetThrow();
            biteui.SetActive(false);
            catchSystem.SetActive(false);
            CaughtUI.SetActive(false);



            rightC.SetActive(false);

            


            Destroy(InsOBJonUI);
            Destroy( InsOBJonhook);
            FindObjectOfType<SoundManager>().Stop("FishFlaps");
            FindObjectOfType<SoundManager>().Stop("Reelin");

            

            objpickup.SitTextenable();

        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            biteui.SetActive(false);
            CatchsysBool = false;
            
        }

    }

    private void Slidebar(float fishpos)
    {
        if (CatchsysBool == true)
        {


            if (Mathf.Abs(fishpos - 0f) < 15f)
            {
                fishui.color = Color.green;

                

            }
            else if (Mathf.Abs(fishpos - 0f) >= 16f && Input.GetKeyDown(KeyCode.Mouse0) && CatchsysBool == true)
            {

                fishui.color = Color.red;
                catchSystem.SetActive(false);
                noCatchUI.SetActive(true);
                uiAnim.Play(noCatchAnim);
                FindObjectOfType<SoundManager>().Play("NoFish");
                CatchsysBool = false;


            }
            else
            {
                fishui.color = Color.white;
            }


        }

        
    }
    private float RandomFish(int i )

    {
         
        GameObject gameObject = fishlist[i];
        float speed;

        if (gameObject == fishlist[0] || gameObject == fishlist[1])
        {
            speed = 0.5f;
        }
        
        if (gameObject == fishlist[2] || gameObject == fishlist[3])
        {
            speed = 1f;
        }
        
        if (gameObject == fishlist[4])
        {
            speed = 1.5f;
        }

        else
        {
            speed = 2f;
        }

        return speed;
    }



    private void enablebite()
    {
        biteui.SetActive(true);
        uiAnim.Play(biteanim);
    }
    

    

}
