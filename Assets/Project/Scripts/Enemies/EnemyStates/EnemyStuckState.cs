using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyStuckState : EnemyState
{
    bool isPlayingSound;
    bool isMoving;
    float moveEffortDuration;

    Vector2 targetPosition;

    float moveEffort = 1f;
    [SerializeField] float minMoveEffort = 1.0f;
    [SerializeField] float maxMoveEffort = 1.5f;
    [SerializeField] float tiredDuration = 0.7f;

    [SerializeField] StuckCoreGarbage garbage;

    [SerializeField] AudioSource footstepsAudioSource;
    [SerializeField] AudioSource screamAudioSource;

    protected override void StateDoStart()
    {
        isMoving = false;
        isPlayingSound = false;
        footstepsAudioSource.Stop();
    }


    public override bool StateUpdate()
    {
        if (!isMoving)
        {
            StartCoroutine(StuckMoveEffort());
            if (!isPlayingSound) StartCoroutine(StuckSound());
        }

        return false;
    }


    IEnumerator StuckMoveEffort()
    {
        isMoving = true;


        for (int i = Random.Range(1,3); i > 0; --i)
        {
            moveEffortDuration = Random.Range(0.20f, 0.25f);
            targetPosition = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * moveEffort;
            transform.DOPunchPosition(targetPosition, moveEffortDuration, 1);

            garbage.ShakeTransforms(moveEffortDuration);

            yield return new WaitForSeconds(moveEffortDuration);
        }

        yield return new WaitForSeconds(tiredDuration);

        isMoving = false;
    }

    IEnumerator StuckSound()
    {
        isPlayingSound = true;

        screamAudioSource.pitch = Random.Range(0.8f, 1.2f);
        screamAudioSource.Play();
        yield return new WaitForSeconds(Random.Range(1.2f, 1.5f));

        isPlayingSound = false;
    }



}
