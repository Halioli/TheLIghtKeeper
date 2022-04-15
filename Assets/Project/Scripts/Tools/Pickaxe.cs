using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    private const int MAX_LEVELS = 3;
    private int currentLevel = 0;
    private int[] damageValueLvl = { 1, 2, 2 };
    private Hardness[] hardnessLvl = { Hardness.HARD, Hardness.VERY_HARD, Hardness.VERY_HARD };
    private int[] extraDropLvl = { 1, 1, 2};

    public int damageValue { get; private set; }
    public int criticalDamageValue { get; private set; }
    public Hardness hardness { get; private set; }
    public int extraDrop { get; private set; }


    private void Awake()
    {
        damageValue = 1;
        criticalDamageValue = damageValue + 1;
        hardness = Hardness.NORMAL;
        extraDrop = 1;
    }

    private void OnEnable()
    {
        PickaxeUpgrade.OnPickaxeUpgrade += Upgrade;
    }

    private void OnDisable()
    {
        PickaxeUpgrade.OnPickaxeUpgrade -= Upgrade;
    }


    private void Upgrade()
    {
        if (currentLevel >= MAX_LEVELS)
            return;

        damageValue = damageValueLvl[currentLevel];
        criticalDamageValue = damageValueLvl[currentLevel];
        hardness = hardnessLvl[currentLevel];
        extraDrop = extraDropLvl[currentLevel];

        ++currentLevel;
    }

}
