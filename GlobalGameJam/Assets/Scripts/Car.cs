using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Car : MonoBehaviour
{
    public enum BrokenPart
    {
        LeftWheel,
        RightWheel,
        Engine
    }

    [SerializeField] private bool isPlayer;
    [SerializeField] private float brokenTireAngle;
    [SerializeField] private float speedOnGrass = 10;
    [SerializeField] private float speedBrokenEngine = 5;
    [SerializeField] private float speedBrokenBothWheels = 6;
    [SerializeField] private float speedAllBroken = 3;
    private ObjectSpawner cableManager;
    private CarAI _carAIMove;
    private CarMovement _carMovement;
    [HideInInspector] public List<BrokenPart> _brokenParts = new List<BrokenPart>();
    private RaycastHit hit;
    private Rigidbody _rb;
    private bool isInGrass;
    [HideInInspector] public SoundManager sManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if (isPlayer)
        {
            _carMovement = GetComponent<CarMovement>();
            cableManager = GetComponentInChildren<ObjectSpawner>();
        }
        else
        {
            _carAIMove = GetComponent<CarAI>();
        }
        sManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || hinput.anyGamepad.rightBumper)
        {
            RepairEngine();
            RepairLeftWheel();
            RepairRightWheel();
        }
        
        if(Physics.Raycast(transform.position, Vector3.down, out hit,  3))
        {
            if (hit.collider.gameObject.name == "cespedA" || hit.collider.gameObject.name == "CespedB")
            {
                if (!sManager.throughGrass.isPlaying && this.isPlayer) sManager.throughGrass.Play();
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
        if (_brokenParts.Contains(BrokenPart.LeftWheel) && _brokenParts.Contains(BrokenPart.RightWheel))
        {
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, speedBrokenBothWheels);
            return;
        }
        if (_brokenParts.Contains(BrokenPart.LeftWheel))
        {
            if (isPlayer) _carMovement.steerVariance = -brokenTireAngle;
            else _carAIMove.steerVariance = -brokenTireAngle;
            return;
        }

        if (_brokenParts.Contains(BrokenPart.RightWheel))
        {
            if (isPlayer) _carMovement.steerVariance = brokenTireAngle;
            else _carAIMove.steerVariance = brokenTireAngle;
            return;
        }
        
        if (isPlayer) _carMovement.steerVariance = 0;
        else _carAIMove.steerVariance = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_brokenParts.Count == 3)
        {
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, speedAllBroken);
            return;
        }
        if (_brokenParts.Contains(BrokenPart.Engine))
        {
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, speedBrokenEngine);
        }
    }

    public void BreakEngine()
    {
        if (!isPlayer) return;
        if(!_brokenParts.Contains(BrokenPart.Engine)) _brokenParts.Add(BrokenPart.Engine);
        if (!sManager.embrague.isPlaying && this.isPlayer) sManager.embrague.Play();
        cableManager.SpawnProblem(2);
    }

    public void RepairEngine()
    {
        if (_brokenParts.Contains(BrokenPart.Engine)) _brokenParts.Remove(BrokenPart.Engine);
        if (!sManager.fixedThat.isPlaying && this.isPlayer) sManager.fixedThat.Play();
    }

    public void BreakLeftWheel()
    {
        if (!isPlayer) return;
        if (!_brokenParts.Contains(BrokenPart.LeftWheel)) _brokenParts.Add(BrokenPart.LeftWheel);
        if (!sManager.wheelPinchazo.isPlaying && this.isPlayer) sManager.wheelPinchazo.Play();
        cableManager.SpawnProblem(0);
    }

    public void RepairLeftWheel()
    {
        if (_brokenParts.Contains(BrokenPart.LeftWheel)) _brokenParts.Remove(BrokenPart.LeftWheel);
        if (!sManager.fixedThat.isPlaying && this.isPlayer) sManager.fixedThat.Play();
    }

    public void BreakRightWheel()
    {
        if (!isPlayer) return;
        if (!_brokenParts.Contains(BrokenPart.RightWheel)) _brokenParts.Add(BrokenPart.RightWheel);
        if (!sManager.wheelPinchazo.isPlaying && this.isPlayer) sManager.wheelPinchazo.Play();
        cableManager.SpawnProblem(1);
    }

    public void RepairRightWheel()
    {
        if (_brokenParts.Contains(BrokenPart.RightWheel)) _brokenParts.Remove(BrokenPart.RightWheel);
        if (!sManager.fixedThat.isPlaying && this.isPlayer) sManager.fixedThat.Play();
    }
}
