using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerInputs
{
    // Private attributes
    private Vector2 moveDirection;
    private Rigidbody2D rigidbody2D;
    private bool facingRight;

    // Public attributes
    public float moveSpeed;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        facingRight = false;
    }

    private void Update()
    {
        moveDirection = PlayerPressedMovementButtons();
        rigidbody2D.velocity = moveDirection.normalized * moveSpeed;
        FlipSprite();
    }

    private void FlipSprite()
    {
        if((moveDirection.x > 0 && facingRight) || moveDirection.x < 0 && !facingRight){
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
