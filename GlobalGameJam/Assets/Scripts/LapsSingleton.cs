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

    public int totalLaps = 0;
    [HideInInspector] public int carsFinished = 0;

    public int GetMyFinalPosition()
    {
        return carsFinished + 1;
    }
}
