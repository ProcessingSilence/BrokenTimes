using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawblade : MonoBehaviour
{
    public float speed = 100;
    public Transform rotatingSprite;
    public BladeHitbox my_BladeHitBox_script;

    private CircleCollider2D hitbox;

    private PlayerController my_PlayerController_script;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = rotatingSprite.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.pitch = CamObjScript.TimeSpeed.timeSpeed;
        // Rotate the sprite of the sawblade forever.
        rotatingSprite.Rotate(0, 0, speed * CamObjScript.TimeSpeed.timeSpeed*Time.deltaTime);
        
        if (my_BladeHitBox_script.hitPlayerFlag == 1)
        {
            my_BladeHitBox_script.hitPlayerFlag = 2;
            
            // Get the player script from the player-sawblade collision detection.
            my_PlayerController_script = my_BladeHitBox_script.my_PlayerController_script;
            if (my_PlayerController_script.deathFlag == 0)
            {
                my_PlayerController_script.deathFlag = 1;
            }
        }
    }
}
