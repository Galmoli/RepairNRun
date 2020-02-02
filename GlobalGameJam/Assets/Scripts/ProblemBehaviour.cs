using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemBehaviour : MonoBehaviour
{
    [HideInInspector] public Hand handerele;

    [HideInInspector] public SoundManager sManager;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0);
        handerele = FindObjectOfType<Hand>();
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
            if (!sManager.electroShock.isPlaying) sManager.electroShock.Play();
            handerele.animatorController.SetBool("Idle", false);
            handerele.animatorController.SetBool("Action", true);
            Destroy(this.gameObject);
        }
        else if (this.CompareTag("secondProblem") && other.CompareTag("SecondSolution") && hinput.anyGamepad.rightStickClick.released)
        {
            hinput.anyGamepad.Vibrate(0.25f);
            if (!sManager.electroShock.isPlaying) sManager.electroShock.Play();
            handerele.animatorController.SetBool("Idle", false);
            handerele.animatorController.SetBool("Action", true);
            Destroy(this.gameObject);
        }
        else if (this.CompareTag("thirdProblem") && other.CompareTag("ThirdSolution") && hinput.anyGamepad.rightStickClick.released)
        {
            hinput.anyGamepad.Vibrate(0.25f);
            if (!sManager.electroShock.isPlaying) sManager.electroShock.Play();
            handerele.animatorController.SetBool("Idle", false);
            handerele.animatorController.SetBool("Action", true);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (this.CompareTag("firstProblem") && other.CompareTag("FirstSolution") && hinput.anyGamepad.rightStickClick.released)
        {
            hinput.anyGamepad.Vibrate(0.25f);
            if (!sManager.electroShock.isPlaying) sManager.electroShock.Play();
            handerele.animatorController.SetBool("Idle", false);
            handerele.animatorController.SetBool("Action", true);
            Destroy(this.gameObject);
        }
        else if (this.CompareTag("secondProblem") && other.CompareTag("SecondSolution") && hinput.anyGamepad.rightStickClick.released)
        {
            hinput.anyGamepad.Vibrate(0.25f);
            if (!sManager.electroShock.isPlaying) sManager.electroShock.Play();
            handerele.animatorController.SetBool("Idle", false);
            handerele.animatorController.SetBool("Action", true);
            Destroy(this.gameObject);
        }
        else if (this.CompareTag("thirdProblem") && other.CompareTag("ThirdSolution") && hinput.anyGamepad.rightStickClick.released)
        {
            hinput.anyGamepad.Vibrate(0.25f);
            if (!sManager.electroShock.isPlaying) sManager.electroShock.Play();
            handerele.animatorController.SetBool("Idle", false);
            handerele.animatorController.SetBool("Action", true);
            Destroy(this.gameObject);
        }

    }
}
