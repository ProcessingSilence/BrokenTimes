using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseMissiles : MonoBehaviour
{
    public Transform ShitTonOfMissiles;

    public Vector3 offset;

    public int spawnFlag;
    public Transform Player;

    // Update is called once per frame
    void Update()
    {
        if (spawnFlag == 1)
        {
            spawnFlag = 2;
            var position = Player.position + offset;
            position = new Vector3(position.x, offset.y, position.z);
            ShitTonOfMissiles.position = position;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (spawnFlag == 0)
                spawnFlag = 1;
        }
    }
}
