using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{

    private const int MAX_LEVELS = 3;
    private int currentLevel = 0;
    private int[] damageValueIncrement = { 0, 1, 0 };
    private float[] knockbackIncrement = { 0f, 20f, 0f };

    public int damageValue { get; private set; }


    private void Start()
    {
        damageValue = 1;

    }
}
