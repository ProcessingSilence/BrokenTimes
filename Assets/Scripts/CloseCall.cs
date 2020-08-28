using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCall : MonoBehaviour
{
    public Transform Missile;

    public Transform Player;
    public Transform TimeObjThing;
    public float distance;

    public float minDist;
    // Update is called once per frame
    void Update()
    { 
       distance = Missile.position.x - Player.position.x;
       if (distance < minDist)
       {
           transform.position = TimeObjThing.position;
       }
    }
}
