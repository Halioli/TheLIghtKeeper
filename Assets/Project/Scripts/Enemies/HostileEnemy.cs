using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileEnemy : Enemy
{
    protected Collider2D collider;
    protected bool insideLight = false;

    public AudioClip deathAudioClip;


    // Events
    public delegate void EnemyDisappears();
    public static event EnemyDisappears enemyDisappearsEvent;

    private void OnEnable()
    {
        DarknessSystem.OnPlayerEntersLight += DoFleeAndBanish;
    }

    public void OnDisable()
    {
        DarknessSystem.OnPlayerEntersLight -= DoFleeAndBanish;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.collider.IsTouchingLayers(LayerMask.NameToLayer("Light")))
        {
            FleeAndBanish();
        }
    }


    public void Banish(float banishTime)
    {
        startedBanishing = true;
        StartCoroutine(StartBanishing(banishTime));

        enemyDisappearsEvent();
    }

    IEnumerator StartBanishing(float banishTime)
    {

        // Play banish audio sound
        //audioSource.clip = banishAudioClip;
        //audioSource.volume = Random.Range(0.1f, 0.2f);
        //audioSource.pitch = Random.Range(0.7f, 1.5f);
        //audioSource.Play();

        // Fading
        Color fadeColor = spriteRenderer.material.color;
        currentBanishTime = banishTime;
        while (currentBanishTime > 0f)
        {
            fadeColor.a = currentBanishTime / BANISH_TIME;
            spriteRenderer.material.color = fadeColor;

            currentBanishTime -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    protected override void Die()
    {
        // Play death animation
        DropItem();
        Banish(BANISH_TIME);
    }

    protected virtual void EnteredLight()
    {
    }

    protected virtual void FleeAndBanish()
    {
        enemyState = EnemyState.SCARED;
        attackState = AttackState.MOVING_TOWARDS_PLAYER;
        Banish(BANISH_TIME);
    }


    protected void DoFleeAndBanish()
    {
        StartCoroutine(StartFleeAndBanish());
    }

    IEnumerator StartFleeAndBanish()
    {
        audioSource.clip = banishAudioClip;
        audioSource.volume = Random.Range(0.1f, 0.2f);
        audioSource.pitch = Random.Range(0.7f, 1.5f);
        audioSource.Play();

        enemyState = EnemyState.WANDERING;
        attackState = AttackState.MOVING_TOWARDS_PLAYER;

        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        
        FleeAndBanish();
    }

}
