using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    public int difficulty;

    [SerializeField] private bool replay;
    [SerializeField] private bool exit;
    [SerializeField] private bool resume;
    [HideInInspector] public bool canPressButton = false;
    [HideInInspector] public Animator buttonAnimator;
    [HideInInspector] public bool triggerAnimationEnd = false;

    public float animationEndTimer = 0.15f;
    [HideInInspector] public float animationEndTimerCapsule = 0;

    AudioSource buttonClick;
    public ParticleSystem pSystem;

    [HideInInspector] public bool lastSceneSender = false;
    [HideInInspector] public float lastSceneTimer;

    // Start is called before the first frame update
    void Start()
    {
        buttonAnimator = GetComponentInChildren<Animator>();
        buttonClick = this.GetComponent<AudioSource>();
        lastSceneTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPressButton && (hinput.anyGamepad.A || hinput.anyGamepad.X || hinput.anyGamepad.B || hinput.anyGamepad.Y || hinput.anyGamepad.rightStickClick))
        {
            PlayerPrefs.SetInt("Difficulty", difficulty);
            buttonAnimator.SetTrigger("buttonClick");
            triggerAnimationEnd = true;
            Time.timeScale = 1;
            
        }
        if (triggerAnimationEnd)
        {
            animationEndTimerCapsule += Time.deltaTime;
        }
        if (animationEndTimerCapsule >= animationEndTimer)
        {
            if (!buttonClick.isPlaying && !lastSceneSender)
            {
                buttonClick.Play();
                lastSceneSender = true;
            }
            if (!pSystem.isPlaying) pSystem.Play();
            lastSceneTimer += Time.deltaTime;
        }
        if (lastSceneTimer >= 0.5f)
        {
            if (replay) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            else if (exit) SceneManager.LoadScene(0);
            else if (resume)
            {
                buttonAnimator.SetTrigger("back2newState");
                lastSceneTimer = 0;
                canPressButton = true;
                triggerAnimationEnd = false;
                animationEndTimer = 0.15f;
                pSystem.Stop();
                buttonClick.Stop();
                FindObjectOfType<PauseMenu>().ResumeGame();
            }
            else SceneManager.LoadScene("GameScene1");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            canPressButton = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            canPressButton = false;
        }
    }
}
