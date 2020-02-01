using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{

    [SerializeField] private Transform path;
    [SerializeField] private float maxSteerAngle = 45;
    [SerializeField] private float maxMotorTorque = 100f;
    [SerializeField] private float maxBreakTorque = 150f;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private WheelCollider wheelFL;
    [SerializeField] private WheelCollider wheelFR;
    [SerializeField] private WheelCollider wheelRL;
    [SerializeField] private WheelCollider wheelRR;
    private List<Transform> nodes;
    private int currentNode = 0;
    private float currentSpeed;
    private bool isBreaking;
    private bool avoiding;

    [Header("Sensors")] 
    [SerializeField] private float sensorLength = 5f;
    [SerializeField] private float frontSensorAngle = 30;

    [SerializeField] private Transform centerSensor;
    [SerializeField] private Transform rightSensor;
    [SerializeField] private Transform leftSensor;
    // Start is called before the first frame update
    void Start()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        foreach (var t in pathTransforms)
        {
            if (t != path.transform)
            {
                nodes.Add(t);
            }
        }
    }

    private void FixedUpdate()
    {
        Sensors();
        ApplySteer();
        Breaking();
        Drive();
        CheckWayPointDistance();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void ApplySteer()
    {
        if(avoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Sensors()
    {
        RaycastHit hit;
        float avoidMultiplier = 0;
        avoiding = false;

        //Front Right sensor
        if (Physics.Raycast(rightSensor.position, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(rightSensor.position, rightSensor.position + transform.forward * sensorLength);
            avoiding = true;
            avoidMultiplier -= 1f;
        }

        //Diagonal Right sensor
        if (Physics.Raycast(rightSensor.position, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(rightSensor.position, rightSensor.position + Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward * sensorLength);
            avoiding = true;
            avoidMultiplier -= 0.5f;
        }

        //Front Left sensor
        if (Physics.Raycast(leftSensor.position, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(leftSensor.position, leftSensor.position + transform.forward * sensorLength);
            avoiding = true;
            avoidMultiplier += 1f;
        }

        //Diagonal Left sensor
        if (Physics.Raycast(leftSensor.position, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(leftSensor.position, leftSensor.position + Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward * sensorLength);
            avoiding = true;
            avoidMultiplier += 0.5f;
        }
        
        //Front Center Sensor
        if (avoidMultiplier == 0)
        {
            if (Physics.Raycast(centerSensor.position, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(centerSensor.position, centerSensor.position + transform.forward * sensorLength);
                avoiding = true;
                if (hit.normal.x < 0)
                {
                    avoidMultiplier = -1f;
                }
                else
                {
                    avoidMultiplier = 1f;
                }
            }
        }

        if (avoiding)
        {
            wheelFL.steerAngle = maxSteerAngle * avoidMultiplier;
            wheelFR.steerAngle = maxSteerAngle * avoidMultiplier;
        }
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        if (currentSpeed < maxSpeed && !isBreaking)
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

    private void CheckWayPointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.5f)
        {
            if (currentNode == nodes.Count - 1) currentNode = 0;
            else currentNode++;
        }
    }
}
