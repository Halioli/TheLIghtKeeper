using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum CriticalMiningState { NONE, FAILED, SUCCEESSFUL };

public class PlayerMiner : PlayerBase
{
    // Private Attributes
    private Collider2D colliderDetectedByMouse = null;
    private Ore oreToMine;

    private float miningReachRadius = 3f;

    private const int START_MINING_DAMAGE = 1;
    private int miningDamage = START_MINING_DAMAGE;
    private const int START_CRITICAL_MINING_DAMAGE = 2;
    private int criticalMiningDamage = START_CRITICAL_MINING_DAMAGE;

    private CriticalMiningState criticalMiningState = CriticalMiningState.NONE;
    private const float START_MINING_TIME = 1.0f;
    private float miningTime = START_MINING_TIME;
    private const float LOWER_INTERVAL_CRITICAL_MINING = 0.5f;
    private const float UPPER_INTERVAL_CRITICAL_MINING = 0.7f;


    // Events
    public delegate void PlayPlayerSound();
    public static event PlayPlayerSound playerMinesOreEvent;
    public static event PlayPlayerSound playerBreaksOreEvent;


    void Update()
    {
        if (playerInputs.PlayerClickedMineButton() && playerStates.PlayerStateIsFree())
        {
            playerInputs.SetNewMousePosition();
            if (PlayerIsInReachToMine(playerInputs.mouseWorldPosition) && MouseClickedOnAnOre(playerInputs.mouseWorldPosition))
            {
                SetOreToMine();
                StartMining();
            }
        }
        
    }




    // METHODS
    private bool PlayerIsInReachToMine(Vector2 mousePosition)
    {
        float distancePlayerMouseClick = Vector2.Distance(mousePosition, transform.position);
        return distancePlayerMouseClick <= miningReachRadius;
    }

    private bool MouseClickedOnAnOre(Vector2 mousePosition)
    {
        colliderDetectedByMouse = Physics2D.OverlapCircle(mousePosition, 0.05f);
        return colliderDetectedByMouse != null && colliderDetectedByMouse.gameObject.CompareTag("Ore");
    }

    private void SetOreToMine()
    {
        oreToMine = colliderDetectedByMouse.gameObject.GetComponent<Ore>();
    }


    private void CheckCriticalMining()
    {
        if (playerInputs.PlayerClickedMineButton())
        {
            if (WithinCriticalInterval())
            {
                criticalMiningState = CriticalMiningState.SUCCEESSFUL;
            }
            else
            {
                criticalMiningState = CriticalMiningState.FAILED;
            }
        }
    }

    private bool WithinCriticalInterval()
    {
        return miningTime >= LOWER_INTERVAL_CRITICAL_MINING && miningTime <= UPPER_INTERVAL_CRITICAL_MINING;
    }


    private void StartMining()
    {
        playerStates.SetCurrentPlayerState(PlayerState.BUSSY); 
        playerStates.SetCurrentPlayerAction(PlayerAction.MINING);
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
                if (playerMinesOreEvent != null)
                    playerMinesOreEvent();
            }
        }
    }

    private void ResetMining()
    {
        miningTime = START_MINING_TIME;
        criticalMiningState = CriticalMiningState.NONE;

        playerStates.SetCurrentPlayerState(PlayerState.FREE);
        playerStates.SetCurrentPlayerAction(PlayerAction.IDLE);
    }

    private void Mine()
    {
        if (criticalMiningState == CriticalMiningState.SUCCEESSFUL)
        {
            MineOre(criticalMiningDamage);
        }
        else
        {
            MineOre(miningDamage);
        }

        ResetMining();
    }

    IEnumerator Mining()
    {
        while (miningTime > 0.0f)
        {
            CheckCriticalMining();

            yield return new WaitForSeconds(Time.deltaTime);
            miningTime -= Time.deltaTime;

        }
    }


}