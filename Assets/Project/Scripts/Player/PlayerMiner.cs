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
    private const float LOWER_INTERVAL_CRITICAL_MINING = 0.4f;
    private const float UPPER_INTERVAL_CRITICAL_MINING = 0.9f;
    
    // Public Attributes
    public GameObject interactArea;

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
            //if (PlayerIsInReachToMine(PlayerInputs.instance.mouseWorldPosition) && MouseClickedOnAnOre(PlayerInputs.instance.mouseWorldPosition))
            //{
                SetOreToMine(interactArea.GetComponent<MineArea>().GetClosestOreToCollider());
                StartMining();
            //}
        }
        
    }

    // METHODS
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
        oreToMine = ore; //colliderDetectedByMouse.gameObject.GetComponent<Ore>();

        PlayerInputs.instance.SpawnSelectSpotAtTransform(oreToMine.transform);
    }


    private void CheckCriticalMining()
    {
        if (PlayerInputs.instance.PlayerClickedMineButton())
        {
            if (WithinCriticalInterval())
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

    private bool WithinCriticalInterval()
    {
        return miningTime >= LOWER_INTERVAL_CRITICAL_MINING && miningTime <= UPPER_INTERVAL_CRITICAL_MINING;
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
        if ((transform.position.x < oreToMine.transform.position.x && !PlayerInputs.instance.facingLeft) ||
            (transform.position.x > oreToMine.transform.position.x && PlayerInputs.instance.facingLeft))
        {
            Vector2 direction = oreToMine.transform.position - transform.position;
            PlayerInputs.instance.FlipSprite(direction);
        }
    }


}