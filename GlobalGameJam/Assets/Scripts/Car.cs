using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Car : MonoBehaviour
{
    enum BrokenPart
    {
        LeftWheel,
        RightWheel,
        Engine
    }

    [SerializeField] private bool isPlayer;
    [SerializeField] private float brokenTireAngle;
    [SerializeField] private float speedOnGrass = 10;
    [SerializeField] private float speedBrokenEngine = 5;
    private CarAI _carAIMove;
    private CarMovement _carMovement;
    private List<BrokenPart> _brokenParts = new List<BrokenPart>();
    private RaycastHit hit;
    private Rigidbody _rb;
    private bool isInGrass;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if (isPlayer)
        {
            _carMovement = GetComponent<CarMovement>();
            //maxTorque = _carMovement.maxMotorTorque;
        }
        else
        {
            _carAIMove = GetComponent<CarAI>();
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) BreakLeftWheel();
        if(Input.GetKeyDown(KeyCode.R)) BreakRightWheel();
        if(Input.GetKeyDown(KeyCode.E)) BreakEngine();

        if (Input.GetKeyDown(KeyCode.S))
        {
            RepairEngine();
            RepairLeftWheel();
            RepairRightWheel();
        }
        
        if(Physics.Raycast(transform.position, Vector3.down, out hit,  3))
        {
            if (hit.collider.gameObject.name == "cespedA" || hit.collider.gameObject.name == "CespedB")
            {
                isInGrass = true;
                _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, speedOnGrass);
            }
            else
            {
                isInGrass = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (_brokenParts.Contains(BrokenPart.LeftWheel))
        {
            if (isPlayer) _carMovement.steerVariance = -brokenTireAngle;
            else _carAIMove.steerVariance = -brokenTireAngle;
        }
        else
        {
            if (isPlayer) _carMovement.steerVariance = 0;
            else _carAIMove.steerVariance = 0;
        }

        if (_brokenParts.Contains(BrokenPart.RightWheel))
        {
            if (isPlayer) _carMovement.steerVariance = brokenTireAngle;
            else _carAIMove.steerVariance = brokenTireAngle;
        }
        else
        {
            if (isPlayer) _carMovement.steerVariance = 0;
            else _carAIMove.steerVariance = 0;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_brokenParts.Contains(BrokenPart.Engine))
        {
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, speedBrokenEngine);
        }
    }

    public void BreakEngine()
    {
        if(!_brokenParts.Contains(BrokenPart.Engine)) _brokenParts.Add(BrokenPart.Engine);
    }

    public void RepairEngine()
    {
        if (_brokenParts.Contains(BrokenPart.Engine)) _brokenParts.Remove(BrokenPart.Engine);
    }

    public void BreakLeftWheel()
    {
        if (!_brokenParts.Contains(BrokenPart.LeftWheel)) _brokenParts.Add(BrokenPart.LeftWheel);
    }

    public void RepairLeftWheel()
    {
        if (_brokenParts.Contains(BrokenPart.LeftWheel)) _brokenParts.Remove(BrokenPart.LeftWheel);
    }

    public void BreakRightWheel()
    {
        if (!_brokenParts.Contains(BrokenPart.RightWheel)) _brokenParts.Add(BrokenPart.RightWheel);
    }

    public void RepairRightWheel()
    {
        if (_brokenParts.Contains(BrokenPart.RightWheel)) _brokenParts.Remove(BrokenPart.RightWheel);
    }
}
