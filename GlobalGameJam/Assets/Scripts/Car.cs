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
    private float speed;
    private CarMovement _carMovement;
    private List<BrokenPart> _brokenParts = new List<BrokenPart>();

    private void Awake()
    {
        _carMovement = GetComponent<CarMovement>();
        speed = _carMovement.maxSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) BreakLeftWheel();
        if(Input.GetKeyDown(KeyCode.R)) BreakRightWheel();
        if(Input.GetKeyDown(KeyCode.E)) BreakEngine();
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
            _carMovement.maxSpeed = brokenEngineSpeed;
        }
        else _carMovement.maxSpeed = speed;
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
