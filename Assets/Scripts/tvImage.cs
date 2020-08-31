using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tvImage : MonoBehaviour
{
    private Image image;

    public Sprite[] tvSprites;

    public int functionNum;

    public CamObjScript my_CamObjScript;

    public int pauseRunningFlag;

    private bool isActive;

    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        functionNum = my_CamObjScript.chosenFunctionNumber;
        image.sprite = tvSprites[functionNum];
        StartCoroutine(Iteration());
    }

    void Update()
    {
        image.enabled = isActive;
    }

    private IEnumerator Iteration()
    {
        if (functionNum != my_CamObjScript.chosenFunctionNumber)
        {
            functionNum = my_CamObjScript.chosenFunctionNumber;
            image.sprite = tvSprites[functionNum];
        }
        // Flashing pause effect to indicate time left being paused.
        if (my_CamObjScript.pauseWaitTime > 0)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            while (my_CamObjScript.pauseWaitTime <= 4.5f && my_CamObjScript.pauseWaitTime > 1.6f)
            {
                isActive = !isActive;
                yield return new WaitForSecondsRealtime(0.5f);
            }
            isActive = true;
            while (my_CamObjScript.pauseWaitTime <= 1.6f && my_CamObjScript.pauseWaitTime > 0)
            {
                isActive = !isActive;
                yield return new WaitForSecondsRealtime(0.05f);
            }
        }
        
        if (isActive == false)
            isActive = true;
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(Iteration());
    }
}
