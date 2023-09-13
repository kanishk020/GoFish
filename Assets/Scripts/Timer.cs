using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;

public class Timer
{

    private static List<Timer> timerslist;
    private static GameObject initgameObject;
    private static void InitIfNeeded()
    {
        if (initgameObject == null)
        {
            initgameObject = new GameObject("Timer_InitGameobject");
            timerslist = new List<Timer>();
        }

    }



    public static Timer Create (Action action, float timer, string timerName = null)

    {
        InitIfNeeded();

        GameObject gameObject = new GameObject("Timer", typeof(MonoBehaviourHook));

        Timer tim = new Timer(action, timer,timerName, gameObject);

        
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = tim.Update;

        timerslist.Add(tim);

        return tim;

    }


    private static void RemoveTimer(Timer timer)

    {
        InitIfNeeded();
        timerslist.Remove(timer);

    }

    public static void StopTimer(string timerName)
    {
        for (int i=0 ; i<timerslist.Count; i++)
        {
            if (timerslist[i].timerName == timerName)
            {
                timerslist[i].DestrySelf();
                i--;
            }
        }
    }   







    private class MonoBehaviourHook : MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            if (onUpdate != null) onUpdate();
            
        }
    }





    private Action action;
    private float time;
    private string timerName;
    private bool isDestroyed;
    private GameObject gameObject;

    private Timer(Action action, float timer, string timerName ,GameObject gameObject)

    {
        this.action = action;
        this.time = timer;
        this.timerName = timerName;
        this.gameObject = gameObject;
        isDestroyed = false;
    }


    public void Update()
    {
        if (!isDestroyed)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                action();
                DestrySelf();
            }

        }
        
    }

    private void DestrySelf()
    {
        RemoveTimer(this);
        isDestroyed = true;
        UnityEngine.Object.Destroy(gameObject);
    }

}