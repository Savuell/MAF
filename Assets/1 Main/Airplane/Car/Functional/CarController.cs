using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private SteeringWheel steeringWheel;
    [SerializeField] private WheelsAngle wheels;
    void Update()
    {
        wheels.rotate = steeringWheel.steerAngle/20;
    }
}
