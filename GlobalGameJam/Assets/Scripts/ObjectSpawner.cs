using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //public GameObject parentCamera;

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

    [HideInInspector] public bool isHavingFirstProblem;
    [HideInInspector] public bool isHavingSecondProblem;
    [HideInInspector] public bool isHavingThirdProblem;

    public GameObject IKmaster1;
    public GameObject IKmaster2;
    public GameObject IKmaster3;

    Vector3 ikStartPos1;
    Vector3 ikStartPos2;
    Vector3 ikStartPos3;

    public GameObject lightOn1;
    public GameObject lightOn2;
    public GameObject lightOn3;

    public GameObject lightOff1;
    public GameObject lightOff2;
    public GameObject lightOff3;

    void Start()
    {
        ikStartPos1 = IKmaster1.transform.position;
        ikStartPos2 = IKmaster2.transform.position;
        ikStartPos3 = IKmaster3.transform.position;
        maxTimerValue = timer;
    }

    /*void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {


            timer = Random.Range(maxTimerValue - timerVariator, maxTimerValue + (timerVariator/3)); //Fer que a mesura que avanci la partida, aquest timer cada vegada sigui mes petit
        }
    }*/
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) SpawnProblem();
    }
    public void SpawnProblem()
    {
        Vector3 transformPosition = new Vector3(Mathf.RoundToInt(Random.Range(-xMaxValue, xMaxValue)), Mathf.RoundToInt(Random.Range(-yMaxValue, yMaxValue)), 0);

        randomItemSelector = Random.Range(0, 3);
        if (randomItemSelector != 0) randomItemSelector = Random.Range(0, 3);

        //CHECK TYPE
        if (randomItemSelector == 0 && isHavingFirstProblem) randomItemSelector++;
        if (randomItemSelector == 1 && isHavingSecondProblem) randomItemSelector++;
        if (randomItemSelector == 2 && isHavingThirdProblem) randomItemSelector++;
        if (randomItemSelector == 0)
        {
            var obj = Instantiate(firstProblem, transformPosition, new Quaternion(0, 0, 0, 0), transform);
            obj.transform.localPosition = transformPosition;
            isHavingFirstProblem = true;
            IKmaster1.transform.SetParent(obj.transform);
            IKmaster1.transform.localPosition = Vector3.zero;
        }
        else if (randomItemSelector == 1)
        {
            var obj = Instantiate(secondProblem, transformPosition, new Quaternion(0, 0, 0, 0), transform);
            obj.transform.localPosition = transformPosition;
            isHavingSecondProblem = true;
            IKmaster2.transform.SetParent(obj.transform);
            IKmaster2.transform.localPosition = Vector3.zero;
        }
        else if (randomItemSelector == 2)
        {
            var obj = Instantiate(thirdProblem, transformPosition, new Quaternion(0, 0, 0, 0), transform);
            obj.transform.localPosition = transformPosition;
            isHavingThirdProblem = true;
            IKmaster3.transform.SetParent(obj.transform);
            IKmaster3.transform.localPosition = Vector3.zero;
        }
    }
    public void CallCoroutineDisappear(int i)
    {
        StartCoroutine(DisappearProblem(i));
    }
    public IEnumerator DisappearProblem(int i)
    {
        if (i == 1)
        {
            lightOn1.SetActive(true);
            lightOff1.SetActive(false);
            yield return new WaitForSeconds(1);
            IKmaster1.transform.position = ikStartPos1;
            isHavingFirstProblem = false;
            lightOn1.SetActive(false);
            lightOff1.SetActive(true);
        }
        else if (i == 2)
        {
            lightOn2.SetActive(true);
            lightOff2.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            IKmaster2.transform.position = ikStartPos2;
            isHavingSecondProblem = false;
            lightOn2.SetActive(false);
            lightOff2.SetActive(true);
        }
        else if (i == 3)
        {
            lightOn3.SetActive(true);
            lightOff3.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            IKmaster3.transform.position = ikStartPos3;
            isHavingThirdProblem = false;
            lightOn3.SetActive(false);
            lightOff3.SetActive(true);
        }
    }
}


