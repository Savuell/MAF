using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCar : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float pos1, pos2;
    /*[HideInInspector]*/ public bool button;
    private GameObject axis;
    private GameObject hand;
    private Vector3 handPrePos, handDeltaPos;
     [SerializeField] private bool isPressing;
     [SerializeField] private bool waitPress;
    private int handed;
    private void Start()
    {
        axis = transform.parent.gameObject;
    }
    void FixedUpdate()
    {
        if (!isPressing) GetPress();
        else Press();
    }
    IEnumerator WaitForEndOfPress()
    {
        waitPress = true;
        yield return new WaitForSeconds(1);
        isPressing = false;
        waitPress = false;
    }
    void GetPress()
    {
        if (handed > 0)
        {
            if (handPrePos == Vector3.zero) handPrePos = hand.transform.position;
            handDeltaPos = hand.transform.position - handPrePos;
            handPrePos = hand.transform.position;
            if (handDeltaPos.sqrMagnitude >= 0.0001f)
            {
                isPressing = true;
                button = !button;
            }
        }
    }
    void Press()
    {
        handPrePos = Vector3.zero;
        axis.transform.localPosition = Vector3.Lerp(axis.transform.localPosition, new Vector3(0, 0, button ? pos2 : pos1), speed);
        if (Mathf.Abs(axis.transform.localPosition.z - (button ? pos2 : pos1)) < 0.01f && !waitPress) StartCoroutine(WaitForEndOfPress());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            handed++;
            hand = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHand")) handed--;
    }
}
