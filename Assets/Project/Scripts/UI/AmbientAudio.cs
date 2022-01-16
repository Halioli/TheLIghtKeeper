using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudio : MonoBehaviour
{
    [SerializeField] bool ambientCanPlay;
    [SerializeField] int randomChance;
    [SerializeField] float ambientCooldown;

    [SerializeField] AudioSource ambientAudioSource;
    [SerializeField] AudioClip[] ambientAudioClips;



    void Start()
    {
        StartCoroutine(AmbientCooldown());
    }


    IEnumerator AmbientCooldown()
    {
        while (ambientCanPlay)
        {
            yield return new WaitForSeconds(ambientCooldown);

            if (Random.Range(0, randomChance) == 0)
            {
                ambientAudioSource.clip = ambientAudioClips[Random.Range(0, ambientAudioClips.Length)];
                ambientAudioSource.Play();
            }

            yield return new WaitUntil(() => !ambientAudioSource.isPlaying);
        }

    }


}
