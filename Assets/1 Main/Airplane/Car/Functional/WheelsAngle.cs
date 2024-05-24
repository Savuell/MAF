using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsAngle : MonoBehaviour
{
    [SerializeField] private GameObject[] wheels = new GameObject[2];
    [SerializeField] private GameObject[] points = new GameObject[2];
    [SerializeField] private GameObject stick;
    [SerializeField] private float rotate;
    [SerializeField] private WheelCollider[] colliders = new WheelCollider[2];
    void Update()
    {
        stick.transform.position = (points[0].transform.position + points[1].transform.position) / 2;
        stick.transform.forward = points[1].transform.position - points[0].transform.position;
        rotate = Mathf.Max(Mathf.Min(rotate, 30), -30);
        foreach (WheelCollider collider in colliders) collider.steerAngle = -rotate;
        foreach (GameObject wheel in wheels) wheel.transform.localEulerAngles = new Vector3(wheel.transform.localEulerAngles.x, rotate, wheel.transform.localEulerAngles.z);
    }
}
