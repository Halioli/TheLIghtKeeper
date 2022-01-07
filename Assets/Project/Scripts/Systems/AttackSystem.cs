using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    // Public Attributes
    public int attackValue;
    public float pushValue = 0;

    // Method used if attackValue needs to be changed
    public void DamageHealthSystemWithAttackValue(HealthSystem healthSystem)
    {
        healthSystem.ReceiveDamage(attackValue);
    }


    public void SetAttackValue(int attackValueToSet)
    {
        attackValue = attackValueToSet;
    }

    public void IncrementAttackValue(int attackValueToIncrement)
    {
        attackValue += attackValueToIncrement;
    }

    public void SetPushValue(float pushValueToSet)
    {
        pushValue = pushValueToSet;
    }

    public void IncrementPushValue(float pushValueToIncrement)
    {
        pushValue += pushValueToIncrement;
    }

}
