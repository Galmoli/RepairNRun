using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFlipper : MonoBehaviour
{
    [SerializeField] private float timeToFlip;
    private float currentTime;

    private void Update()
    {
        float angle = Vector3.Dot(transform.up, Vector3.up);
        if (angle < 0.5f)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timeToFlip) FlipCar();
        }
        else currentTime = 0;
    }

    private void FlipCar()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.up), 0.1f);
    }
}
