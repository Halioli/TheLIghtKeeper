using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum CriticalMiningState { NONE, FAILED, SUCCEESSFUL };

public class PlayerMiner : PlayerBase
{
    // Private Attributes
    private Collider2D colliderDetectedByMouse = null;
    private Ore oreToMine;

    private const int START_MINING_DAMAGE = 1;
    private int miningDamage = START_MINING_DAMAGE;
    private const int START_CRITICAL_MINING_DAMAGE = 2;
    private int criticalMiningDamage = START_CRITICAL_MINING_DAMAGE;

    private CriticalMiningState criticalMiningState = CriticalMiningState.NONE;
    private const float MINING_TIME = 1.0f;
    private float miningTime = 0;

    private bool canCriticalMine = false;
    private bool miningAnOre = false;
    private Vector2 raycastStartingPosition;
    private Vector2 raycastEndingPosition;

    // Public Attributes
    public GameObject interactArea;
    public LayerMask defaultLayerMask;

    // Events
    public delegate void PlayPlayerSound();
    public static event PlayPlayerSound playerMiningBuildUpSoundEvent;
    public static event PlayPlayerSound successCriticalMiningSoundEvent;
    public static event PlayPlayerSound failCriticalMiningSoundEvent;
    public static event PlayPlayerSound playerMinesOreEvent;
    public static event PlayPlayerSound playerBreaksOreEvent;

    void Update()
    {

        if (PlayerInputs.instance.PlayerClickedMineButton() && playerStates.PlayerStateIsFree() && !playerStates.PlayerActionIsMining())
        {
            PlayerInputs.instance.SetNewMousePosition();

            miningAnOre = MineRaycast();

            if (!miningAnOre)
                PlayerInputs.instance.SpawnSelectSpotAtTransform(interactArea.transform);

            StartMining();
        }
        
    }

    // METHODS
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastStartingPosition, raycastEndingPosition * 3f);
    }

    private bool MineRaycast()
    {
        raycastStartingPosition = transform.position;
        raycastStartingPosition.y += -1f;

        raycastEndingPosition = PlayerInputs.instance.GetMousePositionInWorld() - raycastStartingPosition;
        raycastEndingPosition.Normalize();

        RaycastHit2D raycastHit2D = Physics2D.Raycast(raycastStartingPosition, raycastEndingPosition, PlayerInputs.instance.playerReach, defaultLayerMask);

        if (raycastHit2D.collider != null)
        {
            if (raycastHit2D.collider.gameObject.CompareTag("Ore"))
            {
                SetOreToMine(raycastHit2D.collider.gameObject.GetComponent<Ore>());
                return true;
            }
        }

        return false;
    }

    private bool PlayerIsInReachToMine(Vector2 mousePosition)
    {
        float distancePlayerMouseClick = Vector2.Distance(mousePosition, transform.position);
        return distancePlayerMouseClick <= PlayerInputs.instance.playerReach;
    }

    private bool MouseClickedOnAnOre(Vector2 mousePosition)
    {
        colliderDetectedByMouse = Physics2D.OverlapCircle(mousePosition, 0.05f);
        return colliderDetectedByMouse != null && colliderDetectedByMouse.gameObject.CompareTag("Ore");
    }

    private void SetOreToMine(Ore ore)
    {
        oreToMine = ore;

        PlayerInputs.instance.SpawnSelectSpotAtTransform(oreToMine.transform);
    }


    private void CheckCriticalMining()
    {
        if (PlayerInputs.instance.PlayerClickedMineButton())
        {
            if (canCriticalMine)
            {
                criticalMiningState = CriticalMiningState.SUCCEESSFUL;
                successCriticalMiningSoundEvent();
            }
            else
            {
                criticalMiningState = CriticalMiningState.FAILED;
                failCriticalMiningSoundEvent();
            }
        }
    }

    public void StartCriticalInterval()
    {
        canCriticalMine = true;
    }

    public void FinishCriticalInterval()
    {
        canCriticalMine = false;
    }

    private void StartMining()
    {
        playerMiningBuildUpSoundEvent();

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
            oreToMine.GetsMined(damageToDeal);

            if (oreToMine.Broke())
            {
                // Play normal mine sound
                if (playerBreaksOreEvent != null)
                    playerBreaksOreEvent();
            }
            else
            {
                // Play break sound
                if (playerMinesOreEvent != null) { }
                    playerMinesOreEvent();
            }
        }
    }

    private void ResetMining()
    {
        miningTime = 0;
        criticalMiningState = CriticalMiningState.NONE;

        playerStates.SetCurrentPlayerState(PlayerState.FREE);
        playerStates.SetCurrentPlayerAction(PlayerAction.IDLE);
    }

    private void Mine()
    {
        if (!miningAnOre)
            return;

        if (criticalMiningState == CriticalMiningState.SUCCEESSFUL)
        {
            MineOre(criticalMiningDamage);
        }
        else
        {
            MineOre(miningDamage);
        }
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
    }

    private void FlipPlayerSpriteFacingOreToMine()
    {
        Vector2 mousePosition = PlayerInputs.instance.GetMousePositionInWorld();

        if ((transform.position.x < mousePosition.x && !PlayerInputs.instance.facingLeft) ||
            (transform.position.x > mousePosition.x && PlayerInputs.instance.facingLeft))
        {
            Vector2 direction = mousePosition - (Vector2)transform.position;
            PlayerInputs.instance.FlipSprite(direction);
        }
    }


}