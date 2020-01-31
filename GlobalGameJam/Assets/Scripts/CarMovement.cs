using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private enum Direction
    {
        Right,
        Left,
        None
    }
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float wheelRotation;
    [SerializeField] private GameObject leftWheel;
    [SerializeField] private GameObject rightWheel;
    [SerializeField] private CameraManager _cameraManager;
    private Vector3 _velocity;
    private Rigidbody _rb;
    private Direction carDirection = Direction.None;

    private void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody>();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        //Forward
        if (hinput.anyGamepad.rightTrigger) _velocity = transform.forward * speed;
        else _velocity = Vector3.zero;
        if (hinput.anyGamepad.leftTrigger) _velocity = Vector3.zero;
        
        //Direction
        if(hinput.anyGamepad.leftStick.left)
        {
            if (_velocity != Vector3.zero) carDirection = Direction.Left;
            
            
            leftWheel.transform.localRotation = Quaternion.Euler(0, -wheelRotation, 0);
            rightWheel.transform.localRotation = Quaternion.Euler(0, -wheelRotation, 0);
        }
        if(hinput.anyGamepad.leftStick.right)
        {
            if (_velocity != Vector3.zero) carDirection = Direction.Right;
            
            leftWheel.transform.localRotation = Quaternion.Euler(0, wheelRotation, 0);
            rightWheel.transform.localRotation = Quaternion.Euler(0, wheelRotation, 0);
        }

        if (hinput.anyGamepad.leftStick.inDeadZone)
        {
            leftWheel.transform.localRotation = Quaternion.Euler(Vector3.zero);
            rightWheel.transform.localRotation = Quaternion.Euler(Vector3.zero);
            carDirection = Direction.None;
        }
        
        _cameraManager.SetVelocity(_velocity);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _velocity;
        if (carDirection == Direction.Left)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
                                               transform.rotation.eulerAngles.y - rotationSpeed * Time.deltaTime, 
                                                  transform.rotation.eulerAngles.z);
        }

        if (carDirection == Direction.Right)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
                                               transform.rotation.eulerAngles.y + rotationSpeed * Time.deltaTime, 
                                                  transform.rotation.eulerAngles.z);
        }
    }
}
