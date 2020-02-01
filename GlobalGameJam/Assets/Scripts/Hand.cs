using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [HideInInspector] public hGamepad player1Hand;
    public float handSpeed;

    //[HideInInspector] public bool heBeFixin = false; //Quan aixo s'activi al ontriggerenter, que s'activi una animacio a la hand arreglant el problema (i q duri el mateix q el timer)
    //public float fixinTimer = 1f;
    //[HideInInspector] float fixinTimerCapsule = 0;

    [HideInInspector] public bool heJusPressFam = false;
    [HideInInspector] public int heJusPressFamIntTimer = 0;


    public float XClamper = 3.14f;
    public float YClamper = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        player1Hand = hinput.gamepad[0];

        //fixinTimerCapsule = fixinTimer;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!heBeFixin)
        //{
            this.gameObject.transform.position += player1Hand.rightStick.worldPositionCamera * handSpeed * Time.deltaTime;


            if (transform.localPosition.x > XClamper)
                transform.localPosition = new Vector3(XClamper, transform.localPosition.y, transform.localPosition.z);
            if (transform.localPosition.x < -XClamper)
                transform.localPosition = new Vector3(-XClamper, transform.localPosition.y, transform.localPosition.z);

            if (transform.localPosition.y > YClamper)
                transform.localPosition = new Vector3(transform.localPosition.x, YClamper, transform.localPosition.z);
            if (transform.localPosition.y < -YClamper)
                transform.localPosition = new Vector3(transform.localPosition.x, -YClamper, transform.localPosition.z);

        //}
            //else if (heBeFixin)
            //{
            //    fixinTimer -= Time.deltaTime;
            //    if (fixinTimer <= 0)
            //    {
            //        heBeFixin = false;
            //        fixinTimer = fixinTimerCapsule;
            //    }
            //}

        if (player1Hand.rightStickClick.justPressed)
        {
            heJusPressFam = true;
            heJusPressFamIntTimer = 10;
        }
        if (heJusPressFam)
        {
            heJusPressFamIntTimer--;
        }
        if (heJusPressFamIntTimer <= 0)
        {
            heJusPressFam = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("firstProblem") && player1Hand.rightStickClick)
        //{
        //    heBeFixin = true;
        //    Destroy(other.gameObject);
        //}
        //else if (other.CompareTag("secondProblem") && player1Hand.A)
        //{
        //    heBeFixin = true;
        //    Destroy(other.gameObject);
        //}
        //else if (other.CompareTag("thirdProblem") && player1Hand.X)
        //{
        //    heBeFixin = true;
        //    Destroy(other.gameObject);
        //}

        if (other.CompareTag("fourthProblem") && heJusPressFam)
        {
            other.transform.parent = this.gameObject.transform;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //if (other.CompareTag("firstProblem") && player1Hand.rightStickClick)
        //{
        //    heBeFixin = true;
        //    Destroy(other.gameObject);
        //}
        //else if (other.CompareTag("secondProblem") && player1Hand.A)
        //{
        //    heBeFixin = true;
        //    Destroy(other.gameObject);
        //}
        //else if (other.CompareTag("thirdProblem") && player1Hand.X)
        //{
        //    heBeFixin = true;
        //    Destroy(other.gameObject);
        //}

        if (other.CompareTag("fourthProblem") && heJusPressFam)
        {
            other.transform.parent = this.gameObject.transform;
        }
        if (other.CompareTag("fourthProblem") && player1Hand.rightStickClick.released)
        {
            other.transform.parent = FindObjectOfType<ObjectSpawner>().gameObject.transform;
        }
    }
}
