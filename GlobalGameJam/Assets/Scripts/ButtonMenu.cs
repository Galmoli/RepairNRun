using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    [HideInInspector] public bool canPressButton = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canPressButton && hinput.anyGamepad.A)
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entra");
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
