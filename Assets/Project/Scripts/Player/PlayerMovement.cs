using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerBase
{
    // Private attributes
    private Vector2 moveDirection;
    private Rigidbody2D rigidbody2D;
    private bool beingPushed = false;
    private Vector2 pushDirection = new Vector2();
    private float pushForce = 0f;

    // Public attributes
    public float moveSpeed;
    public ParticleSystem walkingParticleSystem;
    public Animator animator;

    // Events
    public delegate void PlayerWalkingSound();
    public static event PlayerWalkingSound playPlayerWalkingSoundEvent;
    public static event PlayerWalkingSound pausePlayerWalkingSoundEvent;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        walkingParticleSystem.Play();
    }

    private void Update()
    {
        if (playerStates.PlayerStateIsFree())
        {
            moveDirection = PlayerInputs.instance.PlayerPressedMovementButtons();
            if (moveDirection == Vector2.zero && playerStates.PlayerActionIsWalking())
            {
                playerStates.SetCurrentPlayerAction(PlayerAction.IDLE);
                //Update speed for walk animation
                animator.SetBool("isWalking", false);
                pausePlayerWalkingSoundEvent();
            }
            else if (moveDirection != Vector2.zero)
            {
                playerStates.SetCurrentPlayerAction(PlayerAction.WALKING);
                FlipSprite();
                animator.SetBool("isWalking", true);
                playPlayerWalkingSoundEvent();
            }
        }
                
    }

    private void FixedUpdate()
    {
        if (beingPushed)
        {
            rigidbody2D.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            beingPushed = false;
        }
        if (playerStates.PlayerActionIsWalking())
        {
            rigidbody2D.AddForce(moveDirection.normalized * moveSpeed);
            if (rigidbody2D.velocity.magnitude > 3f)
                rigidbody2D.velocity = rigidbody2D.velocity.normalized * Mathf.Lerp(rigidbody2D.velocity.magnitude, 3f, Time.fixedDeltaTime * 30f);
        }
    }


    private void FlipSprite()
    {
        if (!PlayerInputs.instance.canFlip)
            return;

        if((moveDirection.x > 0 && !PlayerInputs.instance.facingLeft) || moveDirection.x < 0 && PlayerInputs.instance.facingLeft)
        {
            PlayerInputs.instance.facingLeft = !PlayerInputs.instance.facingLeft;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
    }

    public void GetsPushed(Vector2 newPushDirection, float newPushForce)
    {
        beingPushed = true;
        pushDirection = newPushDirection;
        pushForce = newPushForce;
    }
}
