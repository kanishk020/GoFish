
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_MEnu : MonoBehaviour
{
    public GameObject Volumeslider;
    public GameObject Playbutton;

    public GameObject savefoundsys;
    public static bool continues;
    public static float volume=0.5f;

     


    private void Awake()


    {
        string path = Application.persistentDataPath + "/saved.fish";
        Volumeslider.SetActive(false);
        if (File.Exists(path))
        {
            continues = true;
            Playbutton.SetActive(false);
            savefoundsys.SetActive(true);


        }
        else
        {
            continues=false;
            Playbutton.SetActive(true);
            savefoundsys.SetActive(false);
        }
        


    }

    





    public void PLayGame()


    {


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
    public void Quitgame() 
    {
        Application.Quit();
    }

    public void  sliderUI()
    {
        if (Volumeslider.activeSelf)
        {
            Volumeslider.SetActive(false);
        }
        else
        {
            Volumeslider.SetActive(true);
        }

        
    }
    public void Contin()
    {
        
        
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }

    public void Newgame() 
    {
        string path = Application.persistentDataPath + "/saved.fish";

        File.Delete(path);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
    public void VolumeSlider(float vol )
    {

        volume = vol;

        Debug.Log(volume);


    }
  



}
