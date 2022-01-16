using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum CriticalMiningState { NONE, FAILED, SUCCEESSFUL };

public class PlayerMiner : PlayerBase
{
    // Private Attributes
    private const float OVERLAP_CIRCLE_RADIUS = 1.5f;

    private Ore oreToMine;
    private CriticalMiningState criticalMiningState = CriticalMiningState.NONE;
    private const float MINING_TIME = 1.0f;
    private float miningTime = 0;

    private bool canCriticalMine = false;
    private bool miningAnOre = false;
    private Vector2 raycastStartingPosition;
    private Vector2 raycastEndingPosition;

    // Overlap Circle & Dot Product
    private Vector2 overlapCirclePosition;
    private Vector2 mouseDirection;
    private Vector2 oreDirection;
    private Collider2D[] collidedElements;
    private Collider2D maxColl;
    private float max = -2f;
    private float dotRes;

    [SerializeField] Pickaxe pickaxe;

    // Public Attributes
    public GameObject interactArea;
    public LayerMask defaultLayerMask;
    public static Collider2D OverlapCircle;
    public Animator animator;

    // Events
    public delegate void PlayPlayerSound();
    public static event PlayPlayerSound playerMinesEvent;
    public static event PlayPlayerSound playerSucceessfulMineEvent;
    public static event PlayPlayerSound playerFailMineEvent;
    public static event PlayPlayerSound playerMineEvent;
    public static event PlayPlayerSound playerBreaksOreEvent;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        MineTargetCheck();

        if (PlayerInputs.instance.PlayerClickedMineButton() && playerStates.PlayerStateIsFree() && !playerStates.PlayerActionIsMining())
        {
            if (miningAnOre)
                SetOreToMine(maxColl.GetComponent<Ore>());

            StartMining();
        }
    }

    private void MineTargetCheck()
    {
        PlayerInputs.instance.SetNewMousePosition();

        // Update & check all colliders
        UpdateOverlapCirlcePositionAndMouseDirection();
        collidedElements = ReturnAllOverlapedColliders();

        // Get the dot product from every collider in reach
        miningAnOre = false;
        maxColl = null;
        max = -2f;
        dotRes = max;
        for (int i = 0; i < collidedElements.Length; ++i)
        {
            if (collidedElements[i].CompareTag("Ore"))
            {
                oreDirection = (transform.position - collidedElements[i].transform.position).normalized;
                dotRes = Vector2.Dot(mouseDirection, oreDirection);

                if (dotRes > max)
                {
                    max = dotRes;
                    maxColl = collidedElements[i];
                }

                miningAnOre = true;

            }
        }


        if (maxColl != null && miningAnOre)
        {

            maxColl.GetComponentInChildren<SelectSpot>().DoSelect();

        }
        else if (maxColl != null)
        {
            maxColl.GetComponentInChildren<SelectSpot>().StopSelect();
        }
    }



    // METHODS
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastStartingPosition, raycastEndingPosition * 3f);
    }

    private void SetOreToMine(Ore ore)
    {
        oreToMine = ore;

        //PlayerInputs.instance.SpawnSelectSpotAtTransform(oreToMine.transform);
    }

    private void CheckCriticalMining()
    {
        if (PlayerInputs.instance.PlayerClickedMineButton())
        {
            if (canCriticalMine)
            {
                criticalMiningState = CriticalMiningState.SUCCEESSFUL;
                playerSucceessfulMineEvent();
                animator.SetBool("isPerfect", true);
            }
            else
            {
                criticalMiningState = CriticalMiningState.FAILED;
                playerFailMineEvent();
            }
        }
    }

    private void StartMining()
    {
        playerMinesEvent();

        FlipPlayerSpriteFacingOreToMine();
        playerStates.SetCurrentPlayerState(PlayerState.BUSSY); 
        playerStates.SetCurrentPlayerAction(PlayerAction.MINING);
        criticalMiningState = CriticalMiningState.NONE;
        StartCoroutine("Mining");
    }

    private void MineOre(int damageToDeal)
    {
        if (oreToMine.CanBeMined())
        {
            if (oreToMine.hardness <= pickaxe.hardness)
            {
                oreToMine.GetsMined(damageToDeal, pickaxe.extraDrop);

                if (oreToMine.Broke())
                {
                    // Play normal mine sound
                    if (playerBreaksOreEvent != null)
                        playerBreaksOreEvent();
                }
                else
                {
                    // Play break sound
                    if (playerMineEvent != null) { 
                        playerMineEvent();
                    }
                }
            }
            else
            {
                Debug.Log("!!! Pickaxe NOT strong enough !!!");
            }
        }
    }

    private void ResetMining()
    {
        miningTime = 0;
        criticalMiningState = CriticalMiningState.NONE;

        playerStates.SetCurrentPlayerState(PlayerState.FREE);
        playerStates.SetCurrentPlayerAction(PlayerAction.IDLE);

        Array.Clear(collidedElements, 0, collidedElements.Length);
        miningAnOre = false;
        maxColl = null;
        max = -2f;
        dotRes = max;
    }

    private void Mine()
    {
        if (!miningAnOre || oreToMine == null)
            return;

        if (criticalMiningState == CriticalMiningState.SUCCEESSFUL)
        {
            MineOre(pickaxe.criticalDamageValue);
        }
        else
        {
            MineOre(pickaxe.damageValue);
        }
    }

    private void UpdateOverlapCirlcePositionAndMouseDirection()
    {
        overlapCirclePosition = transform.position;
        overlapCirclePosition.y -= 1;

        mouseDirection = ((Vector2)transform.position - PlayerInputs.instance.GetMousePositionInWorld()).normalized;
    }

    private Collider2D[] ReturnAllOverlapedColliders()
    {
        if (collidedElements != null && collidedElements.Length != 0)
            Array.Clear(collidedElements, 0, collidedElements.Length);

        return Physics2D.OverlapCircleAll(overlapCirclePosition, OVERLAP_CIRCLE_RADIUS, defaultLayerMask);
    }

    public void StartCriticalInterval()
    {
        canCriticalMine = true;

    }

    public void FinishCriticalInterval()
    {
        canCriticalMine = false;
    }

    IEnumerator Mining()
    {
        PlayerInputs.instance.canMove = false;

        while (miningTime <= MINING_TIME)
        {

            yield return new WaitForSeconds(Time.deltaTime);
            miningTime += Time.deltaTime;

            if (criticalMiningState == CriticalMiningState.NONE)
                CheckCriticalMining();

        }

        ResetMining();

        PlayerInputs.instance.canMove = true;
        animator.SetBool("isPerfect", false);
    }

    private void FlipPlayerSpriteFacingOreToMine()
    {
        Vector2 targetPosition = Vector2.zero;

        if (miningAnOre)
            targetPosition = oreToMine.transform.position;
        else
            targetPosition = PlayerInputs.instance.GetMousePositionInWorld();

        if ((transform.position.x < targetPosition.x && !PlayerInputs.instance.facingLeft) ||
            (transform.position.x > targetPosition.x && PlayerInputs.instance.facingLeft))
        {
            Vector2 direction = targetPosition - (Vector2)transform.position;
            PlayerInputs.instance.FlipSprite(direction);
        }
    }


}