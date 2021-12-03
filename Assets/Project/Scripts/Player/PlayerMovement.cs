using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerBase
{
    // Private attributes
    private Vector2 moveDirection;
    private Rigidbody2D rigidbody2D;

    // Public attributes
    public float moveSpeed;
    public ParticleSystem walkingParticleSystem;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        walkingParticleSystem.Stop();
    }

    private void Update()
    {
        if (playerStates.PlayerStateIsFree())
        {
            moveDirection = playerInputs.PlayerPressedMovementButtons();
            if (moveDirection == Vector2.zero && playerStates.PlayerActionIsWalking())
            {
                playerStates.SetCurrentPlayerAction(PlayerAction.IDLE);
                walkingParticleSystem.Stop();
            }
            else if (moveDirection != Vector2.zero)
            {
                playerStates.SetCurrentPlayerAction(PlayerAction.WALKING);
                FlipSprite();
                walkingParticleSystem.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        if (playerStates.PlayerActionIsWalking())
        {
            rigidbody2D.velocity = moveDirection.normalized * moveSpeed;
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }


    private void FlipSprite()
    {
        if((moveDirection.x > 0 && playerInputs.facingRight) || moveDirection.x < 0 && !playerInputs.facingRight)
        {
            playerInputs.facingRight = !playerInputs.facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }


}
