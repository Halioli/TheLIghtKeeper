using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGameObject : MonoBehaviour
{
    // Private Attributes
    private Rigidbody2D rigidbody2D;
    
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

    private void PlayDropSound()
    {
        audioSource.clip = itemIsDropped;
        audioSource.Play();
    }

    IEnumerator StopDroping(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        rigidbody2D.bodyType = RigidbodyType2D.Static;
    }



    public void StartDespawning()
    {
        StartCoroutine("Despawning");
    }

    IEnumerator Despawning()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        Color transparentColor = spriteRenderer.material.color;
        transparentColor.a = 0.0f;

        Color semiTransparentColor = spriteRenderer.material.color;
        semiTransparentColor.a = 0.8f;

        while (currentDespawnTimeInSeconds > START_DESPAWN_FADING_TIME)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            currentDespawnTimeInSeconds -= Time.deltaTime;
        }

        while (currentDespawnTimeInSeconds > 0.0f)
        {
            spriteRenderer.material.color = semiTransparentColor;
            yield return new WaitForSeconds(currentDespawnTimeInSeconds / DESPAWN_TIME_IN_SECONDS);
            spriteRenderer.material.color = transparentColor;
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
}
