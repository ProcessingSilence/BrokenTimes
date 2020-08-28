using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Manager : MonoBehaviour
{
    public GameObject[] blockObjs;

    private int iterated;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < blockObjs.Length; i++)
        {
            if (blockObjs[i].activeSelf)
                blockObjs[i].SetActive(false);              
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!blockObjs[iterated].activeSelf)
        {
            if (iterated + 1 < blockObjs.Length)
            {
                iterated++;
                blockObjs[iterated].SetActive(true);
            }
            else
            {
                gameObject.GetComponent<Block_Manager>().enabled = false;
            }
        }
    }
}
