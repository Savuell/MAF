using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private SteeringWheel steeringWheel;
    [SerializeField] private WheelsAngle wheels;
    [SerializeField] private GameObject tech, techMain, underHood;
    [SerializeField] private CarHinge hood;
    void Update()
    {
        wheels.rotate = steeringWheel.steerAngle/20;
        ShowTech(hood.opened||hood.isOpen);
    }
    void ShowTech(bool hide)
    {
        tech.SetActive(hide);
        techMain.SetActive(hide);
        underHood.SetActive(hide);
    }
}
