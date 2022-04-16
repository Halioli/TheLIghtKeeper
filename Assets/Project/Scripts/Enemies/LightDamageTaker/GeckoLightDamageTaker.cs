using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeckoLightDamageTaker : LightDamageTaker
{
    [SerializeField] Gecko gecko;


    private void Awake()
    {
        Init();
    }


    protected override void DoReceiveDamage()
    {
        base.DoReceiveDamage();

        gecko.GetsTouched();
    }

}
