using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealingStation : MonoBehaviour
{
    private bool playerInside;
    private GameObject player;
    private HealthSystem playerHealthSystem;
    public GameObject maxHealthMessage;
    public GameObject backgroundText;
    public Animator animator;

    [SerializeField] AudioSource healAudioSource;

    public delegate void PlayerHealedByHealingStation();
    public static event PlayerHealedByHealingStation OnHealedByHealingStation;


    // Start is called before the first frame update
    void Start()
    {
        playerInside = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealthSystem = player.GetComponent<HealthSystem>();
        maxHealthMessage.SetActive(false);
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
            maxHealthMessage.SetActive(false);
            backgroundText.SetActive(false);
        }
    }

    private void RestorePlayerHealth()
    {
        if (playerHealthSystem.GetHealth() < playerHealthSystem.maxHealth)
        {
            ShowPlayerHealedMessage();
            if (OnHealedByHealingStation != null)
                OnHealedByHealingStation();
            animator.SetBool("isHealed", true);
        }
        else
        {
            ShowMaxHealthMessage();
        }
    }

    private void ShowMaxHealthMessage()
    {
        //maxHealthMessage.text = "Player at Max Health";
        backgroundText.SetActive(true);
        maxHealthMessage.SetActive(true);
    }

    private void ShowPlayerHealedMessage()
    {
        //maxHealthMessage.text = "Player Healed";
        backgroundText.SetActive(true);
        maxHealthMessage.SetActive(true);
    }
}
