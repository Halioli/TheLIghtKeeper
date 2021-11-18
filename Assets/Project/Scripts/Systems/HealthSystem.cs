using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    // Public Attributes
    public int maxHealth;

    // Private Attributes
    public int health;


    void Start()
    {
        health = maxHealth;
    }

    public void RevivePlayer() { health = maxHealth; }

    public int GetMaxHealth() { return maxHealth; }

    public int GetHealth() { return health; }

    public void ReceiveDamage(int damageValueToSubstract)
    {
        health = (health - damageValueToSubstract < 0 ? 0 : health -= damageValueToSubstract);
    }

    public bool IsDead() { return health <= 0; }
 
}
