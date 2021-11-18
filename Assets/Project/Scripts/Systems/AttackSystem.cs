using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    // Public Attributes
    public int attackValue;


    // Method used if attackValue needs to be changed
    public void SettAttackValue(int attackValueToSet)
    {
        attackValue = attackValueToSet;
    }

    public void DamageHealthSystemWithAttackValue(HealthSystem healthSystem)
    {
        healthSystem.ReceiveDamage(attackValue);
    }
    
}
