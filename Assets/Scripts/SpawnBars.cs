using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBars : MonoBehaviour
{
    public float xOffset;

    public GameObject BlackBar;
    public GameObject[] SpawnedBlackBar;
    void Awake()
    {
        for (int i = 1, j = 0; i > -2; i-=2, j++)
        {
            SpawnedBlackBar[j] = Instantiate(BlackBar, transform.position + new Vector3(xOffset* i,0,-1), Quaternion.identity);
            SpawnedBlackBar[j].transform.parent = gameObject.transform;
        }
        Destroy(gameObject.GetComponent<SpawnBars>());
    }
}
