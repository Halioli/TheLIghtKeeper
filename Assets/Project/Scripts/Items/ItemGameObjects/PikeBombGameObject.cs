using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikeBombGameObject : ItemGameObject
{
    private const float FORCE = 12f;
    private const float OVERLAP_CIRCLE_RADIUS = 1.5f;
    private const int DAMAGE = 5;
    private Collider2D[] collidedElements;

    public AudioClip pikeBombUseSound;
    public AudioClip pikeBombExplosionSound;

    public override void DoFunctionality()
    {
        canBePickedUp = false;
        StartCoroutine("Functionality");
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

    IEnumerator Functionality()
    {
        Vector2 dir;
        dir = PlayerInputs.instance.GetMousePositionInWorld() - (Vector2)transform.position;
        dir = dir.normalized;

        FunctionalitySound();
        
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        rigidbody2D.AddForce(dir * FORCE, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);
        rigidbody2D.bodyType = RigidbodyType2D.Static;

        ExplosionSound();
        collidedElements = ReturnAllOverlapedColliders(transform.position);
        DamageAllCollided();
        
        Destroy(gameObject);
    }
}
