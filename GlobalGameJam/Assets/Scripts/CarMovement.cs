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
    
    public float maxMotorTorque = 100f;
    [SerializeField] private float maxBreakTorque = 150f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    [SerializeField] private WheelCollider wheelRL;
    [SerializeField] private WheelCollider wheelRR;
    [HideInInspector] public float steerVariance = 0;
    private float avoidMultiplier;
    private bool isBreaking;
    private bool isAccelerating;
    private Direction currentDirection = Direction.None;

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
        newSteer += steerVariance;
        
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
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
