using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [Header("Amounts")]
    public int currentWood;
    public float currentWater;
    public int carrots;
    public int fishes;

    [Header("Limits")]
    public float waterLimit = 50;
    public float woodLimit = 10;
    public float carrotLimit = 10;
    public float fishLimit = 5;

    
    public void WaterLimit(float water)
    {
        if(currentWater <= waterLimit)
        {
            currentWater+= water;
        }
    }
}
