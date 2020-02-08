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
    
    [HideInInspector] public List<BrokenPart> _brokenParts = new List<BrokenPart>();
    [HideInInspector] public bool isInGrass;
    [HideInInspector] public bool raceFinished = false;
    [HideInInspector] public SoundManager sManager;
    [SerializeField] private ObjectSpawner cableManager;
    
    private CarMovement _carMovement;
    private CarBlackboard _blackboard;
    
    private RaycastHit hit;
    private Rigidbody _rb;

    public ParticleSystem brokenMotorParticles;
    public ParticleSystem brokenWheelRightParticles;
    public ParticleSystem brokenWheelLeftParticles;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _blackboard = GetComponent<CarBlackboard>();
        _carMovement = GetComponent<CarMovement>();
        sManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        if(Physics.Raycast(_blackboard.grassChecker.position, Vector3.down, out hit,  3))
        {
            if (hit.collider.gameObject.CompareTag("Grass"))
            {
                if (!sManager.throughGrass.isPlaying) sManager.throughGrass.Play();
                isInGrass = true;
                _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _blackboard.speedOnGrass);
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
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _blackboard.speedBrokenBothWheels);
            if (!brokenWheelRightParticles.isPlaying) brokenWheelRightParticles.Play();
            if(!brokenWheelLeftParticles.isPlaying)brokenWheelLeftParticles.Play();
            return;
        }
        if (_brokenParts.Contains(BrokenPart.LeftWheel))
        {
            _carMovement.steerVariance = -_blackboard.brokenTireAngle;
            if (!brokenWheelLeftParticles.isPlaying) brokenWheelLeftParticles.Play();
            return;
        }
        else  brokenWheelLeftParticles.Stop();

        if (_brokenParts.Contains(BrokenPart.RightWheel))
        {
            _carMovement.steerVariance = _blackboard.brokenTireAngle;
            if (!brokenWheelRightParticles.isPlaying) brokenWheelRightParticles.Play();
            return;
        }
        else brokenWheelRightParticles.Stop();

        _carMovement.steerVariance = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_brokenParts.Count == 3)
        {
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _blackboard.speedAllBroken);
            return;
        }
        if (_brokenParts.Contains(BrokenPart.Engine))
        {
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _blackboard.speedBrokenEngine);
            if (!brokenMotorParticles.isPlaying) brokenMotorParticles.Play();
        }
        else brokenMotorParticles.Stop();
    }

    public void BreakEngine()
    {
        if (raceFinished) return;
        if(!_brokenParts.Contains(BrokenPart.Engine)) _brokenParts.Add(BrokenPart.Engine);
        if (!sManager.embrague.isPlaying) sManager.embrague.Play();
        cableManager.SpawnProblem(2);
        hinput.anyGamepad.Vibrate(0.25f, 0.25f, 4f);
    }

    public void RepairEngine()
    {
        if (_brokenParts.Contains(BrokenPart.Engine)) _brokenParts.Remove(BrokenPart.Engine);
        if (!sManager.fixedThat.isPlaying) sManager.fixedThat.Play();
        hinput.anyGamepad.StopVibration();
    }

    public void BreakLeftWheel()
    {
        if (raceFinished) return;
        if (!_brokenParts.Contains(BrokenPart.LeftWheel)) _brokenParts.Add(BrokenPart.LeftWheel);
        if (!sManager.wheelPinchazo.isPlaying) sManager.wheelPinchazo.Play();
        cableManager.SpawnProblem(0);
        hinput.anyGamepad.Vibrate(0.5f, 0, 0.25f);
    }

    public void RepairLeftWheel()
    {
        if (_brokenParts.Contains(BrokenPart.LeftWheel)) _brokenParts.Remove(BrokenPart.LeftWheel);
        if (!sManager.fixedThat.isPlaying) sManager.fixedThat.Play();
    }

    public void BreakRightWheel()
    {
        if (raceFinished) return;
        if (!_brokenParts.Contains(BrokenPart.RightWheel)) _brokenParts.Add(BrokenPart.RightWheel);
        if (!sManager.wheelPinchazo.isPlaying) sManager.wheelPinchazo.Play();
        cableManager.SpawnProblem(1);
        hinput.anyGamepad.Vibrate(0, 0.5f, 0.25f);
    }

    public void RepairRightWheel()
    {
        if (_brokenParts.Contains(BrokenPart.RightWheel)) _brokenParts.Remove(BrokenPart.RightWheel);
        if (!sManager.fixedThat.isPlaying) sManager.fixedThat.Play();
    }

    public void EnableIA()
    {
        gameObject.GetComponent<CarAI>().enabled = true;
        gameObject.GetComponent<CarMovement>().enabled = false;
        this.enabled = false;
    }
}
