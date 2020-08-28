using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHands : MonoBehaviour
{
    public float speed = 100;
    public float fixedSpeed;
    public bool isFixed;
    void Update()
    {
        // Rotate the sprite of the sawblade forever.
        if (isFixed == false)
            transform.Rotate(0, 0, speed * CamObjScript.TimeSpeed.timeSpeed*Time.deltaTime);
        else
        {
            transform.Rotate(0, 0, fixedSpeed * Time.deltaTime);
        }
    }
}
