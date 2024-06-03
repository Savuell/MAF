using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private GameObject toRotate;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float speed;
    private void Start()
    {
        if (!toRotate) toRotate = transform.parent.gameObject;
    }
    void Update()
    {
        toRotate.transform.Rotate(rotation * Time.deltaTime * speed); 
    }
}
