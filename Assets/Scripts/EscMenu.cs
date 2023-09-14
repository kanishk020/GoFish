using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    public static bool ispaused = false;
    public GameObject Uiload;
    public GameObject UiSave;
     

    public GameObject pauseMenuUI;

    private void Start()
    {
        Uiload.SetActive(false);
        UiSave.SetActive(false);
        pauseMenuUI.SetActive(false);
    }


    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) 
        {
            if(ispaused == true) 
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (ispaused == true)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        if(ispaused == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
    public void Resume()
    {
        

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        ispaused = false;
    }
    private void Pause()
    {
        Uiload.SetActive(false);
        UiSave.SetActive(false);
        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;
        ispaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Loaded()
    {
        Uiload.SetActive(true);
        UiSave.SetActive(false);

    }

    public void Saved()
    {
        UiSave.SetActive(true);
        Uiload.SetActive(false);
    }
}
