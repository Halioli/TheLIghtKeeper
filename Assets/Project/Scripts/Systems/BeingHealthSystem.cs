using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingHealthSystem : HealthSystem
{
    private float INVULNERABILITY_TIME = 0.5f;

    private SpriteRenderer spriteRenderer;
    private int NUMBER_OF_SPRITE_FLASH_TICKS = 2;
    private float SPRITE_FLASH_TIME = 0.1f;

    private Color normalColor;
    private Color hurtedColor;


    void Start()
    {
        health = maxHealth;
        canBeDamaged = true;

        spriteRenderer = GetComponent<SpriteRenderer>();

        normalColor = new Color(0, 0, 0);

        //normalColor = spriteRenderer.material.color;
        hurtedColor = new Color(0.4f, 0, 0.1f, 0.8f);
    }


    override public void ReceiveDamage(int damageValueToSubstract)
    {
        if (canBeDamaged)
        {
            health = (health - damageValueToSubstract < 0 ? 0 : health -= damageValueToSubstract);
            StartCoroutine("DamageFeedback");
            StartCoroutine("TurnMomentarilyInvulnerable");
        }
    }

    IEnumerator DamageFeedback()
    {

        for (int i = 0; i < NUMBER_OF_SPRITE_FLASH_TICKS; i++)
        {
            spriteRenderer.material.color = hurtedColor;
            yield return new WaitForSeconds(SPRITE_FLASH_TIME);

            spriteRenderer.material.color = normalColor;
            yield return new WaitForSeconds(SPRITE_FLASH_TIME);
        }

    }

    IEnumerator TurnMomentarilyInvulnerable()
    {
        canBeDamaged = false;

        yield return new WaitForSeconds(INVULNERABILITY_TIME);

        canBeDamaged = true;
    }
}
