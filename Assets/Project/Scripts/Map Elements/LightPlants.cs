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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            plantAnimator.SetBool("playerInRange", false);
        }
    }

    IEnumerator PlayLightPlantParticles()
    {
        lightPlantParticles.Play();
        yield return new WaitForSeconds(0.3f);
        lightPlantParticles.Stop();
    }
}
