using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BatteryCharge : MonoBehaviour
{
    [HideInInspector] public bool charged;
    [SerializeField] private CarHinge cap;
    [SerializeField] private XRSocketInteractor socket;
    [SerializeField] private OpenHinge open;
    private Collider trigger;
    public float chargeSpeed;
    private bool connected;
    void Update()
    {
        open.enabled = !charged;
        socket.enabled = cap.isOpen;
        charged = connected && cap.isOpen && trigger;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Charge"))
        {
            connected = true;
            trigger = other;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Charge"))
        {
            connected = false;
            trigger = null;
        }
    }
}
