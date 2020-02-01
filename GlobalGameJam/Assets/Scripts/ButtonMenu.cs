using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    [HideInInspector] public bool canPressButton = false;
    [HideInInspector] public Animator buttonAnimator;

    // Start is called before the first frame update
    void Start()
    {
        buttonAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPressButton && (hinput.anyGamepad.A || hinput.anyGamepad.X || hinput.anyGamepad.B || hinput.anyGamepad.Y || hinput.anyGamepad.leftStickClick))
        {
            buttonAnimator.SetTrigger("buttonClick");
            //SceneManager.LoadScene("MainScene");
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
