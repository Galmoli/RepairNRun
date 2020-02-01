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

    [SerializeField] private float brokenEngineSpeed;
    [SerializeField] private float brokenTireAngle;
    [SerializeField] private float maxTorqueOnGrass = 200;
    private float maxTorque;
    private CarAI _carMovement;
    private List<BrokenPart> _brokenParts = new List<BrokenPart>();
    private RaycastHit hit;
    private bool isInGrass;

    private void Awake()
    {
        _carMovement = GetComponent<CarAI>();
        maxTorque = _carMovement.maxMotorTorque;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) BreakLeftWheel();
        if(Input.GetKeyDown(KeyCode.R)) BreakRightWheel();
        if(Input.GetKeyDown(KeyCode.E)) BreakEngine();
        
        if(Physics.Raycast(transform.position, Vector3.down, out hit,  3))
        {
            if (hit.collider.gameObject.name == "cespedA" || hit.collider.gameObject.name == "CespedB")
            {
                isInGrass = true;
                _carMovement.maxMotorTorque = maxTorqueOnGrass;
            }
            else
            {
                isInGrass = false;
                _carMovement.maxMotorTorque = maxTorque;
            }
        }
    }

    void FixedUpdate()
    {
        if (_brokenParts.Contains(BrokenPart.LeftWheel))
        {
            
        }

        if (_brokenParts.Contains(BrokenPart.RightWheel))
        {
            
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_brokenParts.Contains(BrokenPart.Engine))
        {
            _carMovement.maxMotorTorque = brokenEngineSpeed;
        }
        else if(!isInGrass) _carMovement.maxMotorTorque = maxTorque;
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
