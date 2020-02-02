using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour
{
    public CarMovement playerCarMovement;
    public CarAI[] allCarsMovement;
    [HideInInspector] public float timer = 0;
    [HideInInspector] public float maxTimer = 3;

    public Text countdownTimer;
    [HideInInspector] public bool restartThisTimer = true;
    [HideInInspector] public bool restartThisSecondTimer = true;
    [HideInInspector] public bool restartThisThirdTimer = true;
    [HideInInspector] public bool cantPlayAnymore = true;
    [HideInInspector] public AudioSource countdownSounds;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= allCarsMovement.Length - 1; i++)
        {
            allCarsMovement[i].enabled = false;
        }

        countdownTimer.color = new Color(countdownTimer.color.r, countdownTimer.color.g, countdownTimer.color.b, 0); //Comença sent transparent
        countdownSounds = countdownTimer.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 4.5f)
        {
            countdownSounds.Stop();
            countdownTimer.enabled = false;
            countdownTimer.gameObject.SetActive(false);
        }
        else if (timer >= 4)
        {
            restartThisSecondTimer = true;
            restartThisTimer = true;
        }
        else if (timer >= 3)
        {
            if (!countdownSounds.isPlaying && cantPlayAnymore)
            {
                countdownSounds.pitch = 1.5f;
                countdownSounds.Play();
                cantPlayAnymore = false;
            }

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
            if (!countdownSounds.isPlaying) countdownSounds.Play();
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
            if (!countdownSounds.isPlaying)
            {
                countdownSounds.Play();
            }

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
