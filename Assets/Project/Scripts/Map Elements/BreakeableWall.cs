using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakeableWall : Ore
{
    private void Start()
    {
        breakState = OreState.WHOLE;

        currentSpriteIndex = 0;
        currentSprite = spriteList[currentSpriteIndex];

        healthSystem = GetComponent<HealthSystem>();
        foreach (ParticleSystem particleSystem in oreParticleSystem)
        {
            particleSystem.Stop();
        }
    }

    public override void GetsMined(int damageAmount)
    {
        base.GetsMined(damageAmount);
    }
}
