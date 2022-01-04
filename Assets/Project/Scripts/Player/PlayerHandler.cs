using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    // Private Attributes
    private bool animationEnds = false;
    private HealthSystem playerHealthSystem;
    private Rigidbody2D playerRigidbody2D;

    // Public Attributes
    public Animator animator;
    public HUDHandler hudHandler;

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
                gameObject.layer = LayerMask.NameToLayer("Default"); // Enemies layer can't collide with Default layer
                StartCoroutine("DeathAnimation");
            }
            // Teleport to starting position (0, 0)
            else
            {
                gameObject.layer = LayerMask.NameToLayer("Player");
                playerRigidbody2D.transform.position = Vector3.zero;
                playerHealthSystem.RestoreHealthToMaxHealth();
                animationEnds = false;
            }
        }

        if (PlayerInputs.instance.PlayerPressedExitButton())
            PlayerInputs.instance.QuitGame();
    }

    public void DoDeathImageFade()
    {
        hudHandler.DoDeathImageFade();
    }

    public void DoFadeToBlack()
    {
        hudHandler.DoFadeToBlack();
    }

    public void RestoreHUD()
    {
        hudHandler.RestoreFades();
    }

    public void DeathAnimationFinished()
    {
        animationEnds = true;
    }

    IEnumerator DeathAnimation()
    {
        animator.SetBool("isDead", true);
        yield return new WaitUntil(() => animationEnds);
        animator.SetBool("isDead", false);
    }
}