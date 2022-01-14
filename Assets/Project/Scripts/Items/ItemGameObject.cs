using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemGameObject : MonoBehaviour
{
    // Private Attributes
    private Interpolator lerp;
    private float lerpDistance = 0.3f;
    private float halfLerpDistance = 0.15f;
    private float startYLerp;

    protected Rigidbody2D rigidbody2D;
    public bool canBePickedUp;

    private const float DROP_DOWN_FORCE_Y = 1.5f;
    private const float DROP_DOWN_TIME = 0.37f;

    private const float DROP_FORWARD_FORCE_X = 2.0f;
    private const float DROP_FORWARD_FORCE_Y = 2.5f;
    private const float DROP_FORWARD_TIME = 0.55f;

    private const float DESPAWN_TIME_IN_SECONDS = 10.0f;
    public float currentDespawnTimeInSeconds = DESPAWN_TIME_IN_SECONDS;
    private const float START_DESPAWN_FADING_TIME = 3.0f;

    // Public Attributes
    public Item item;

    // Audio
    public AudioSource audioSource;
    public AudioClip itemIsDropped;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        lerp = new Interpolator(1f, Interpolator.Type.SMOOTH);
        startYLerp = transform.position.y;

        rigidbody2D.gravityScale = 0f;
    }

    private void Update()
    {
        if (canBePickedUp)
            ItemFloating();
    }

    public void DropsDown()
    {
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        rigidbody2D.AddForce(transform.up * DROP_DOWN_FORCE_Y, ForceMode2D.Impulse);

        PlayDropSound();

        StartCoroutine("StopDroping", DROP_DOWN_TIME);
    }

    public void DropsForward(int directionX)
    {
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        rigidbody2D.AddForce(new Vector2(directionX * DROP_FORWARD_FORCE_X, DROP_FORWARD_FORCE_Y), ForceMode2D.Impulse);

        PlayDropSound();

        StartCoroutine("StopDroping", DROP_FORWARD_TIME);
    }

    public void DropsRandom(float despawnTime)
    {
        transform.DOJump(new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), 0), 0.1f, 1, 0.3f);
        StartDespawning(despawnTime);
    }

    public void DropsRandom()
    {
        transform.DOJump(new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), 0), 0.1f, 1, 0.3f);
        StartDespawning(DESPAWN_TIME_IN_SECONDS);
    }

    private void PlayDropSound()
    {
        audioSource.clip = itemIsDropped;
        audioSource.Play();
    }

    IEnumerator StopDroping(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        //rigidbody2D.bodyType = RigidbodyType2D.Static;
        rigidbody2D.gravityScale = 0f;
    }

    public void StartDespawning(float despawnTime)
    {
        StartCoroutine(Despawning(despawnTime));
    }

    IEnumerator Despawning(float despawnTime)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color transparentColor = spriteRenderer.color;
        transparentColor.a = 0.0f;

        Color semiTransparentColor = spriteRenderer.color;
        semiTransparentColor.a = 0.5f;

        currentDespawnTimeInSeconds = despawnTime;
        while (currentDespawnTimeInSeconds > START_DESPAWN_FADING_TIME)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            currentDespawnTimeInSeconds -= Time.deltaTime;
        }

        while (currentDespawnTimeInSeconds > 0.0f)
        {
            spriteRenderer.color = semiTransparentColor;
            yield return new WaitForSeconds(currentDespawnTimeInSeconds / DESPAWN_TIME_IN_SECONDS);

            spriteRenderer.color = transparentColor;
            yield return new WaitForSeconds(currentDespawnTimeInSeconds / DESPAWN_TIME_IN_SECONDS);

            semiTransparentColor.a -= 0.025f;

            currentDespawnTimeInSeconds -= 0.25f;
        }

        Destroy(gameObject);
    }

    public virtual void DoFunctionality()
    {
        // Consumible does functionality
    }

    public void SetSelfStatic()
    {
        rigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    public void SetSelfDynamic()
    {
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    private void ItemFloating()
    {
        lerp.Update(Time.deltaTime);
        if (lerp.isMinPrecise)
            lerp.ToMax();
        else if (lerp.isMaxPrecise)
            lerp.ToMin();

        transform.position = new Vector3(transform.position.x, startYLerp + (halfLerpDistance + lerpDistance * lerp.Value), 0f);
    }
}
