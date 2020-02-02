using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource OST;
    public AudioSource cheeringAudience;
    public AudioSource embrague;
    public AudioSource fixedThat;
    public AudioSource throughGrass;
    public AudioSource wheelPinchazo;
    public AudioSource carCrash;
    public AudioSource objectAppeared;
    public AudioSource woops;
    public AudioSource electroShock;

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.W) || hinput.anyGamepad.leftStickClick) && !woops.isPlaying)
        {
            woops.Play();
            hinput.anyGamepad.Vibrate(0.25f, 0.05f, 0.5f);
        }
    }
}
