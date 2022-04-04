using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    // Protected Attributes
    public int health;
    protected bool canBeDamaged;
    protected bool canBeHealed;

    // Public Attributes
    public int maxHealth;


    void Awake()
    {
        health = maxHealth;
        canBeDamaged = true;
        canBeHealed = true;
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
    }

    virtual public void ReceiveHealth(int healthValueToAdd)
    {
        if (canBeHealed)
        {
            if ((health + healthValueToAdd) <= maxHealth)
            {
                health += healthValueToAdd;
            }
            else
            {
                RestoreHealthToMaxHealth();
            }
        }
    }

    virtual public bool IsDead() { return health <= 0; }
}
