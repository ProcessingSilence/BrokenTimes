using System;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClockParticle : MonoBehaviour
{
    public Vector2 randomSpeedRange;
    public Vector2 randomRotationRange;
    // Store the current speed times timescale.
    public float speedTimesTime;
    private float randomSpeed;
    private float randomRotation;
    private float randomStartScale;
    public float scaleIncrement;
    private float originalScaleIncrement;

    public float timeExisting;
    public float timeExistingLimit = 1;
    public Vector3 ParentPos;
    // Determine if xPos needs to be more or less than 0 to be deleted.
    private float moreOrLess;
    // 0: randomly negative or positive, 1: negative only, 2: positive only, 
    public int random_negative_positive;
    private float minusVar;
    public Vector2 startScaleRange = new Vector2(-0.3f, 0.6f);
    
    // Start is called before the first frame update
    void Start()
    {
        // Time moving forward: Start small
        if (CamObjScript.TimeSpeed.timeSpeed > 0)
        {
            randomStartScale = Random.Range(0.2f, 0.5f);
        }
        else
        {
            if (random_negative_positive < 1)
            {
                minusVar =  1.1f/(1.1f - Vector2.Distance(transform.position, transform.parent.position));
                randomStartScale = minusVar + Random.Range(startScaleRange.x, startScaleRange.y) - 0.8f;
            }
            else
            {
                var startSize = Vector2.Distance(transform.position, transform.parent.position);
                if (startSize < 0)
                    startSize = -startSize;
                startSize += Random.Range(startScaleRange.x, startScaleRange.y);
                randomStartScale = startSize;
            }
        }

        // Time moving backward: Start large
        randomSpeed = Random.Range(randomSpeedRange.x, randomSpeedRange.y);
        
        if (random_negative_positive != 0)
        {
            // Negative numbers only
            if (randomSpeed > 0 && random_negative_positive < 2)
                randomSpeed = -randomSpeed;
            
            // Positive numbers only
            else if (randomSpeed < 0 && random_negative_positive > 1)
                randomSpeed = -randomSpeed;
        }

        randomRotation = Random.Range(randomRotationRange.x, randomRotationRange.y);
        StartCoroutine(DestroyTimeIncrement());
        StartCoroutine(MovementIncrement());
        transform.localScale = new Vector3(randomStartScale,randomStartScale,randomStartScale);
        originalScaleIncrement = scaleIncrement;
    }
    
    private void LateUpdate()
    {
        if (timeExisting >= timeExistingLimit || timeExisting <= -timeExistingLimit)
            Destroy(gameObject);
    }

    private IEnumerator DestroyTimeIncrement()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        timeExisting += 0.1f * CamObjScript.TimeSpeed.timeSpeed;
        StartCoroutine(DestroyTimeIncrement());
    }

    private IEnumerator MovementIncrement()
    {
        speedTimesTime = (randomSpeed * CamObjScript.TimeSpeed.timeSpeed)* CamObjScript.TimeSpeed.timeSpeed;
        ParentPos = transform.parent.position;
        
        // Scale check
        if (transform.localScale.x < 0 || transform.localScale.y < 0)
            Destroy(gameObject);
        
        // Move backwards when time is reversed.
        if (CamObjScript.TimeSpeed.timeSpeed < 0)
            DestroyAtZeroPosition();
        
        yield return new WaitForSecondsRealtime(0.01f);
        
        // Rotation increment
        transform.Rotate(0, 0, randomRotation * CamObjScript.TimeSpeed.timeSpeed*Time.deltaTime);
        
        // Scale increment
        if (CamObjScript.TimeSpeed.timeSpeed < 0 && random_negative_positive > 0)
        {
            scaleIncrement = originalScaleIncrement * 2;
        }
        transform.localScale += new Vector3(scaleIncrement, scaleIncrement, 0) * CamObjScript.TimeSpeed.timeSpeed;


        
        // Movement increment
        transform.Translate(speedTimesTime, 0,0);
        
        StartCoroutine(MovementIncrement());
    }

    // Called when time is in reverse, moves particles backwards and shrinks them.
    private void DestroyAtZeroPosition()
    {
        if (transform.position.x < transform.parent.position.x + 0.1f)
        {
            // Make negative number positive if particle is on the left.
            if (speedTimesTime < 0 && (random_negative_positive < 2))
            {
                speedTimesTime = -speedTimesTime * 2;
            }

        }
        else if (transform.position.x > transform.parent.position.x -0.1f)
        {
            // Make positive number negative if particle is on the right.
            if (speedTimesTime > 0 && (random_negative_positive == 2 || random_negative_positive == 0))
            {
                speedTimesTime = -speedTimesTime * 2;
            }
        }
    }
}
