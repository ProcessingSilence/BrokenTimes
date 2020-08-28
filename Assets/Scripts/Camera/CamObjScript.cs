using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamObjScript : MonoBehaviour
{

    public int[] rewindStopStartFastForward = new int [5] {-1, 0, 1, 4, -4};
    public int chosenFunctionNumber;
    public float timeSpeedDebug;
    public float speedDivision;
    public float pauseWaitTime;
    
    // What function number to choose after the pause time.
    public int afterPauseFunctionNum;
    void Awake()
    {
        TimeSpeed.timeSpeed = rewindStopStartFastForward[chosenFunctionNumber];
        StartCoroutine(TimeIterate());
    }

    void Update()
    {
        Movement();
    }

    public static class TimeSpeed
    {
        public static float timeSpeed;
    }

    IEnumerator TimeIterate()
    {
        // Correct timeSpeed to 0 if [-0.1f > timeSpeed < 0.1f]
        if (rewindStopStartFastForward[chosenFunctionNumber] == 0 && TimeSpeed.timeSpeed < 0.01f && TimeSpeed.timeSpeed > -0.01f)
        {
            TimeSpeed.timeSpeed = 0;
            var tempPauseWaitTime = pauseWaitTime;
            for (int i = 0; i < tempPauseWaitTime; i++)
            {
                yield return new WaitForSeconds(1);
                pauseWaitTime -= 1;
            }
            chosenFunctionNumber = afterPauseFunctionNum;
        }
        if (TimeSpeed.timeSpeed < rewindStopStartFastForward[chosenFunctionNumber])
        {
            TimeSpeed.timeSpeed += 0.02f;
        }
        else if (TimeSpeed.timeSpeed > rewindStopStartFastForward[chosenFunctionNumber])
        {
            TimeSpeed.timeSpeed -= 0.02f;
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(TimeIterate());
    }

    void Movement()
    {
        timeSpeedDebug = TimeSpeed.timeSpeed;
        if (rewindStopStartFastForward[chosenFunctionNumber] != 0)
            transform.Translate((TimeSpeed.timeSpeed) * Time.deltaTime,0,0);
    }

    IEnumerator PauseTime(float pauseWaitTime)
    {
        yield return new WaitForSeconds(pauseWaitTime);
        
    }

}
