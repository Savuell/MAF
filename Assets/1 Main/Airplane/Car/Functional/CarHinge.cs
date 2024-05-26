using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHinge : MonoBehaviour
{
    [SerializeField] private bool invert;
    [SerializeField] private float openSpeed, closeSpeed;
    [SerializeField] private float closedAngle, openedAngle;
    public bool isOpen;
    public bool opened;
    void Update()
    {
        if (Mathf.Abs(transform.localEulerAngles.z - closedAngle) < 0.1f) opened = false;
        else opened = true;
        if (isOpen) transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles - new Vector3(0, 0, invert ? 360 : 0), new Vector3(0, 0, openedAngle), openSpeed);
        else transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles - new Vector3(0, 0, invert ? 360 : 0), new Vector3(0, 0, closedAngle), closeSpeed);
    }
}
