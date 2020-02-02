using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarCollisions : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    private Car _car;
    private float timeSinceLastBrokenPiece = 0;
    private float nextPieceWillBreakIn;

    private void Awake()
    {
        _car = GetComponentInParent<Car>();
    }

    private void Start()
    {
        nextPieceWillBreakIn = Random.Range(15, 21);
    }

    private void Update()
    {
        timeSinceLastBrokenPiece += Time.deltaTime;
        if (timeSinceLastBrokenPiece >= nextPieceWillBreakIn)
        {
            Break();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LapChecker")) GetComponentInParent<LapManager>().CheckLap();
        if(isPlayer) if(other.CompareTag("Enemy")) Break();
    }

    private void Break()
    {
        if (isPlayer)
        {
            hinput.anyGamepad.Vibrate(0.55f, 0.25f, 0.5f);
            SoundManager sm = FindObjectOfType<SoundManager>();
            if (!sm.carCrash.isPlaying) sm.carCrash.Play();
        }
        if (_car._brokenParts.Count == 3) return; //All things broken
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
        
        timeSinceLastBrokenPiece = 0;
        nextPieceWillBreakIn = Random.Range(15, 21);
    }
}
