using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public SoundsList[] Sound;
    public static SoundManager instance;


    private float VolMult = Main_MEnu.volume;
    public GameObject bird;
    public GameObject SoundAmb;
    

    
    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad (gameObject);

        foreach(SoundsList s in Sound)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume*VolMult;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
        bird.GetComponent<AudioSource>().volume = VolMult;
        SoundAmb.GetComponent<AudioSource>().volume = VolMult;
    }

    
    public void Play(string name)
    {
        SoundsList s = Array.Find(Sound, sound => sound.Name== name);
        if (s == null)
        {
            Debug.LogWarning("SoundMismatch");
            return;
        }
        s.source.Play();
    }
    public void Stop (String name) 
    {
        SoundsList s = Array.Find(Sound,sound => sound.Name== name);
        if (s == null) 
        {
            Debug.LogWarning("SoundMismatch");
            return;

        }
        s.source.Stop();
    }
}
