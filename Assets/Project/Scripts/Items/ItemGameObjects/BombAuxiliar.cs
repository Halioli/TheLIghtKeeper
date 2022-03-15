using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class BombAuxiliar : MonoBehaviour
{
    [SerializeField] float timeBeforeExplode = 2f;
    [SerializeField] float timeBeforeDetonation = 1.25f;
    private const float FORCE = 12f;
    private const float OVERLAP_CIRCLE_RADIUS = 2f;
    private const int DAMAGE = 5;
    private Collider2D[] collidedElements;
    private bool wasThrown = false;
    private Vector2 dir;

    [SerializeField] Hardness hardness = Hardness.NORMAL;
    [SerializeField] ConeLight light;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip bombUseSound;
    [SerializeField] AudioClip bombDetonationTick;
    [SerializeField] AudioClip bombExplosionSound;
    public Animator animator;



    //private void FixedUpdate()
    //{
    //    if (!wasThrown)
    //    {
    //        //ThrowBomb();
    //        wasThrown = true;
    //    }
    //}

    private void Start()
    {
        DoFunctionality();
    }


    public void DoFunctionality()
    {
        //rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        //gameObject.layer = LayerMask.NameToLayer("Default");

        StartCoroutine(Functionality());
        animator = GetComponent<Animator>();
        light.Expand(0.5f);
    }

    private void FunctionalitySound()
    {
        audioSource.clip = bombUseSound;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }

    private void DetonationTickSound()
    {
        audioSource.clip = bombDetonationTick;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }

    private void ExplosionSound()
    {
        audioSource.clip = bombExplosionSound;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
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
                Ore ore = collidedElements[i].GetComponent<Ore>();
                if (ore.hardness <= hardness) ore.GetsMined(DAMAGE, 1);
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
        yield return new WaitForSeconds(timeBeforeExplode - timeBeforeDetonation);

        DetonationTickSound();
        transform.DOShakeRotation(timeBeforeDetonation, 40, 20, 40, false);
        yield return new WaitForSeconds(timeBeforeDetonation);
        light.ExtraExpand(400, 400, 1.5f);
        light.SetDistance(OVERLAP_CIRCLE_RADIUS);


        //rigidbody2D.bodyType = RigidbodyType2D.Static;

        ExplosionSound();
        CinemachineShake.Instance.ShakeCamera(4f, 1f);
        animator.SetBool("explosion", true);

        yield return new WaitForSeconds(0.10f);

        collidedElements = ReturnAllOverlapedColliders(transform.position);
        DamageAllCollided();
    }

    //private void ThrowBomb()
    //{
    //    rigidbody2D.AddForce(dir * FORCE, ForceMode2D.Impulse);
    //}


}
