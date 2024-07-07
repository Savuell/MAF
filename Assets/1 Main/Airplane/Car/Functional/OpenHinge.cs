using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class OpenHinge : MonoBehaviour
{
    [SerializeField] private InputActionProperty leftHand, rightHand;
    private int handed;
    private List<GameObject> hand = new List<GameObject>();
    private InputActionProperty currentHand;
    private CarHinge axis;
    private bool openingHinge;
    private void Start()
    {
        axis = transform.parent.GetComponent<CarHinge>();
    }
    void Update()
    {
        if (handed > 0 && !ShowCarDetails.isShowing)
        {
            currentHand = hand[0].transform.parent.name == "LeftHand" ? leftHand : rightHand;
            if (handed > 1) currentHand = leftHand.action.ReadValue<float>() > rightHand.action.ReadValue<float>() ? leftHand : rightHand;
            if (currentHand.action.ReadValue<float>() > 0.5f && !openingHinge)
            {
                axis.isOpen = !axis.isOpen;
                StartCoroutine(WaitOpen());
            }
        }
    }
    IEnumerator WaitOpen()
    {
        openingHinge = true;
        yield return new WaitForSeconds(0.2f);
        openingHinge = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            hand.Add(other.gameObject);
            handed++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            handed--;
            hand.Remove(other.gameObject);
        }
    }
}
