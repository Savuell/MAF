using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsTransform : MonoBehaviour
{
    [SerializeField] private GameObject wCollider;
    [SerializeField] private GameObject wheel;
    [SerializeField] private GameObject wTransform;
    private WheelCollider wc;

    private void Start()
    {
        wc = wCollider.GetComponent<WheelCollider>();
    }
    void Update()
    {
        Vector3 pos;
        Quaternion rot;
        wc.GetWorldPose(out pos, out rot);
        wheel.transform.rotation = rot;
        wTransform.transform.position = pos;
    }
}
