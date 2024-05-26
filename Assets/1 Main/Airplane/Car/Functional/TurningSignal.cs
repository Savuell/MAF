using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;

public class TurningSignal : MonoBehaviour
{
    [SerializeField] private Lever lever;
    [SerializeField] private GameObject[] signals = new GameObject[4];
    [SerializeField] private GameObject[] headlights = new GameObject[4];
    private bool flash;
    private int turn;
    void FixedUpdate()
    {
        turn = lever.turn;
        TurnSignal();
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
}
