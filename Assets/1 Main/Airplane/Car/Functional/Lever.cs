using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject attachment;
    [SerializeField] private float speed;
    [HideInInspector] public int turn;
    private GameObject axis;
    private GameObject hand;
    private Vector3 handPrePos, handDeltaPos;
    private bool isTurning;
    private bool waitTurn;
    private int handed;
    [SerializeField] private bool isTurningLever;
    [SerializeField] private SteeringWheel steeringWheel;
    private bool wheelTurn;
    [SerializeField] private bool isWipersLever;
    private void Start()
    {
        if (isWipersLever) turn = -2;
        axis = transform.parent.gameObject;
    }
    void FixedUpdate()
    {
        if(!isTurning)GetTurn();
        else Turn();
    }
    IEnumerator WaitForEndOfTurn()
    {
        waitTurn = true;
        yield return new WaitForSeconds(0.5f);
        isTurning = false;
        waitTurn = false;
    }
    void GetTurn()
    {
        if (Mathf.Abs(turn) == 2 && isTurningLever)
        {
            if (Mathf.Abs(steeringWheel.steerAngle) > 45 && !wheelTurn) wheelTurn = true;
            if (Mathf.Abs(steeringWheel.steerAngle) < 1 && wheelTurn)
            {
                wheelTurn = false;
                turn = (int)Mathf.Sign(turn);
                isTurning = true;
            }
        }
        else wheelTurn = false;
        if (handed > 0)
        {
            attachment.transform.position = hand.transform.position;
            if (handPrePos == Vector3.zero) handPrePos = attachment.transform.localPosition;
            handDeltaPos = attachment.transform.localPosition - handPrePos;
            handPrePos = attachment.transform.localPosition;
            if (handDeltaPos.sqrMagnitude >= 0.0001f)
            {
                if (handDeltaPos.x < 0)
                {
                    if (turn != 2)
                    {
                        isTurning = true;
                        if (turn == -2) turn = -1;
                        else turn = 2;
                    }
                }
                if (handDeltaPos.x > 0)
                {
                    if (turn != -2)
                    {
                        isTurning = true;
                        if (turn == 2) turn = 1;
                        else turn = -2;
                    }
                }
            }
        }
    }
    void Turn()
    {
        handPrePos = Vector3.zero;
        switch (turn)
        {
            case -2://0 > -30   >180 <360+angle-
                axis.transform.localEulerAngles += new Vector3(0, 0, -Time.deltaTime * speed);
                if (axis.transform.localEulerAngles.z > 180 && axis.transform.localEulerAngles.z < 330)
                {
                    axis.transform.localEulerAngles = new Vector3(0, 0, 330);
                    if (!waitTurn) StartCoroutine(WaitForEndOfTurn());
                }
                break;
            case -1://-30 > 0   >angle+ <180
                axis.transform.localEulerAngles += new Vector3(0, 0, Time.deltaTime * speed);
                if (axis.transform.localEulerAngles.z > 0 && axis.transform.localEulerAngles.z < 180)
                {
                    axis.transform.localEulerAngles = new Vector3(0, 0, 0);
                    if (!waitTurn) StartCoroutine(WaitForEndOfTurn());
                }
                break;
            case 1://30 > 0      >angle+ <180
                axis.transform.localEulerAngles += new Vector3(0, 0, -Time.deltaTime * speed);
                if (axis.transform.localEulerAngles.z > 180 && axis.transform.localEulerAngles.z < 360)
                {
                    axis.transform.localEulerAngles = new Vector3(0, 0, 0);
                    if (!waitTurn) StartCoroutine(WaitForEndOfTurn());
                }
                break;
            case 2://0 > 30      >angle+ <180
                axis.transform.localEulerAngles += new Vector3(0, 0, Time.deltaTime * speed);
                if (axis.transform.localEulerAngles.z > 30 && axis.transform.localEulerAngles.z < 180)
                {
                    axis.transform.localEulerAngles = new Vector3(0, 0, 30);
                    if (!waitTurn) StartCoroutine(WaitForEndOfTurn());
                }
                break;
        }
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
