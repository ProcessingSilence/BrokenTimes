using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    public int FadeInOrOut;
    private bool isFading;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeInOrOut != 0 && isFading == false)
        {
            isFading = true;
            StartCoroutine(Fading(FadeInOrOut));
        }
    }

    IEnumerator Fading(float negOrPos)
    {

        for (int i = 0; i < 10; i++)
        {
            image.color += new Color(0,0,0,0.1f * negOrPos);
            yield return new WaitForSeconds(0.01f);
        }
        FadeInOrOut = 0;
        isFading = false;

    }
}
