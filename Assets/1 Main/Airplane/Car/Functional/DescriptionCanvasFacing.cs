using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionCanvasFacing : MonoBehaviour
{
    private GameObject cameraPos;
    private void Start()
    {
        cameraPos = Camera.main.gameObject;
    }
    void Update()
    {
        transform.forward = cameraPos.transform.position - transform.position;
    }
}
