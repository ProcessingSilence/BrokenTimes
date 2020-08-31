using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Transform targetYPos;
    public Transform graphic;
    public GameObject hitPlayer;
    public ClockHands minuteHand;
    public ClockHands secondHand;
    public Sprite FixedSprite;
    public SpriteRenderer ClockSprite;
    private int goalFlag;
    private AudioSource audioSource;
    public GameObject clockMovement;
    public GameObject clockParticles;
    private MainSceneManager my_MainSceneManager_script;
    public ParticleSystem particleSystem;
    void Awake()
    {
        my_MainSceneManager_script = GameObject.Find("SceneManager").GetComponent<MainSceneManager>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        graphic.position = new Vector3(transform.position.x, targetYPos.position.y, transform.position.z);
        if (goalFlag == 1)
        {
            goalFlag = 2;
            ClockSprite.sprite = FixedSprite;
            Destroy(clockMovement);
            Destroy(clockParticles);
            audioSource.Play();
            my_MainSceneManager_script.levelNum = 2;
            minuteHand.isFixed = secondHand.isFixed = true;
            particleSystem.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && goalFlag == 0)
        {
            goalFlag = 1;
        }
    }
}
