using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileEnemy : Enemy
{
    // Events
    public delegate void EnemyDisappears();
    public static event EnemyDisappears enemyDisappearsEvent;

    private void OnEnable()
    {
        DarknessSystem.OnPlayerEntersLight += FleeAndBanish;
    }

    public void OnDisable()
    {
        DarknessSystem.OnPlayerEntersLight -= FleeAndBanish;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Light"))
        {
            FleeAndBanish();
        }
    }


    public void Banish()
    {
        startedBanishing = true;
        StartCoroutine(StartBanishing());

        enemyDisappearsEvent();
    }

    IEnumerator StartBanishing()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 0.3f));

        // Play banish audio sound
        audioSource.clip = banishAudioClip;
        audioSource.volume = Random.Range(0.1f, 0.2f);
        audioSource.pitch = Random.Range(0.7f, 1.5f);
        audioSource.Play();

        // Fading
        Color fadeColor = spriteRenderer.material.color;
        while (currentBanishTime > 0f)
        {
            fadeColor.a = currentBanishTime / BANISH_TIME;
            spriteRenderer.material.color = fadeColor;

            currentBanishTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(gameObject);
    }

    protected override void Die()
    {
        // Play death animation
        DropItem();
        Banish();
    }

    protected virtual void FleeAndBanish()
    {
        enemyState = EnemyState.SCARED;
        attackState = AttackState.MOVING_TOWARDS_PLAYER;
        Banish();
    }


}
