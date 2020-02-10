using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEditor;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    [SerializeField] private Transform midLapPos;
    [SerializeField] private float midLapCheckSize = 20f;
    [SerializeField] private bool isPlayer;
    [SerializeField] private bool draw;
    [SerializeField] private CinemachineVirtualCamera finishCamera;
    [SerializeField] private CinemachineVirtualCamera cinematicCamera;
    [SerializeField] private GameObject replayButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject finishedText;
    [SerializeField] private GameObject firstPos;
    [SerializeField] private GameObject secondPos;
    [SerializeField] private GameObject thirdPos;
    [SerializeField] private GameObject otherPos;
    [SerializeField] private GameObject volante;
    [SerializeField] private GameObject Aparato;
    [SerializeField] private TextMeshProUGUI otherText;
    private bool midLap;
    private int currentLaps;

    private void Start()
    {
        if(isPlayer) replayButton.SetActive(false);
        if(isPlayer) exitButton.SetActive(false);
    }

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
                if (isPlayer)
                {
                    replayButton.SetActive(true);
                    exitButton.SetActive(true);
                    finishedText.SetActive(true);
                    var myPos = LapsSingleton.Instance.GetMyFinalPosition();
                    switch (myPos)
                    {
                        case 1:
                            firstPos.SetActive(true);
                            break;
                        case 2:
                            secondPos.SetActive(true);
                            break;
                        case 3:
                            thirdPos.SetActive(true);
                            break;
                        default:
                            otherPos.SetActive(true);
                            otherText.text = myPos.ToString();
                            break;
                    }
                    hinput.anyGamepad.StopVibration();
                    finishCamera.enabled = true;
                    GetComponent<Car>().EnableIA();
                    GetComponent<Car>().raceFinished = true;
                    GetComponent<Car>()._brokenParts.Clear();
                    volante.SetActive(false);
                    Aparato.SetActive(false);
                    StartCoroutine(CinematicCamera());
                }
                else LapsSingleton.Instance.CarFinished();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(draw) Gizmos.DrawSphere(midLapPos.position, midLapCheckSize);
    }

    IEnumerator CinematicCamera()
    {
        yield return new WaitForSeconds(3f);
        cinematicCamera.enabled = true;
    }
}
