using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public CamObjScript my_CamObjScript_script;
    public int rewindStopStartFastForward;
    public float pauseTimeAmount;
    private int objHit;
    public bool pausingTime;

    private void Update()
    {
        if (objHit == 1)
        {
            objHit = 2;
            if (pausingTime)
            {
                my_CamObjScript_script.chosenFunctionNumber = 1;
                my_CamObjScript_script.afterPauseFunctionNum = rewindStopStartFastForward;
                my_CamObjScript_script.pauseWaitTime = pauseTimeAmount;
            }
            else
            {
                my_CamObjScript_script.chosenFunctionNumber = rewindStopStartFastForward;
            }
        }
    }

    private void LateUpdate()
    {
        if (objHit == 2)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera"))
        {
            my_CamObjScript_script = other.GetComponent<CamObjScript>();
            objHit = 1;
        }
    }
}
