using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState { BUSSY, FREE, DEAD };
public enum PlayerAction { IDLE, WALKING, MINING, ATTACKING, INTERACTING };


public class PlayerStates : MonoBehaviour
{
    private PlayerState currentPlayerState;
    private PlayerAction currentPlayerAction;


    void Awake()
    {
        currentPlayerState = PlayerState.FREE;
        currentPlayerAction = PlayerAction.IDLE;
    }


    public void SetCurrentPlayerState(PlayerState playerStateToSet)
    {
        currentPlayerState = playerStateToSet;
    }

    public bool PlayerStateIsBussy()
    {
        return currentPlayerState == PlayerState.BUSSY;
    }

    public bool PlayerStateIsFree()
    {
        return currentPlayerState == PlayerState.FREE;
    }

    public bool PlayerStateIsDead()
    {
        return currentPlayerState == PlayerState.DEAD;
    }


    public void SetCurrentPlayerAction(PlayerAction playerActionToSet)
    {
        currentPlayerAction = playerActionToSet;
    }

    public bool PlayerActionIsIdle()
    {
        return currentPlayerAction == PlayerAction.IDLE;
    }

    public bool PlayerActionIsWalking()
    {
        return currentPlayerAction == PlayerAction.WALKING;
    }

    public bool PlayerActionIsMining()
    {
        return currentPlayerAction == PlayerAction.MINING;
    }

    public bool PlayerActionIsAttacking()
    {
        return currentPlayerAction == PlayerAction.ATTACKING;
    }

    public bool PlayerActionIsInteracting()
    {
        return currentPlayerAction == PlayerAction.INTERACTING;
    }
}
