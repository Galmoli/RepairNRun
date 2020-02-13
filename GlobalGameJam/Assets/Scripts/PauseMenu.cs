using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject RestartButton;
    public GameObject ExitButton;

    [HideInInspector] public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused && (Input.GetButtonDown("Pause") || Input.GetKeyDown(KeyCode.M)))
        {
            isPaused = true;
            RestartButton.SetActive(true);
            ExitButton.SetActive(true);
            Time.timeScale = 0.25f;
        }
        else if (isPaused && (Input.GetButtonDown("Pause") || Input.GetKeyDown(KeyCode.M)))
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        RestartButton.SetActive(false);
        ExitButton.SetActive(false);
        Time.timeScale = 1;
    }
}
