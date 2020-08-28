using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform ToFollow;
    public Vector3 offset;
    public bool setPositionOnce;
    public bool dontDestroyOnTimeStop;

    private void Start()
    {
        if (setPositionOnce)
        {
            var position = transform.position;
            position = ToFollow.position + offset;
            position.z = 0;
            transform.position = position;
        }
    }

    void LateUpdate()
    {
        var position = transform.position;
        position = ToFollow.position + offset;
        position.z = 0;
        transform.position = position;
        if (CamObjScript.TimeSpeed.timeSpeed == 0 && dontDestroyOnTimeStop == false)
        {
            Destroy(gameObject.GetComponent<FollowScript>());
        }
    }
}
