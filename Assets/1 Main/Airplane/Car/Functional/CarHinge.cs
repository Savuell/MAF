using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHinge : MonoBehaviour
{
    [SerializeField] private bool invert;
    [SerializeField] private float openSpeed, closeSpeed;
    [SerializeField] private float closedAngle, openedAngle;
    [SerializeField] private bool playDoorSound;
    [SerializeField] private AudioSource openSound, closeSound;
    public bool isOpen;
    public bool opened;
    private bool preOpen;
    void FixedUpdate()
    {
        if (Mathf.Abs(transform.localEulerAngles.z - closedAngle) < 0.1f) opened = false;
        else opened = true;
        if (isOpen)
        {
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles - new Vector3(0, 0, invert ? 360 : 0), new Vector3(0, 0, openedAngle), openSpeed);
        }
        else
        {
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles - new Vector3(0, 0, invert ? 360 : 0), new Vector3(0, 0, closedAngle), closeSpeed);
        }
        if (preOpen != isOpen && playDoorSound) 
        {
            if (isOpen) openSound.Play();
            else closeSound.Play();
        }
        preOpen = isOpen;
    }
}
