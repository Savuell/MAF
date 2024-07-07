using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WipersController : MonoBehaviour
{
    [SerializeField] private Lever lever;
    [SerializeField] private GameObject[] wipers = new GameObject[3];
    [SerializeField] private float speed;
    private float wait, timer, preWait;
    private bool cycle, turn;
    void FixedUpdate()
    {
        switch(lever.turn)
        {
            case 2:
                wait = 0;
                break;
            case -2:
                wait = float.PositiveInfinity;
                break;
            default:
                wait = 10;
                break; 
        }
        if (preWait != wait)timer = 0;
        if (wait == float.PositiveInfinity) timer = wait;
        preWait = wait;
        if (timer <= 0)
        {
            timer = wait;
            turn = true;
        }
        timer -= Time.deltaTime;
        if (turn) Turn();
    }
    void Turn()
    {
        foreach (GameObject wiper in wipers)
        {
            wiper.transform.localEulerAngles += new Vector3(0, 0, (cycle ? -1 : 1) * Time.deltaTime * speed);
        }
        if (!cycle && wipers[0].transform.localEulerAngles.z > 70 && wipers[0].transform.localEulerAngles.z < 180) cycle = true;
        if (cycle && wipers[0].transform.localEulerAngles.z < 335 && wipers[0].transform.localEulerAngles.z > 180) 
        {
            cycle = false;
            turn = false;
        }
    }
}
