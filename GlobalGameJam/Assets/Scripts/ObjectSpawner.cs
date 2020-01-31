using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public int xMaxValue = 12;
    public int yMaxValue = 5;

    public float timer = 3;
    public float timerVariator = 1.5f;
    [HideInInspector] public float maxTimerValue = 0;

    //BONUS IS KEY NAME FOR POWER UP
    [HideInInspector] public int randomItemSelector;

    public GameObject firstProblem;
    public GameObject secondProblem;
    public GameObject thirdProblem;

    // Start is called before the first frame update
    void Start()
    {
        maxTimerValue = timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Vector3 transformPosition = new Vector3(Mathf.RoundToInt(Random.Range(-xMaxValue, xMaxValue)), Mathf.RoundToInt(Random.Range(-yMaxValue - 1, yMaxValue)), 0);

            randomItemSelector = Random.Range(0, 3);
            if (randomItemSelector != 0) randomItemSelector = Random.Range(0, 3);

            if (randomItemSelector == 0)
            {
                Debug.Log("first");
                var obj =Instantiate(firstProblem, transformPosition, new Quaternion(0, 0, 0, 0), transform);
                obj.transform.localPosition = transformPosition;
            }
            else if (randomItemSelector == 1)
            {
                Debug.Log("second");
                var obj = Instantiate(secondProblem, transformPosition, new Quaternion(0, 0, 0, 0), transform);
                obj.transform.localPosition = transformPosition;
            }
            else
            {
                Debug.Log("third");
                var obj = Instantiate(thirdProblem, transformPosition, new Quaternion(0, 0, 0, 0), transform);
                obj.transform.localPosition = transformPosition;
            }

            timer = Random.Range(maxTimerValue - timerVariator, maxTimerValue + (timerVariator/3)); //Fer que a mesura que avanci la partida, aquest timer cada vegada sigui mes petit
        }
    }
}
