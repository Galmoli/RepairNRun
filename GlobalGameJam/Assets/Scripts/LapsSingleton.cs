using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapsSingleton : MonoBehaviour
{
    private static LapsSingleton instance;
    public static LapsSingleton Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<LapsSingleton>();
            return instance;
        }
    }

    [HideInInspector] public int carsFinished = 0;
    public int totalLaps = 0;
    private int numOfEnemies;

    private void Awake()
    {
        numOfEnemies = FindObjectsOfType<CarAI>().Length;
    }

    public void CarFinished()
    {
        if (carsFinished >= numOfEnemies) return;
        carsFinished++;
    }

    public int GetMyFinalPosition()
    {
        return carsFinished + 1;
    }
}
