using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform carLookAt;
    [SerializeField] private Transform target;
    [Range(0, 0.25f)]
    [SerializeField] private float distanceDelta = 0.16f;
    private Vector3 _velocity;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(carLookAt);
    }

    void FixedUpdate()
    {
        transform.position += _velocity * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, distanceDelta);
    }    
}
