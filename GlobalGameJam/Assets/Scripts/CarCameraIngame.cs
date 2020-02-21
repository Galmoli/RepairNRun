using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CarCameraIngame : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public CarMovement carMovement;
    public GameObject lookAt;
    public ParticleSystem speedParticles;

    public float lerpTime;

    private float iniFOV;
    public float fovMultiplicator;

    private Vector3 iniLookPos;
    public float lookPosMultiplicator;

    public float colorMultiplicator;
    public float particlesSpeedTrigger;

    void Start()
    {
        iniFOV = virtualCamera.m_Lens.FieldOfView;
        iniLookPos = lookAt.transform.localPosition;
    }

    void Update()
    {
        float carSpeed = carMovement.GetCarVelocity();
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView,iniFOV + (carSpeed * fovMultiplicator),lerpTime);
        lookAt.transform.localPosition = Vector3.Lerp(lookAt.transform.localPosition, iniLookPos + new Vector3(0, carSpeed * lookPosMultiplicator, 0), lerpTime);
        if (carSpeed > particlesSpeedTrigger) speedParticles.startColor = new Color(1, 1, 1, carSpeed * colorMultiplicator);
        else speedParticles.startColor = new Color(1, 1, 1, 0);
    }
}
