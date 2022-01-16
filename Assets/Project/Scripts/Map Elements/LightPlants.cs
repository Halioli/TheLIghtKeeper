using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightPlants : MonoBehaviour
{
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
}
