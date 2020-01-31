using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [HideInInspector] public hGamepad player1Hand;
    public float handSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player1Hand = hinput.gamepad[0];
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += player1Hand.rightStick.worldPositionCamera * handSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("firstProblem") && player1Hand.rightStickClick)
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("secondProblem") && player1Hand.A)
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("thirdProblem") && player1Hand.X)
        {
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("firstProblem") && player1Hand.rightStickClick)
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("secondProblem") && player1Hand.A)
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("thirdProblem") && player1Hand.X)
        {
            Destroy(other.gameObject);
        }
    }
}
