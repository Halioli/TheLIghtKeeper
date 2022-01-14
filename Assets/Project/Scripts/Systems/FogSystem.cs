using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSystem : MonoBehaviour
{
    public PlayerLightChecker playerLightChecker;
    private GameObject player;

    private bool playerInFog = false;
    private float timer;

    private Vector3 respawnPosition;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player.tag);
        timer = 10f;
        respawnPosition = new Vector3(30f, -11f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInFog)
        {
            if (!playerLightChecker.IsPlayerInLight())
            {
                if(timer > 0f)
                {
                    timer -= Time.deltaTime;
                    Debug.Log(timer);
                }
                else
                {
                    ResetTimer();
                    player.transform.position = respawnPosition;
                }
            }
            else
            {
                ResetTimer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInFog = true;
            Debug.Log("PlayerIn");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            playerInFog = false;
            Debug.Log("PlayerOut");
            ResetTimer();
        }
    }
    private void ResetTimer()
    {
        timer = 10f;
    }
}
