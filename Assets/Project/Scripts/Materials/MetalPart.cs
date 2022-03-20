using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MetalPart : Ore
{
    public delegate void MetalPartGetsMinedAction();
    public static event MetalPartGetsMinedAction playerMinesMetalPartEvent;
    public static event MetalPartGetsMinedAction playerBreaksMetalPartEvent;

    protected override void DamageTakeAnimation()
    {
        transform.DOPunchScale(new Vector3(-0.3f, -0.3f, 0), 0.40f);
    }

    protected override void OnDamageTake()
    {
        if (playerMinesMetalPartEvent != null) playerMinesMetalPartEvent();
    }

    protected override void OnDeathDamageTake()
    {
        if (playerBreaksMetalPartEvent != null) playerBreaksMetalPartEvent();
    }

}
