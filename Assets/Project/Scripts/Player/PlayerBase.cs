using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    // Protected attributes
    protected PlayerInputs playerInputs;
    protected PlayerStates playerStates;
    protected HealthSystem healthSystem;
    protected AttackSystem attackSystem;

    // Public Attributes
    // public AudioSource audioSource;
    // public AudioClip hurtedAudioClip



    void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
        playerStates = GetComponent<PlayerStates>();
    }


    public void ReceiveDamage(int damageValue)
    {
        healthSystem.ReceiveDamage(damageValue);

        //hurtedAudioSource.Play();
    }

    public void DealDamage(HealthSystem healthSystemToDealDamage)
    {
        healthSystemToDealDamage.ReceiveDamage(attackSystem.attackValue);

        //attackAudioSource.Play();
    }
}
