using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikeBombGameObject : ItemGameObject
{
    private const float FORCE = 12f;
    private const float OVERLAP_CIRCLE_RADIUS = 1.5f;
    private const int DAMAGE = 5;
    private Collider2D[] collidedElements;
    private bool wasThrown = false;
    private Vector2 dir;

    public AudioClip pikeBombUseSound;
    public AudioClip pikeBombExplosionSound;
    public Animator animator;



    private void FixedUpdate()
    {
        if (!wasThrown)
        {
            ThrowBomb();
            wasThrown = true;
        }
    }


    public override void DoFunctionality()
    {
        permanentNotPickedUp = true;
        canBePickedUp = false;
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        gameObject.layer = LayerMask.NameToLayer("Default");

        StartCoroutine(Functionality());
        animator = GetComponent<Animator>();
    }

    private void FunctionalitySound()
    {
        audioSource.clip = pikeBombUseSound;
        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.Play();
    }

    private void ExplosionSound()
    {
        audioSource.clip = pikeBombExplosionSound;
        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.Play();
    }

    private Collider2D[] ReturnAllOverlapedColliders(Vector2 position)
    {
        return Physics2D.OverlapCircleAll(position, OVERLAP_CIRCLE_RADIUS);
    }

    private void DamageAllCollided()
    {
        for (int i = 0; i < collidedElements.Length; i++)
        {
            if (collidedElements[i].CompareTag("Ore"))
            {
                collidedElements[i].GetComponent<Ore>().GetsMined(DAMAGE, 1);
            }
            else if (collidedElements[i].CompareTag("Enemy"))
            {
                collidedElements[i].GetComponent<Enemy>().ReceiveDamage(DAMAGE);
            }
            else if (collidedElements[i].CompareTag("Player"))
            {
                collidedElements[i].GetComponent<PlayerCombat>().ReceiveDamage(DAMAGE);
            }
        }
    }

    private void DestroyBomb()
    {
        Destroy(gameObject);
    }

    IEnumerator Functionality()
    {
        dir = PlayerInputs.instance.GetMousePositionInWorld() - (Vector2)transform.position;
        dir = dir.normalized;

        FunctionalitySound();

        yield return new WaitForSeconds(0.4f);
        rigidbody2D.bodyType = RigidbodyType2D.Static;

        ExplosionSound();
        animator.SetBool("explosion", true);
        collidedElements = ReturnAllOverlapedColliders(transform.position);
        DamageAllCollided();
    }

    private void ThrowBomb()
    {
        rigidbody2D.AddForce(dir * FORCE, ForceMode2D.Impulse);
    }
}
