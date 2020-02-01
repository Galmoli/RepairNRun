using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolanteController : MonoBehaviour
{

    public float maxRotation;
    public float minRotation;
    public float rotationSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log(transform.localEulerAngles.y);
        if (hinput.anyGamepad.leftStick.right && (transform.localEulerAngles.y < maxRotation || transform.localEulerAngles.y >= minRotation - 5)) transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        else if (hinput.anyGamepad.leftStick.left && (transform.localEulerAngles.y > minRotation || transform.localEulerAngles.y <= maxRotation + 5)) transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        else
        {
            if(transform.localEulerAngles.y <= maxRotation+5 && transform.localEulerAngles.y > 5) transform.Rotate(0, -rotationSpeed * Time.deltaTime * 0.5f, 0);
            else if (transform.localEulerAngles.y >= minRotation - 5 && transform.localEulerAngles.y < 355) transform.Rotate(0, rotationSpeed * Time.deltaTime * 0.5f, 0);
        }
    }
}
