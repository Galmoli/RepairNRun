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
    [SerializeField] private float maxSteerAngle = 45;
    [SerializeField] private float maxMotorTorque = 100f;
    [SerializeField] private float maxBreakTorque = 150f;
    public float maxSpeed = 100f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    [SerializeField] private WheelCollider wheelRL;
    [SerializeField] private WheelCollider wheelRR;
    private float avoidMultiplier;
    private float currentSpeed;
    private bool isBreaking;
    private bool isAccelerating;
    private Direction currentDirection = Direction.None;

    [Header("Sensors")] 
    [SerializeField] private float sensorLength = 5f;
    [SerializeField] private float frontSensorAngle = 30;
    [SerializeField] private Transform centerSensor;
    [SerializeField] private Transform rightSensor;
    [SerializeField] private Transform leftSensor;

    private void FixedUpdate()
    {
        ApplySteer();
        Breaking();
        Drive();
    }

    // Update is called once per frame
    private void Update()
    {
        if (hinput.anyGamepad.leftStick.right) currentDirection = Direction.Right;
        if (hinput.anyGamepad.leftStick.left) currentDirection = Direction.Left;
        if (hinput.anyGamepad.leftStick.inDeadZone) currentDirection = Direction.None;
        if (hinput.anyGamepad.rightTrigger.pressed) isAccelerating = true;
        else isAccelerating = false;
        if (hinput.anyGamepad.leftTrigger.pressed) isBreaking = true;
        else isBreaking = false;
    }

    private void ApplySteer()
    {
        float newSteer = 0;
        switch (currentDirection)
        {
            case Direction.Right:
                newSteer = 45;
                break;
            case Direction.Left:
                newSteer = -45;
                break;
            case Direction.None:
                newSteer = 0;
                break;
        }
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        if (isAccelerating && !isBreaking)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }

    private void Breaking()
    {
        if (isBreaking)
        {
            wheelRL.brakeTorque = maxBreakTorque;
            wheelRR.brakeTorque = maxBreakTorque;
        }
        else
        {
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
        
    }
}
