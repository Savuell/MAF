using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SteeringWheel : MonoBehaviour
{
    [SerializeField] private InputActionProperty leftHand, rightHand;
    [SerializeField] private GameObject steeringWheel, attachment;
    private List<GameObject> hand = new List<GameObject>();
    private InputActionProperty currentHand;
    private int handed;
    private float rot0;
    private float deltaRot, preRot;
    private int times360;
    public float steerAngle;
    void FixedUpdate()
    {
        if (handed > 0)
        {
            currentHand = hand[0].transform.parent.name == "LeftHand" ? leftHand : rightHand;
            if (handed > 1) currentHand = leftHand.action.ReadValue<float>() > rightHand.action.ReadValue<float>() ? leftHand : rightHand;
            if (currentHand.action.ReadValue<float>() > 0.5f)
            {
                HandRotate();
            }
            else rot0 = 999;
        }
        else steeringWheel.transform.localEulerAngles -= new Vector3(0, 0, Mathf.Sign(steerAngle) * Mathf.Min(Mathf.Abs(steeringWheel.transform.localEulerAngles.z), Mathf.Abs(steerAngle * Time.deltaTime * 5)));
        Calculate();
    }
    void Calculate()
    {
        deltaRot = steeringWheel.transform.localEulerAngles.z - preRot;
        if (Mathf.Abs(deltaRot) > 300) times360 -= (int)Mathf.Sign(deltaRot);
        preRot = steeringWheel.transform.localEulerAngles.z;
        steerAngle = times360 * 360 + preRot;
    }
    void HandRotate()
    {
        attachment.transform.position = hand[0].transform.position;
        Vector3 pos = new Vector3(0, 0, attachment.transform.localPosition.z);
        attachment.transform.localPosition = pos;
        attachment.transform.forward = hand[0].transform.position - attachment.transform.position;
        if (rot0 == 999)
        {
            if (attachment.transform.localEulerAngles.y > 180) rot0 = 180 - attachment.transform.localEulerAngles.x + steeringWheel.transform.localEulerAngles.z;
            else rot0 = attachment.transform.localEulerAngles.x + steeringWheel.transform.localEulerAngles.z;
        }
        pos = attachment.transform.localEulerAngles;
        pos.z = 90;
        attachment.transform.localEulerAngles = pos;
        if (attachment.transform.localEulerAngles.y > 180) steeringWheel.transform.localEulerAngles = new Vector3(0, 0, attachment.transform.localEulerAngles.x + rot0 + 180);
        else steeringWheel.transform.localEulerAngles = new Vector3(0, 0, -attachment.transform.localEulerAngles.x + rot0);
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
