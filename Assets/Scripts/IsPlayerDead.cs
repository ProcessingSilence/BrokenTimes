using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerDead : MonoBehaviour
{
    private GameObject Player;

    private int deathFlag;

    private MainSceneManager my_MainSceneManager_script;
    // Start is called before the first frame update
    void Start()
    {
        my_MainSceneManager_script = GameObject.Find("SceneManager").GetComponent<MainSceneManager>();
        Player = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.activeSelf == false)
        {
            my_MainSceneManager_script.levelNum = 1;
        }
    }
}
