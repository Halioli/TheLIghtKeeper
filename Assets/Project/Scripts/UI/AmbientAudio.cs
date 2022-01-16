using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudio : MonoBehaviour
{
    [SerializeField] bool ambientCanPlay = true;
    [SerializeField] bool musicCanPlay = true;
    [SerializeField] int randomChance;
    [SerializeField] float ambientCooldown;

    [SerializeField] AudioSource ambientAudioSource;
    [SerializeField] AudioClip[] ambientAudioClips;

    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioClip musicInside;
    [SerializeField] AudioClip[] musicsOutside;

    private const float musicVolume = 0.1f;

    void Start()
    {
        StartCoroutine(AmbientSounds());
        StartCoroutine(Music());

        musicAudioSource.Play();
    }


    private void OnEnable()
    {
        // += TransitionToInterior;
        // += TransitionToExterior;
    }

    private void OnDisable()
    {
        // -= TransitionToInterior;
        // -= TransitionToExterior;
    }



    IEnumerator AmbientSounds()
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


    IEnumerator Music()
    {
        while (musicCanPlay)
        {
            musicAudioSource.clip = musicsOutside[Random.Range(0, musicsOutside.Length)];
            musicAudioSource.Play();
            yield return new WaitUntil(() => !musicAudioSource.isPlaying);

            TransitionToExterior();
        }
    }


    private void TransitionToInterior()
    {
        MusicTransition(musicInside);
    }

    private void TransitionToExterior()
    {
        MusicTransition(musicsOutside[Random.Range(0, musicsOutside.Length)]);
    }


    IEnumerator MusicTransition(AudioClip nextAudioClip)
    {
        Interpolator fadeLerp = new Interpolator(1f, Interpolator.Type.SIN);
        fadeLerp.ToMin();

        while (!fadeLerp.isMinPrecise)
        {
            fadeLerp.Update(Time.deltaTime);

            musicAudioSource.volume = fadeLerp.Value * musicVolume;

            yield return null;
        }

        musicAudioSource.clip = nextAudioClip;

        fadeLerp = new Interpolator(1f, Interpolator.Type.SIN);
        fadeLerp.ToMax();
        while (!fadeLerp.isMaxPrecise)
        {
            fadeLerp.Update(Time.deltaTime);
            musicAudioSource.volume = fadeLerp.Value * musicVolume;
            yield return null;
        }
    }


}
