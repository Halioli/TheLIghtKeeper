using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerInputs
{
    // Private attributes
    private Vector2 moveDirection;
    private Rigidbody2D rigidbody2D;

    // Public attributes
    public float moveSpeed;
    public ParticleSystem walkingParticleSystem;
    public PlayerMiner playerMiner;

    private Vector3 playerPos;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        facingRight = false;
        walkingParticleSystem.Stop();
        playerMiner = GetComponent<PlayerMiner>();
    }

    private void Update()
    {
        FlipSpriteIfClickedAnOre();

        if (!playerMiner.IsMining())
        {
            moveDirection = PlayerPressedMovementButtons();
            rigidbody2D.velocity = moveDirection.normalized * moveSpeed;
            FlipSprite();
            CheckPartlicleSystemActive();
        }else
        {
            rigidbody2D.velocity = Vector2.zero;
        }
        playerPos = transform.position;
    }

    private void FlipSprite()
    {
        if((moveDirection.x > 0 && facingRight) || moveDirection.x < 0 && !facingRight){
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    private void CheckPartlicleSystemActive()
    {
        if (rigidbody2D.velocity.x != 0f || rigidbody2D.velocity.y != 0f)
        {
            if (!walkingParticleSystem.isPlaying)
            {
                walkingParticleSystem.Play();
            }
        }
        else
        {
            walkingParticleSystem.Stop();
        }
    }

    private void FlipSpriteIfClickedAnOre()
    {
        if (PlayerClickedMineButton())
        {
            if (mousePosition.x > playerPos.x && !facingRight || mousePosition.x < playerPos.x && facingRight)
            {
                facingRight = !facingRight;
                transform.Rotate(new Vector3(0, 180, 0));
            }
        }
      
    }
}
