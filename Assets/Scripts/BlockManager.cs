using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject[] children;

    public int iteration;
    public int childCount;
    void Awake()
    {
        childCount = transform.childCount;
        children = new GameObject[childCount];
    }

    void Start()
    {
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
            children[i].SetActive(false);
        }
        children[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (children[iteration].activeSelf == false && iteration < childCount-1)
        {
            Debug.Log("new");
            iteration++;
            children[iteration].SetActive(true);
        }
    }
}
