using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBlackboard : MonoBehaviour
{
    [Header("Broken Car")]
    public float brokenTireAngle;
    public float speedOnGrass = 10;
    public float speedBrokenEngine = 5;
    public float speedBrokenBothWheels = 6;
    public float speedAllBroken = 3;
    
    [Header("Car Speed")]
    public float maxSteerAngle = 45;
    public float maxMotorTorque = 100f;
    public float maxBreakTorque = 150f;
    public float maxSpeed = 100f;
    public float maxTimeOnGrass = 20f;
    
    [Header("Visual")] 
    public float wheelVisualAngle = 20f;

    [Header("Transforms")] 
    public Transform grassChecker;
}
