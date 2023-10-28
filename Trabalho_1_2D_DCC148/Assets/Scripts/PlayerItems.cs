using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public int currentWood;
    public float currentWater;
    public int carrots;
    private float waterLimit = 50;
    
    public void WaterLimit(float water)
    {
        if(currentWater <= waterLimit)
        {
            currentWater+= water;
        }
    }
}
