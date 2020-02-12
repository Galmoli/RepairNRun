using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour, ICar
{
    [Header("Torques")] 
    [SerializeField] private float fastTorque;
    [SerializeField] private float defaultTorque;
    [SerializeField] private float slowTorque;

    [Header("Wheels")]
    [SerializeField] private Transform path;
    [SerializeField] private WheelCollider wheelFL;
    [SerializeField] private WheelCollider wheelFR;
    [SerializeField] private WheelCollider wheelRL;
    [SerializeField] private WheelCollider wheelRR;
    [SerializeField] private GameObject rightWheel;
    [SerializeField] private GameObject leftWheel;
    [HideInInspector] public float steerVariance;
    
    private List<Transform> nodes;
    private CarBlackboard _blackboard;
    
    private float currentSpeed;
    private float currentTimeOnGrass;
    private int currentNode = 0;
    private bool isBreaking;
    private bool avoiding;
    private Vector3 lastNodePos;
    private Rigidbody rb;

    [Header("Sensors")] 
    [SerializeField] private float sensorLength = 5f;
    [SerializeField] private float frontSensorAngle = 30;

    [SerializeField] private Transform centerSensor;
    [SerializeField] private Transform rightSensor;
    [SerializeField] private Transform leftSensor;
    // Start is called before the first frame update
    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        _blackboard = GetComponent<CarBlackboard>();
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        foreach (var t in pathTransforms)
        {
            if (t != path.transform) nodes.Add(t);
        }
    }

    private void FixedUpdate()
    {
        Sensors();
        ApplySteer();
        Break();
        Drive();
        CheckWayPointDistance();
    }

    public void ApplySteer()
    {
        if(avoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * _blackboard.maxSteerAngle;
        newSteer += steerVariance;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
        
        rightWheel.transform.localRotation = Quaternion.AngleAxis(newSteer /2, Vector3.up);
        leftWheel.transform.localRotation = Quaternion.AngleAxis(newSteer /2, Vector3.up);
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

        if (Physics.Raycast(transform.position, Vector3.down, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Grass"))
            {
                currentTimeOnGrass += Time.deltaTime;
                if (currentTimeOnGrass >= _blackboard.maxTimeOnGrass)
                {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    transform.position = lastNodePos;
                }
            }
            else currentTimeOnGrass = 0;
        }

        if (avoiding)
        {
            wheelFL.steerAngle = _blackboard.maxSteerAngle * avoidMultiplier;
            wheelFR.steerAngle = _blackboard.maxSteerAngle * avoidMultiplier;
        }
    }

    public void Drive()
    {
        currentSpeed = rb.velocity.magnitude;
        if (currentSpeed < _blackboard.maxSpeed && !isBreaking)
        {
            wheelFL.motorTorque = _blackboard.maxMotorTorque;
            wheelFR.motorTorque = _blackboard.maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }

    public void Break()
    {
        if (isBreaking)
        {
            wheelRL.brakeTorque = _blackboard.maxBreakTorque;
            wheelRR.brakeTorque = _blackboard.maxBreakTorque;
        }
        else
        {
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
    }

    private void CheckWayPointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 10f)
        {
            lastNodePos = nodes[currentNode].position;
            if (currentNode == nodes.Count - 1) currentNode = 0;
            else currentNode++;
            AdaptVelocityToPlayerNode();
        }
    }

    private void AdaptVelocityToPlayerNode()
    {
        if (currentNode - 1 < path.GetComponent<Path>().GetLastAchievedNode())
        {
            _blackboard.maxMotorTorque = fastTorque;
        }
        else if (currentNode - 2 > path.GetComponent<Path>().GetLastAchievedNode())
        {
            _blackboard.maxMotorTorque = slowTorque;
        }
        else _blackboard.maxMotorTorque = defaultTorque;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LapChecker")) GetComponent<LapManager>().CheckLap();
    }
}
