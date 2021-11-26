using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    // Protected Attributes
    protected int health;
    protected bool canBeDamaged;

    // Public Attributes
    public int maxHealth;

    public AudioSource hurtedAudioSource;



    void Start()
    {
        health = maxHealth;
        canBeDamaged = true;
    }

    public void RestoreHealthToMaxHealth() { health = maxHealth; }

    public int GetMaxHealth() { return maxHealth; }

    public int GetHealth() { return health; }

    virtual public void ReceiveDamage(int damageValueToSubstract)
    {
        if (canBeDamaged)
        {
            health = (health - damageValueToSubstract < 0 ? 0 : health -= damageValueToSubstract);
        }
        PlayHurtedSound();
    }

    virtual public bool IsDead() { return health <= 0; }
 
    private void PlayHurtedSound()
    {
        hurtedAudioSource.Play();
    }


}
