using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BreakeableWall : Ore
{
    private void Start()
    {
        mineralItemToDrop = null;
        breakState = OreState.WHOLE;

        currentSpriteIndex = 0;
        currentSprite = spriteList[currentSpriteIndex];

        healthSystem = GetComponent<HealthSystem>();
        foreach (ParticleSystem particleSystem in oreParticleSystem)
        {
            particleSystem.Stop();
        }
    }

    public override void GetsMined(int damageAmount, int numberOfDrops)
    {
        transform.DOPunchScale(new Vector3(-0.6f, -0.6f, 0), 0.40f);
        // Damage the Ore
        healthSystem.ReceiveDamage(damageAmount);
        // Update ore Sprite
        ProgressNAmountOfSprites(damageAmount);

        if (healthSystem.IsDead())
        {
            breakState = OreState.BROKEN;

            // Start disappear coroutine
            StartCoroutine(Disappear());

            OnDeathDamageTake();
        }
        else
        {
            OnDamageTake();
        }
        UpdateCurrentSprite();
        StartCoroutine(PlayBreakParticles());
    }

    protected override void OnDamageTake()
    {
        base.OnDamageTake();
    }

    protected override void OnDeathDamageTake()
    {
        base.OnDeathDamageTake();
    }

}
