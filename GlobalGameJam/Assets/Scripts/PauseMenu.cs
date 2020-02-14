using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject RestartButton;
    public GameObject ExitButton;

    public GameObject GreyFader;
    public GameObject UIText;

    public GameObject handToMove;
    public GameObject handToHideTemporarly;

    [HideInInspector] public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused && (Input.GetButtonDown("Pause") || Input.GetKeyDown(KeyCode.M)))
        {
            isPaused = true;
            RestartButton.SetActive(true);
            ExitButton.SetActive(true);
            GreyFader.SetActive(true);
            UIText.SetActive(true);
            handToMove.SetActive(true);
            handToHideTemporarly.SetActive(false);
            Time.timeScale = 0.05f;
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
        GreyFader.SetActive(false);
        UIText.SetActive(false);
        handToMove.SetActive(false);
        handToHideTemporarly.SetActive(true);
        Time.timeScale = 1;
    }
}
