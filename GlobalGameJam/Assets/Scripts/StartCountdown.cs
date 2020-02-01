using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour
{
    public CarMovement[] allCarsMovement;
    [HideInInspector] public float timer = 0;
    [HideInInspector] public float maxTimer = 3;

    public Text countdownTimer;
    [HideInInspector] public bool restartThisTimer = true;
    [HideInInspector] public bool restartThisSecondTimer = true;
    [HideInInspector] public bool restartThisThirdTimer = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= allCarsMovement.Length - 1; i++)
        {
            allCarsMovement[i].enabled = false;
        }

        countdownTimer.color = new Color(countdownTimer.color.r, countdownTimer.color.g, countdownTimer.color.b, 0); //Comença sent transparent
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 4)
        {
            countdownTimer.enabled = false;
            restartThisSecondTimer = true;
            restartThisTimer = true;
            countdownTimer.gameObject.SetActive(false);
        }
        else if (timer >= 3)
        {
            if (restartThisThirdTimer)
            {
                countdownTimer.color = new Color(countdownTimer.color.r, countdownTimer.color.g, countdownTimer.color.b, Time.deltaTime);
                restartThisThirdTimer = false;
            }
            else
            {
                countdownTimer.color += new Color(countdownTimer.color.r, countdownTimer.color.g, countdownTimer.color.b, Time.deltaTime);
            }
            countdownTimer.text = "GO!";

            for (int i = 0; i <= allCarsMovement.Length - 1; i++)
            {
                allCarsMovement[i].enabled = true;
            }
        }
        else if (timer >= 2)
        {
            if (restartThisSecondTimer)
            {
                countdownTimer.color = new Color(countdownTimer.color.r, countdownTimer.color.g, countdownTimer.color.b, Time.deltaTime);
                restartThisSecondTimer = false;
            }
            else
            {
                countdownTimer.color += new Color(countdownTimer.color.r, countdownTimer.color.g, countdownTimer.color.b, Time.deltaTime);
            }
            countdownTimer.text = "1";
        }
        else if (timer >= 1)
        {
            if (restartThisTimer)
            {
                countdownTimer.color = new Color(countdownTimer.color.r, countdownTimer.color.g, countdownTimer.color.b, Time.deltaTime);
                restartThisTimer = false;
            }
            else
            {
                countdownTimer.color += new Color(countdownTimer.color.r, countdownTimer.color.g, countdownTimer.color.b, Time.deltaTime);
            }
            countdownTimer.text = "2";
        }
        else if (timer >= 0)
        {
            countdownTimer.color += new Color(countdownTimer.color.r, countdownTimer.color.g, countdownTimer.color.b, Time.deltaTime);
            countdownTimer.text = "3";
        }
    }
}
