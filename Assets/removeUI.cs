using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeUI : MonoBehaviour
{
    [SerializeField] private GameObject nocatchui;

    public void DisableUI()
    {
        nocatchui.SetActive(false);
    }

    
    
}
