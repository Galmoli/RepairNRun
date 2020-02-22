using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour, ICar
{
    private enum Direction
    {
        Right,
        Left,
        None
    }
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    [SerializeField] private WheelCollider wheelRL;
    [SerializeField] private WheelCollider wheelRR;
    [SerializeField] private GameObject leftWheel;
    [SerializeField] private GameObject rightWheel;
    [HideInInspector] public float steerVariance = 0;
    [HideInInspector] public bool canMove = false;
    
    private float avoidMultiplier;
    private bool isBreaking;
    private bool isAccelerating;
    private bool backwards;
    
    private Direction currentDirection = Direction.None;
    private Rigidbody rb;
    [HideInInspector] public SoundManager sManager;

    private Animator anim;
    private Car car;
    private CarBlackboard _blackboard;
    public ParticleSystem accelerateParticles;
    public ParticleSystem accelerateGrassParticles;
    public ParticleSystem vroom;
    public ParticleSystem breakParticles;

    private void Awake()
    {
        car = GetComponent<Car>();
        rb = GetComponent<Rigidbody>();
        sManager = FindObjectOfType<SoundManager>();
        anim = GetComponent<Animator>();
        _blackboard = GetComponent<CarBlackboard>();
    }
    private void FixedUpdate()
    {
        ApplySteer();
        Drive();
        Break();
    }

    // Update is called once per frame
    private void Update()
    {
        if (hinput.anyGamepad.leftStick.right) currentDirection = Direction.Right;
        if (hinput.anyGamepad.leftStick.left) currentDirection = Direction.Left;
        if (hinput.anyGamepad.leftStick.inDeadZone) currentDirection = Direction.None;
        if (hinput.anyGamepad.rightTrigger.pressed) isAccelerating = true;
        else isAccelerating = false;

        //COSAS CHUNGAS MADE BY DANIRIWEZ
        if (Input.GetKey(KeyCode.D)) currentDirection = Direction.Right;
        else if (Input.GetKey(KeyCode.A)) currentDirection = Direction.Left;
        else currentDirection = Direction.None;
        if (Input.GetKey(KeyCode.W)) isAccelerating = true;
        else isAccelerating = false;
        ////////////////////////////////////
        if (hinput.anyGamepad.leftTrigger.pressed || Input.GetKey(KeyCode.S))//MAS COSAS CHUNGAS
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
        if (isAccelerating || backwards || isBreaking)
        {
            if (!vroom.isPlaying) vroom.Play();
            if (isBreaking)
            {
                Debug.Log("isBreaking");
                vroom.Stop();
                breakParticles.Play();
            }
            else breakParticles.Stop();
            anim.SetBool("run", true);
        }
        else
        {
            vroom.Stop();
            breakParticles.Stop();
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

    public void ApplySteer()
    {
        float newSteer = 0;
        switch (currentDirection)
        {
            case Direction.Right:
                newSteer = _blackboard.maxSteerAngle;
                rightWheel.transform.localRotation = Quaternion.AngleAxis(_blackboard.wheelVisualAngle, Vector3.up);
                leftWheel.transform.localRotation = Quaternion.AngleAxis(_blackboard.wheelVisualAngle, Vector3.up);
                break;
            case Direction.Left:
                newSteer = -_blackboard.maxSteerAngle;
                rightWheel.transform.localRotation = Quaternion.AngleAxis(-_blackboard.wheelVisualAngle, Vector3.up);
                leftWheel.transform.localRotation = Quaternion.AngleAxis(-_blackboard.wheelVisualAngle, Vector3.up);
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

    public void Drive()
    {
        if (!canMove) return;
        if (isAccelerating && !isBreaking)
        {
            wheelFL.motorTorque = _blackboard.maxMotorTorque;
            wheelFR.motorTorque = _blackboard.maxMotorTorque;
            if (!sManager.acceleration.isPlaying) sManager.acceleration.Play();
        }
        else if(!backwards)
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
            if (sManager.acceleration.isPlaying) sManager.acceleration.Stop();
        }
    }

    public void Break()
    {
        if (!canMove) return;
        if (isBreaking)
        {
            wheelFL.brakeTorque = _blackboard.maxBreakTorque;
            wheelFR.brakeTorque = _blackboard.maxBreakTorque;
            wheelRL.brakeTorque = _blackboard.maxBreakTorque;
            wheelRR.brakeTorque = _blackboard.maxBreakTorque;
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
            wheelFL.motorTorque = -_blackboard.maxMotorTorque;
            wheelFR.motorTorque = -_blackboard.maxMotorTorque;
        }
        else if(!isAccelerating)
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }

    public float GetCarVelocity()
    {
        return rb.velocity.magnitude;
    }
}
