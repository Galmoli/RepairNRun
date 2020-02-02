using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class InitialCamera : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        StartCoroutine(WaitForCamera());
    }

    IEnumerator WaitForCamera()
    {
        yield return new WaitForSeconds(0.01f);
        _camera.enabled = false;
    }
}
