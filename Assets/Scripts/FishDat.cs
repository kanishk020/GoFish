using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishDat 
{
    public int[] fishdata = new int[7];
    public int totalFish;
    
    public FishDat(CollisiononWater obj)
    {
        for (int i = 0; i < fishdata.Length; i++)
        {
            fishdata[i] = obj.FishSaved[i];
        }

        totalFish = obj.TotalFish;
    }
}
