using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemBehaviour : MonoBehaviour
{
    [HideInInspector] public Hand handerele;
    GameObject IKmaster1;
    GameObject IKmaster2;
    GameObject IKmaster3;
    ObjectSpawner objSpawner;

    [HideInInspector] public SoundManager sManager;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0);
        handerele = FindObjectOfType<Hand>();
        sManager = FindObjectOfType<SoundManager>();
        objSpawner = FindObjectOfType<ObjectSpawner>();
        IKmaster1 = GameObject.FindGameObjectWithTag("IK1");
        IKmaster2 = GameObject.FindGameObjectWithTag("IK2");
        IKmaster3 = GameObject.FindGameObjectWithTag("IK3");
    }
    void Update()
    {
        if (this.CompareTag("firstProblem")) IKmaster1.transform.position = transform.position;
        if (this.CompareTag("secondProblem")) IKmaster2.transform.position = transform.position;
        if (this.CompareTag("thirdProblem")) IKmaster3.transform.position = transform.position;
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
            IKmaster1.transform.SetParent(null);
            objSpawner.CallCoroutineDisappear(1);
        }
        else if (this.CompareTag("secondProblem") && other.CompareTag("SecondSolution") && hinput.anyGamepad.rightStickClick.released)
        {
            hinput.anyGamepad.Vibrate(0.25f);
            if (!sManager.electroShock.isPlaying) sManager.electroShock.Play();
            handerele.animatorController.SetBool("Idle", false);
            handerele.animatorController.SetBool("Action", true);
            Destroy(this.gameObject);
            IKmaster2.transform.SetParent(null);
            objSpawner.CallCoroutineDisappear(2);
        }
        else if (this.CompareTag("thirdProblem") && other.CompareTag("ThirdSolution") && hinput.anyGamepad.rightStickClick.released)
        {
            hinput.anyGamepad.Vibrate(0.25f);
            if (!sManager.electroShock.isPlaying) sManager.electroShock.Play();
            handerele.animatorController.SetBool("Idle", false);
            handerele.animatorController.SetBool("Action", true);
            Destroy(this.gameObject);
            IKmaster3.transform.SetParent(null);
            objSpawner.CallCoroutineDisappear(3);
        }

    }
}
