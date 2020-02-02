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
    [SerializeField] private float turnAngle = 50f;
    [SerializeField] private float wheelVisualAngle = 20f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    [SerializeField] private WheelCollider wheelRL;
    [SerializeField] private WheelCollider wheelRR;
    [SerializeField] private GameObject leftWheel;
    [SerializeField] private GameObject rightWheel;
    [HideInInspector] public float steerVariance = 0;
    private float avoidMultiplier;
    private bool isBreaking;
    private bool isAccelerating;
    private bool backwards;
    private Direction currentDirection = Direction.None;
    private Rigidbody rb;
    [HideInInspector] public SoundManager sManager;

    private Animator anim;
    private Car car;
    public ParticleSystem accelerateParticles;
    public ParticleSystem accelerateGrassParticles;
    public ParticleSystem vroom;

    private void Awake()
    {
        car = GetComponent<Car>();
        rb = GetComponent<Rigidbody>();
        sManager = FindObjectOfType<SoundManager>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        ApplySteer();
        Drive();
        Breaking();
    }

    // Update is called once per frame
    private void Update()
    {

        if (hinput.anyGamepad.leftStick.right) currentDirection = Direction.Right;
        if (hinput.anyGamepad.leftStick.left) currentDirection = Direction.Left;
        if (hinput.anyGamepad.leftStick.inDeadZone) currentDirection = Direction.None;
        if (hinput.anyGamepad.rightTrigger.pressed) isAccelerating = true;
        else isAccelerating = false;
        if (hinput.anyGamepad.leftTrigger.pressed)
        {
            isBreaking = true;
            var velocity = rb.velocity;
            var localVel = transform.InverseTransformDirection(velocity);
            if (localVel.z <= 0.01f)
            {
                isBreaking = false;
                backwards = true;
            }
        }
        else
        {
            isBreaking = false;
            backwards = false;
        }
        //if (!accelerateParticles.isPlaying) accelerateParticles.Play();
        if (isAccelerating || backwards || isBreaking)
        {
            if (!vroom.isPlaying) vroom.Play();
            anim.SetBool("run", true);
        }
        else
        {
            vroom.Stop();
            anim.SetBool("run", false);
        }
        if (rb.velocity.magnitude > 0.5f) 
        {
            if (car.isInGrass)
            {
                if (!accelerateGrassParticles.isPlaying) accelerateGrassParticles.Play();
                accelerateParticles.Stop();
            }
            else
            {
                if (!accelerateParticles.isPlaying) accelerateParticles.Play();
                accelerateGrassParticles.Stop();
            }
        }
        else
        {
            accelerateParticles.Stop();
            accelerateParticles.Stop();
        }
    }

    private void ApplySteer()
    {
        float newSteer = 0;
        switch (currentDirection)
        {
            case Direction.Right:
                newSteer = turnAngle;
                rightWheel.transform.localRotation = Quaternion.AngleAxis(wheelVisualAngle, Vector3.up);
                leftWheel.transform.localRotation = Quaternion.AngleAxis(wheelVisualAngle, Vector3.up);
                break;
            case Direction.Left:
                newSteer = -turnAngle;
                rightWheel.transform.localRotation = Quaternion.AngleAxis(-wheelVisualAngle, Vector3.up);
                leftWheel.transform.localRotation = Quaternion.AngleAxis(-wheelVisualAngle, Vector3.up);
                break;
            case Direction.None:
                rightWheel.transform.localRotation = Quaternion.AngleAxis(0, Vector3.up);
                leftWheel.transform.localRotation = Quaternion.AngleAxis(0, Vector3.up);
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
        else if(!backwards)
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }

    private void Breaking()
    {
        if (isBreaking)
        {
            wheelFL.brakeTorque = maxBreakTorque;
            wheelFR.brakeTorque = maxBreakTorque;
            wheelRL.brakeTorque = maxBreakTorque;
            wheelRR.brakeTorque = maxBreakTorque;
        }
        else
        {
            wheelFL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
        
        if (backwards)
        {
            if (!sManager.backwardsBeep.isPlaying) sManager.backwardsBeep.Play();
            wheelFL.motorTorque = -maxMotorTorque;
            wheelFR.motorTorque = -maxMotorTorque;
        }
        else if(!isAccelerating)
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }
}
