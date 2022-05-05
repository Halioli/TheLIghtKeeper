using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightPlants : MonoBehaviour
{
    private const float MAX_INTENSITY = 1f;
    private const float MIN_INTENSITY = 0.6f;

    private float duration;
    private bool lightAtMin = true;
    private bool lightAtMax = false;
    private Animator plantAnimator;
    
    public CircleCollider2D lightTrigger;
    public Light2D lightPlant;
    public ParticleSystem lightPlantParticles;

    public AudioSource audioSource;
    public AudioClip OnAudioClip;
    public AudioClip OffAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        plantAnimator = GetComponent<Animator>();
        duration = Random.Range(1.5f, 2.5f);
    }

    private void Update()
    {
        if (lightAtMin)
        {
            StartCoroutine(ChangeIntensityToMax());
            lightAtMin = false;
        }
        else if (lightAtMax)
        {
            StartCoroutine(ChangeIntensityToMin());
            lightAtMax = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            plantAnimator.SetBool("playerInRange", true);
            StartCoroutine(PlayLightPlantParticles());
            OnSound();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            plantAnimator.SetBool("playerInRange", false);
            OffSound();
        }
    }

    private void OnSound()
    {
        audioSource.clip = OnAudioClip;
        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.Play();
    }

    private void OffSound()
    {
        audioSource.clip = OffAudioClip;
        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.Play();
    }

    IEnumerator PlayLightPlantParticles()
    {
        lightPlantParticles.Play();
        yield return new WaitForSeconds(0.3f);
        lightPlantParticles.Stop();
    }

    IEnumerator ChangeIntensityToMin()
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            lightPlant.intensity = Mathf.Lerp(MAX_INTENSITY, MIN_INTENSITY, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        lightPlant.intensity = MIN_INTENSITY;
        lightAtMin = true;
    }

    IEnumerator ChangeIntensityToMax()
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            lightPlant.intensity = Mathf.Lerp(MIN_INTENSITY, MAX_INTENSITY, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        lightPlant.intensity = MAX_INTENSITY;
        lightAtMax = true;
    }
}
