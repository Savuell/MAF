using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollider : MonoBehaviour
{
    [SerializeField] private GameObject colliderObject;
    void Update()
    {
        colliderObject.transform.position = transform.position;
        colliderObject.transform.rotation = transform.rotation;
    }
}
