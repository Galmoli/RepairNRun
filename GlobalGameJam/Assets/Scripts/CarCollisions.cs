using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarCollisions : MonoBehaviour
{ 
    private Car _car;
    private CarBlackboard _blackboard;
    private RaycastHit hit;
    private float timeSinceLastBrokenPiece = 0;
    private float nextPieceWillBreakIn;
    private bool canTrigger = true;
    public ParticleSystem collisionParticles;

    private void Awake()
    {
        _car = GetComponentInParent<Car>();
        _blackboard = GetComponent<CarBlackboard>();
    }

    private void Start()
    {
        nextPieceWillBreakIn = Random.Range(20, 30);
    }

    private void Update()
    {
        timeSinceLastBrokenPiece += Time.deltaTime;
        if (timeSinceLastBrokenPiece >= nextPieceWillBreakIn) Break();
        if(Physics.Raycast(_blackboard.grassChecker.position, Vector3.down, out hit,  3))
        {
            if (hit.collider.gameObject.CompareTag("Grass"))
            {
                if (!_car.sManager.throughGrass.isPlaying) _car.sManager.throughGrass.Play();
                _car.isInGrass = true;
            }
            else
            {
                _car.isInGrass = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LapChecker")) GetComponentInParent<LapManager>().CheckLap();
        if(other.CompareTag("Enemy")) Break();
    }

    private void Break()
    {
        //if (!canTrigger) return;
        hinput.anyGamepad.Vibrate(0.55f, 0.25f, 0.5f);
        SoundManager sm = FindObjectOfType<SoundManager>();
        if (!canTrigger || _car._brokenParts.Count == 3)
        {
            if (timeSinceLastBrokenPiece < nextPieceWillBreakIn)
            {
                sm.carCrash.Play();
                collisionParticles.Play();
            }
            timeSinceLastBrokenPiece = 0;
            nextPieceWillBreakIn = Random.Range(20, 30);
            return;
        }
        sm.carCrash.Play();
        collisionParticles.Play();
        //if (!sm.carCrash.isPlaying) sm.carCrash.Play();
        //if (_car._brokenParts.Count == 3) return; //All things broken
        bool broken = false;
        do
        {
            var r = Random.Range(0, 3);
            switch (r)
            {
                case 0:
                {
                    if (!_car._brokenParts.Contains(Car.BrokenPart.Engine))
                    {
                        _car.BreakEngine();
                        broken = true;
                    }
                    break;
                }
                case 1:
                {
                    if (!_car._brokenParts.Contains(Car.BrokenPart.RightWheel))
                    {
                        _car.BreakRightWheel();
                        broken = true;
                    }
                    break;
                }
                case 2:
                {
                    if (!_car._brokenParts.Contains(Car.BrokenPart.LeftWheel))
                    {
                        _car.BreakLeftWheel();
                        broken = true;
                    }
                    break;
                }
            }
        } while (!broken);
        StopAllCoroutines();
        canTrigger = false;
        StartCoroutine(TriggerCountdown());
        
        timeSinceLastBrokenPiece = 0;
        nextPieceWillBreakIn = Random.Range(20, 30);
    }

    private IEnumerator TriggerCountdown()
    {
        yield return new WaitForSeconds(0.5f);
        canTrigger = true;
    }
}
