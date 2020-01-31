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
    [SerializeField] private float acceleration;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float wheelRotation;
    [SerializeField] private GameObject leftWheel;
    [SerializeField] private GameObject rightWheel;
    [SerializeField] private CameraManager _cameraManager;
    private float currentSpeed;
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
        if (hinput.anyGamepad.rightTrigger)
        {
            if (currentSpeed <= speed) currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            if (currentSpeed > 0) currentSpeed -= acceleration * 0.5f * Time.deltaTime;
            if (currentSpeed < 0) currentSpeed += acceleration * 0.5f * Time.deltaTime;
        }
        if (hinput.anyGamepad.leftTrigger)
        {
            if (currentSpeed >= -speed) currentSpeed -= acceleration * 2 * Time.deltaTime;
        }
        _velocity = transform.forward * currentSpeed;
        
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
        if(_velocity.magnitude < 0.05f) _velocity = Vector3.zero;
        _rb.velocity = _velocity;
        if (carDirection == Direction.Left && _velocity.magnitude > 0.75f)
        {
            if (currentSpeed > 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
                                                   transform.rotation.eulerAngles.y - rotationSpeed * Time.deltaTime, 
                                                      transform.rotation.eulerAngles.z);
            }
            else
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
                                                   transform.rotation.eulerAngles.y + rotationSpeed * Time.deltaTime, 
                                                      transform.rotation.eulerAngles.z);
            }
            
        }

        if (carDirection == Direction.Right && _velocity.magnitude > 0.75f)
        {
            if (currentSpeed > 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
                    transform.rotation.eulerAngles.y + rotationSpeed * Time.deltaTime, 
                    transform.rotation.eulerAngles.z);
            }
            else
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
                    transform.rotation.eulerAngles.y - rotationSpeed * Time.deltaTime, 
                    transform.rotation.eulerAngles.z);
            }
        }
    }
}
