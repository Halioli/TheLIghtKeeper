using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataControl : MonoBehaviour
{
    public GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("FirstTime") == 0)
        {
            OnLevelWasLoaded(2);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 2)
        {
            
        }
    }
}
