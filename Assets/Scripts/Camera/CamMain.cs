using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMain : MonoBehaviour
{
    public Transform camObj;

    private Vector3 newPos;

    public float smoothMovement = 1;
    private Vector3 velocity = Vector3.zero;


    // Update is called once per frame
    void LateUpdate()
    {
        newPos = new Vector3(camObj.position.x, camObj.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothMovement);
    }
}
