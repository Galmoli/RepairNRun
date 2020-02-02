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

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && !woops.isPlaying)
        {
            woops.Play();
        }
    }
}
