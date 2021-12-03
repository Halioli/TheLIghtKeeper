using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : PlayerInputs
{
    // Public Attributes
    private HealthSystem playerHealthSystem;
    private Rigidbody2D playerRigidbody2D;
    private bool animationEnds = false;

    public Animator animator;

    private void Start()
    {
        playerHealthSystem = GetComponent<HealthSystem>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerHealthSystem.IsDead())
        {
            //Start corroutine and play animation
            if (!animationEnds)
            {
                StartCoroutine("DeathAnimation");
            }
            // Teleport to starting position (0, 0)
            else
            {

                playerRigidbody2D.transform.position = Vector3.zero;
                playerHealthSystem.RestoreHealthToMaxHealth();
   
            }
        }
    }
    IEnumerator DeathAnimation()
    {
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(2.6f);
        animator.SetBool("isDead", false);
        animationEnds = true;
    }
}