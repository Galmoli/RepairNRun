using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemBehaviour : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("firstProblem") && other.CompareTag("FirstSolution") && hinput.anyGamepad.rightStickClick.released)
        {
            hinput.anyGamepad.Vibrate(0.25f);
            Destroy(this.gameObject);
        }
        else if (this.CompareTag("secondProblem") && other.CompareTag("SecondSolution") && hinput.anyGamepad.rightStickClick.released)
        {
            hinput.anyGamepad.Vibrate(0.25f);
            Destroy(this.gameObject);
        }
        else if (this.CompareTag("thirdProblem") && other.CompareTag("ThirdSolution") && hinput.anyGamepad.rightStickClick.released)
        {
            hinput.anyGamepad.Vibrate(0.25f);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (this.CompareTag("firstProblem") && other.CompareTag("FirstSolution") && hinput.anyGamepad.rightStickClick.released)
        {
            hinput.anyGamepad.Vibrate(0.25f);
            Destroy(this.gameObject);
        }
        else if (this.CompareTag("secondProblem") && other.CompareTag("SecondSolution") && hinput.anyGamepad.rightStickClick.released)
        {
            hinput.anyGamepad.Vibrate(0.25f);
            Destroy(this.gameObject);
        }
        else if (this.CompareTag("thirdProblem") && other.CompareTag("ThirdSolution") && hinput.anyGamepad.rightStickClick.released)
        {
            hinput.anyGamepad.Vibrate(0.25f);
            Destroy(this.gameObject);
        }

    }
}
