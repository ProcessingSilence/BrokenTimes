using System;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This code + ClockParticle.cs are unoptimized messes, but they get the job done.
// I need to make a machine that slaps myself across the face for every time I don't plan out my code before I begin writing it.
public class ParticleSpawner : MonoBehaviour
{
    public GameObject particle;

    public float spawnIteration;

    private float newSpawnIteration;

    private Vector2 randomSpawnRange;

    public float currentOffset = 0;
    public float spawnOffset = 3.6f;
    public Vector2 startScaleRange;

    public Vector2 particleSpeedRange = new Vector2(-0.004f, 0.004f);

    public float scaleIncrement = 0.006f;
    // 0: randomly negative or positive, 1: negative only, 2: positive only, 
    public int random_negative_positive;

    // 0 = Random x movement, 1 = positive x movement, 2 = negative x movement
    public int xMovementOnly;

    private GameObject newParticle;

    private float randomLocation;

    private ClockParticle my_ClockParticle_script;

    // Choose sprite for particle based on sprite list in editor.
    public Sprite[] particleVariants;
    public int spriteChoice;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ParticleIteration());
    }
    
    IEnumerator ParticleIteration()
    {
        yield return new WaitForSeconds(0.01f);
        if (CamObjScript.TimeSpeed.timeSpeed > 0)
        {
            currentOffset = 0;
        }
        else
        {
            currentOffset = CamObjScript.TimeSpeed.timeSpeed/spawnOffset;
        }

        spawnIteration += 0.01f * CamObjScript.TimeSpeed.timeSpeed;
        if (spawnIteration > newSpawnIteration + 0.1)
        {
            newSpawnIteration = 0f;
            spawnIteration = 0;
            SpawnParticle();
        }
        if (spawnIteration < newSpawnIteration - 0.1)
        {
            newSpawnIteration = 0f;
            spawnIteration = 0;
            SpawnParticle();
        }
        StartCoroutine(ParticleIteration());
    }

    void SpawnParticle()
    {
        for (int i = 0; i < 5; i++)
        {
            randomLocation = Random.Range(-0.1f + currentOffset, 0.1f - currentOffset);
            newParticle = Instantiate(particle, transform.position + new Vector3(randomLocation,0,0), Quaternion.identity);
            newParticle.transform.parent = gameObject.transform;
            
            // Set values in particle script
            my_ClockParticle_script = newParticle.GetComponent<ClockParticle>();
            my_ClockParticle_script.randomSpeedRange = particleSpeedRange;
            my_ClockParticle_script.scaleIncrement = scaleIncrement;
            my_ClockParticle_script.startScaleRange = startScaleRange;

            newParticle.GetComponent<SpriteRenderer>().sprite = particleVariants[spriteChoice];
            
            if (random_negative_positive != 0)
            {
                var particleLocation = newParticle.transform.position;
                
                // Negative numbers only.
                if (random_negative_positive < 2)
                {
                    my_ClockParticle_script.random_negative_positive = 1;
                    if (newParticle.transform.position.x > transform.position.x)
                    {
                        newParticle.transform.position = new Vector3(-particleLocation.x, particleLocation.y, particleLocation.z);
                    }
                }
                // Positive numbers only.
                else
                {
                    my_ClockParticle_script.random_negative_positive = 2;
                    if (newParticle.transform.position.x < transform.position.x)
                    {
                        newParticle.transform.position = new Vector3(-particleLocation.x, particleLocation.y, particleLocation.z);
                    }
                }
            }
        }
    }
}

