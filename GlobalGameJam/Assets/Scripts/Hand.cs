﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [HideInInspector] public hGamepad player1Hand;
    public float handSpeed;
    public bool isIniScene;

    //[HideInInspector] public bool heBeFixin = false; //Quan aixo s'activi al ontriggerenter, que s'activi una animacio a la hand arreglant el problema (i q duri el mateix q el timer)
    //public float fixinTimer = 1f;
    //[HideInInspector] float fixinTimerCapsule = 0;

    [HideInInspector] public bool heJusPressFam = true;

    [HideInInspector] public bool heJusReleaseFam = true;
    [HideInInspector] public int heJusReleaseFamIntTimer = 0;

    public float XClamper = 3.14f;
    public float YClamper = 1.5f;

    public Animator animatorController;

    // Start is called before the first frame update
    void Start()
    {
        player1Hand = hinput.anyGamepad;

        heJusPressFam = true;

        if (!isIniScene)animatorController.SetBool("NoAction", false);
        if(!isIniScene)animatorController.SetBool("Action", false);
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += player1Hand.rightStick.worldPositionCamera * handSpeed * Time.deltaTime;

        if (transform.localPosition.x > XClamper)
            transform.localPosition = new Vector3(XClamper, transform.localPosition.y, transform.localPosition.z);
        if (transform.localPosition.x < -XClamper)
             transform.localPosition = new Vector3(-XClamper, transform.localPosition.y, transform.localPosition.z);
        if (transform.localPosition.y > YClamper)
              transform.localPosition = new Vector3(transform.localPosition.x, YClamper, transform.localPosition.z);
        if (transform.localPosition.y < -YClamper)
            transform.localPosition = new Vector3(transform.localPosition.x, -YClamper, transform.localPosition.z);
    }
    private void OnTriggerStay(Collider other)
    {

        if ((other.CompareTag("firstProblem") || other.CompareTag("secondProblem") || other.CompareTag("thirdProblem")) && heJusPressFam)
        {
            other.transform.parent = this.gameObject.transform;
            if(!isIniScene)animatorController.SetBool("Action", true);
            if(!isIniScene)animatorController.SetBool("NoAction", false);
        }
        if ((other.CompareTag("FirstSolution") || other.CompareTag("SecondSolution") || other.CompareTag("ThirdSolution")) && heJusReleaseFam)
        {
            other.transform.parent = FindObjectOfType<ObjectSpawner>().gameObject.transform;
            if(!isIniScene)animatorController.SetBool("Action", false);
            if(!isIniScene)animatorController.SetBool("NoAction", true);
        }
    }
}
