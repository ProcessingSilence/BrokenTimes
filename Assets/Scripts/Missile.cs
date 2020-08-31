using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed;
    private CapsuleCollider2D hitbox;

    private PlayerController my_PlayerController_script;
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;

    private int hitPlayerFlag;

    private CapsuleCollider2D capsuleCollider2D;

    // 0: Traveling noise, 1: BOOM
    public AudioClip[] audioClips;

    private ParticleSystem particleSystem;

    public float divisionAmount;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[0];
        audioSource.Play();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Stop();
    }
    
    void Start()
    {
        if (speed > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        StartCoroutine(Movement());
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPlayerFlag == 0)
            audioSource.pitch = CamObjScript.TimeSpeed.timeSpeed;
        else
            audioSource.pitch = 1;
        if (hitPlayerFlag == 1)
        {
            hitPlayerFlag = 2;
            if (my_PlayerController_script.deathFlag == 0)
            {
                Destroy(capsuleCollider2D);
                Destroy(spriteRenderer);
                speed = 0;
                my_PlayerController_script.deathFlag = 1;
                audioSource.Stop();
                audioSource.loop = false;
                audioSource.clip = audioClips[1];
                audioSource.Play();
                particleSystem.Play();
            }
        }
    }

    IEnumerator Movement()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        rb.velocity = new Vector2(speed * CamObjScript.TimeSpeed.timeSpeed/divisionAmount,0);
        StartCoroutine(Movement());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MissileLauncher"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            hitPlayerFlag = 1;
            my_PlayerController_script = other.GetComponent<PlayerController>();
        }
    }
}
