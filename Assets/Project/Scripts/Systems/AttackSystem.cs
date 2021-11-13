using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    // Public Attributes
    public int attackValue;


    public void DamageHealthSystemWithAttackValue(HealthSystem healthSystem)
    {
        healthSystem.ReceiveDamage(attackValue);
    }
    
}
