using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pickaxe : MonoBehaviour
{
    private const int MAX_LEVELS = 3;
    private int currentLevel = 0;
    private int[] damageValueIncrement = { 0, 1, 0 };
    private Hardness[] hardnessIncrement = { Hardness.HARD, Hardness.HARD, Hardness.HARD };
    private int[] extraDropIncrement = { 0, 0, 1};

    public int damageValue { get; private set; }
    public int criticalDamageValue { get; private set; }
    public Hardness hardness { get; private set; }
    public int extraDrop { get; private set; }


    private void Start()
    {
        damageValue = 1;
        criticalDamageValue = damageValue + 1;
        hardness = Hardness.NORMAL;
        extraDrop = 0;
    }

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

        damageValue += damageValueIncrement[currentLevel];
        criticalDamageValue += damageValueIncrement[currentLevel];
        hardness = hardnessIncrement[currentLevel];
        extraDrop += extraDropIncrement[currentLevel];

        ++currentLevel;
    }

}
