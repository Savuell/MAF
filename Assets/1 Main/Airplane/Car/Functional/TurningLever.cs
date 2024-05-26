using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;

public class TurningLever : MonoBehaviour
{
    [SerializeField] private GameObject[] signals = new GameObject[4];
    [SerializeField] private GameObject[] headlights = new GameObject[4];
    [SerializeField] private GameObject attachment;
    [SerializeField] private SteeringWheel steeringWheel;
    [SerializeField] private float speed;
    private GameObject axis;
    private GameObject hand;
    private Vector3 handPrePos, handDeltaPos;
    private bool isTurning;
    private bool wheelTurn;
    private bool flash;
    private int turn;
    private int handed;
    private void Start()
    {
        axis = transform.parent.gameObject;
    }
    void FixedUpdate()
    {
        GetTurn();
        Turn();
        TurnSignal();
    }
    IEnumerator WaitForEndOfTurn()
    {
        yield return new WaitForSeconds(0.5f);
        isTurning = false;
    }
    void GetTurn()
    {
        if (!isTurning) 
        {
            if(Mathf.Abs(turn)==2)
            {
                if (Mathf.Abs(steeringWheel.steerAngle) > 45 && !wheelTurn) wheelTurn = true;
                if (Mathf.Abs(steeringWheel.steerAngle) < 1 && wheelTurn)
                {
                    wheelTurn = false;
                    turn = (int)Mathf.Sign(turn);
                    isTurning = true;
                }
            }
            if (handed > 0)
            {
                attachment.transform.position = hand.transform.position;
                if (handPrePos == Vector3.zero) handPrePos = attachment.transform.localPosition;
                handDeltaPos = attachment.transform.localPosition - handPrePos;
                handPrePos = attachment.transform.localPosition;
                if(handDeltaPos.sqrMagnitude>=0.0001f)
                {
                    if (handDeltaPos.x < 0)
                    {
                        if (turn != 2)
                        {
                            if (turn == -2) turn = -1;
                            else turn = 2;
                            isTurning = true;
                        }
                    }
                    if (handDeltaPos.x > 0)
                    {
                        if (turn != -2)
                        {
                            if (turn == 2) turn = 1;
                            else turn = -2;
                            isTurning = true;
                        }
                    }
                }
            }
        }
        else handPrePos = Vector3.zero;
    }
    void Turn()
    {
        if (isTurning)
        {
            switch (turn)
            {
                case -2://0 > -30
                    axis.transform.localEulerAngles += new Vector3(0, 0, -Time.deltaTime * speed);
                    if (axis.transform.localEulerAngles.z > 180 && axis.transform.localEulerAngles.z < 330)
                    {
                        axis.transform.localEulerAngles = new Vector3(0, 0, 330);
                        StartCoroutine(WaitForEndOfTurn());
                    }
                    break;
                case -1://-30 > 0s
                    axis.transform.localEulerAngles += new Vector3(0, 0, Time.deltaTime * speed);
                    if (axis.transform.localEulerAngles.z > 0 && axis.transform.localEulerAngles.z < 180)
                    {
                        axis.transform.localEulerAngles = new Vector3(0, 0, 0);
                        StartCoroutine(WaitForEndOfTurn());
                    }
                    break;
                case 1://30 > 0
                    axis.transform.localEulerAngles += new Vector3(0, 0, -Time.deltaTime * speed);
                    if (axis.transform.localEulerAngles.z > 180 && axis.transform.localEulerAngles.z < 360)
                    {
                        axis.transform.localEulerAngles = new Vector3(0, 0, 0);
                        StartCoroutine(WaitForEndOfTurn());
                    }
                    break;
                case 2://0 > 30
                    axis.transform.localEulerAngles += new Vector3(0, 0, Time.deltaTime * speed);
                    if (axis.transform.localEulerAngles.z > 30 && axis.transform.localEulerAngles.z < 180)
                    {
                        axis.transform.localEulerAngles = new Vector3(0, 0, 30);
                        StartCoroutine(WaitForEndOfTurn());
                    }
                    break;
            }
        }
    }
    void TurnSignal()
    {
        if(!flash)
        {
            if(turn==2)StartCoroutine(WaitFlash(0));
            if (turn == -2)StartCoroutine(WaitFlash(1));
        }
    }
    IEnumerator WaitFlash(int num)
    {
        flash = true;
        yield return new WaitForSeconds(0.4f);
        signals[num].SetActive(false);
        signals[num+2].SetActive(true);
        headlights[num].SetActive(false);
        headlights[num + 2].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        signals[num].SetActive(true);
        signals[num+2].SetActive(false);
        headlights[num].SetActive(true);
        headlights[num + 2].SetActive(false);
        flash = false;
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
