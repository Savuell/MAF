using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spedometer : MonoBehaviour
{
    [SerializeField] private GameObject center;
    [SerializeField] private GameObject axis;
     [SerializeField]private Vector3 prePos, deltaPos;
     [SerializeField]private float speed, angle, preAngle;

    void FixedUpdate()
    {
        deltaPos = center.transform.position - prePos;
        speed = (deltaPos.magnitude/Time.fixedDeltaTime)*3.6f;
        angle = speed * 2.375f;
        preAngle = axis.transform.localEulerAngles.z;
        axis.transform.localEulerAngles = Vector3.Lerp(new Vector3(0, 0, preAngle), new Vector3(0, 0, angle), 0.1f);
        prePos = center.transform.position;
    }
}
