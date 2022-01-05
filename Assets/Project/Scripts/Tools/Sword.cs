using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] AttackSystem attackSystem;

    // Upgrades
    // 1 -> attack+
    // 2 -> knockback+
    // 3 -> attack+

    private const int MAX_LEVELS = 3;
    private int currentLevel = 0;
    private int[] attackIncrement = { 2, 0, 3 };
    private float[] knockbackIncrement = { 0f, 20f, 0f };


    private void OnEnable()
    {
        SwordUpgrade.OnSwordUpgrade += Upgrade;
    }

    private void OnDisable()
    {
        SwordUpgrade.OnSwordUpgrade -= Upgrade;
    }


    private void Upgrade()
    {
        if (currentLevel >= MAX_LEVELS)
            return;

        attackSystem.IncrementAttackValue(attackIncrement[currentLevel]);
        attackSystem.IncrementPushValue(knockbackIncrement[currentLevel]);

        ++currentLevel;
    }
}
