using UnityEngine;

public class Node : MonoBehaviour
{
    public int nodeIndex;
    [SerializeField] private float checkingDistance;
    [HideInInspector] public bool carAchievedNode = false;
    private Transform car;

    private void Awake()
    {
        car = FindObjectOfType<Car>().transform;
    }

    private void Update()
    {
        if(carAchievedNode) return;
        if (Vector3.Distance(transform.position, car.position) <= checkingDistance && nodeIndex != 24) carAchievedNode = true;
    }
}
