using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineAlarm : MonoBehaviour
{
    [SerializeField] private ButtonCar button;
    [SerializeField] private GameObject[] signals = new GameObject[2];
    [SerializeField] private GameObject[] headlights = new GameObject[4];
    private bool flash;
    void FixedUpdate()
    {
        if (!flash && button.button) StartCoroutine(WaitFlash());
    }
    IEnumerator WaitFlash()
    {
        flash = true;
        yield return new WaitForSeconds(0.4f);
        signals[0].SetActive(false);
        signals[1].SetActive(true);
        headlights[0].SetActive(false);
        headlights[1].SetActive(true); 
        headlights[2].SetActive(false);
        headlights[3].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        signals[0].SetActive(true);
        signals[1].SetActive(false);
        headlights[0].SetActive(true);
        headlights[1].SetActive(false);
        headlights[2].SetActive(true);
        headlights[3].SetActive(false);
        flash = false;
    }
}
