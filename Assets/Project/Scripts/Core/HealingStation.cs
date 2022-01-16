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
    public GameObject backgroundText;
    public Animator animator;

    [SerializeField] AudioSource healAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        playerInside = false;
        player = GameObject.Find("LightScenePlayer");
        playerHealthSystem = player.GetComponent<HealthSystem>();
        maxHealthMessage.alpha = 0;
        backgroundText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
            RestorePlayerHealth();

            healAudioSource.Play();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
            RestorePlayerHealth();
            maxHealthMessage.alpha = 0;
            backgroundText.SetActive(false);
        }
    }

    private void RestorePlayerHealth()
    {
        if (playerHealthSystem.GetHealth() < playerHealthSystem.maxHealth)
        {
            ShowPlayerHealedMessage();
            playerHealthSystem.RestoreHealthToMaxHealth();
            animator.SetBool("isHealed", true);
        }
        else
        {
            ShowMaxHealthMessage();
        }
    }

    private void ShowMaxHealthMessage()
    {
        maxHealthMessage.text = "Player at Max Health";
        backgroundText.SetActive(true);
        maxHealthMessage.alpha = 100;
    }
    private void ShowPlayerHealedMessage()
    {
        maxHealthMessage.text = "Player Healed";
        backgroundText.SetActive(true);
        maxHealthMessage.alpha = 100;
    }
}
