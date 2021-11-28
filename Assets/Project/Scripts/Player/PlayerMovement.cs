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
        walkingParticleSystem.Play();
    }

    private void Update()
    {
        if (playerStates.PlayerStateIsFree())
        {
            moveDirection = playerInputs.PlayerPressedMovementButtons();
            if (moveDirection == Vector2.zero && playerStates.PlayerActionIsWalking())
            {
                playerStates.SetCurrentPlayerAction(PlayerAction.IDLE);
            }
            else if (moveDirection != Vector2.zero)
            {
                playerStates.SetCurrentPlayerAction(PlayerAction.WALKING);
                FlipSprite();
            }
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.AddForce(Vector2.right * 30f, ForceMode2D.Impulse);
        }
        */
    }

    private void FixedUpdate()
    {
        if (playerStates.PlayerActionIsWalking())
        {
            rigidbody2D.AddForce(moveDirection.normalized * moveSpeed);
            if (rigidbody2D.velocity.magnitude > 3f)
                rigidbody2D.velocity = rigidbody2D.velocity.normalized * Mathf.Lerp(rigidbody2D.velocity.magnitude, 3f, Time.fixedDeltaTime * 30f);
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
