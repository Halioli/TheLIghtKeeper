using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealingStation : MonoBehaviour
{
    private bool playerInside;
    private GameObject player;
    private HealthSystem playerHealthSystem;
    public TextMeshProUGUI maxHealthMessage;

    // Start is called before the first frame update
    void Start()
    {
        playerInside = false;
        player = GameObject.Find("LightScenePlayer");
        playerHealthSystem = player.GetComponent<HealthSystem>();
        maxHealthMessage.alpha = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
            RestorePlayerHealth();
            Debug.Log(playerInside);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
            Debug.Log(playerInside);
            RestorePlayerHealth();
            maxHealthMessage.alpha = 0;
        }
    }

    private void RestorePlayerHealth()
    {
        if (playerHealthSystem.GetHealth() < playerHealthSystem.maxHealth)
        {
            playerHealthSystem.RestoreHealthToMaxHealth();
            ShowPlayerHealedMessage();
        }
        else
        {
            ShowMaxHealthMessage();
        }
    }

    private void ShowMaxHealthMessage()
    {
        maxHealthMessage.text = "Player at Max Health";
        maxHealthMessage.alpha = 100;
    }
    private void ShowPlayerHealedMessage()
    {
        maxHealthMessage.text = "Player Healed";
        maxHealthMessage.alpha = 100;
    }
}
