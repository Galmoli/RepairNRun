using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    [SerializeField] private Transform midLapPos;
    [SerializeField] private float midLapCheckSize = 20f;
    [SerializeField] private bool isPlayer;
    [SerializeField] private bool draw;
    private bool midLap;
    private int currentLaps;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, midLapPos.position) < midLapCheckSize)
        {
            midLap = true;
        }
    }

    public void CheckLap()
    {
        if (midLap)
        {
            currentLaps++;
            midLap = false;
            if (currentLaps >= LapsSingleton.Instance.totalLaps)
            {
                if (isPlayer) Debug.Log(LapsSingleton.Instance.GetMyFinalPosition());
                else LapsSingleton.Instance.carsFinished++;
            }
            Debug.Log(currentLaps);
        }
    }

    private void OnDrawGizmos()
    {
        if(draw) Gizmos.DrawSphere(midLapPos.position, midLapCheckSize);
    }
}
