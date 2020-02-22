using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CarCameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public CarMovement carMovement;
    public GameObject lookAtObj;
    public ParticleSystem speedParticles;

    private float iniFOV;
    public float fovMultiplicator = 0.35f;

    private Vector3 iniLookAt;
    public float lookAtMultiplicator = 0.005f;

    public float goingFastSpeed = 15;
    public float colorParticlesMultiplicator = 0.05f;

    void Start()
    {
        iniFOV = virtualCamera.m_Lens.FieldOfView;
        iniLookAt = lookAtObj.transform.localPosition;
    }

    void Update()
    {
        float carSpeed = carMovement.GetCarVelocity();

        virtualCamera.m_Lens.FieldOfView = iniFOV + carSpeed * fovMultiplicator;

        lookAtObj.transform.localPosition = iniLookAt + new Vector3(0, carSpeed * lookAtMultiplicator, 0);

        if (carSpeed > goingFastSpeed) speedParticles.startColor = new Color(1, 1, 1, carSpeed * colorParticlesMultiplicator);
        else speedParticles.startColor = new Color(1, 1, 1, 0);
    }
}
