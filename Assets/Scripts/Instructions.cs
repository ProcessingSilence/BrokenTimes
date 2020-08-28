using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public SpriteRenderer[] lines;

    private float[] alphaAmt = new float[] {0, 0, 0, 0};


    public float[] waitTime;
    private int startFlag = 0;
    // Start is called before the first frame update
    private BoxCollider2D boxCollider2D;
    void Awake()
    {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        for (int j = 0; j < alphaAmt.Length; j++)
        {
            lines[j].color = new Color(lines[j].color.r, lines[j].color.g, lines[j].color.b, 0);
        }

        startFlag = 0;
        Debug.Log(startFlag);
    }

    void Update()
    {
        if (CamObjScript.TimeSpeed.timeSpeed == 0 && startFlag == 0)
        {
            startFlag = 1;
            StartCoroutine(Transition());
            Destroy(gameObject.GetComponent<FollowScript>());
        }
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(1.8f);
        for (int i = 0; i < alphaAmt.Length; i++)
        {
            Debug.Log("Line " + i);
            while (lines[i].color.a < 1)
            {
                lines[i].color += new Color(0, 0, 0, .1f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(waitTime[i]);
            Debug.Log("WaitDone");
        }
    }
}
