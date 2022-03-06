using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyStuckState : EnemyState
{
    bool isMoving;
    float moveEffortDuration;

    Vector2 targetPosition;

    float moveEffort = 0.5f;
    [SerializeField] float minMoveEffort = 1.0f;
    [SerializeField] float maxMoveEffort = 1.5f;
    [SerializeField] float tiredDuration = 1f;

    public Transform[] garbage;

    protected override void StateDoStart()
    {
        isMoving = false;
    }


    public override bool StateUpdate()
    {
        if (!isMoving)
        {
            StartCoroutine(StuckMoveEffort());
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

            for (int j = 0; j < garbage.Length; ++j){
                garbage[j].DOPunchRotation(new Vector3(0f, 0f, Random.Range(-8f, 8f)), moveEffortDuration);
            }


            yield return new WaitForSeconds(moveEffortDuration);
        }

        yield return new WaitForSeconds(tiredDuration);

        isMoving = false;
    }

}
