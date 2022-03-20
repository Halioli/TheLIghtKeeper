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
    [SerializeField] AudioClip teleportMusic;

    private const float musicVolume = 0.1f;
    private bool finishedTransition = false;
    private bool interior = false;
    private const float teleportMusicVolume = 0.5f;


    void Start()
    {
        //StartCoroutine(AmbientSounds());
        StartCoroutine(Music());
        //musicAudioSource.
    }



    private void OnEnable()
    {
        ShipEntry.OnEntry += TransitionToInterior;
        ShipExit.OnExit += TransitionToExterior;

        Teleporter.OnMenuEnter += TransitionToTeleportMenu;
        Teleporter.OnMenuExit += TransitionToExterior;
    }

    private void OnDisable()
    {
        ShipEntry.OnEntry -= TransitionToInterior;
        ShipExit.OnExit -= TransitionToExterior;

        Teleporter.OnMenuEnter -= TransitionToTeleportMenu;
        Teleporter.OnMenuExit -= TransitionToExterior;
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
        musicAudioSource.clip = musicsOutside[Random.Range(0, musicsOutside.Length)];
        musicAudioSource.Play();

        while (musicCanPlay)
        {
            yield return new WaitUntil(() => !musicAudioSource.isPlaying);

            //TransitionToExterior();
            //yield return new WaitUntil(() => finishedTransition);

            if (interior)
            {
                musicAudioSource.clip = musicInside;
                musicAudioSource.Play();
            }
            else
            {
                musicAudioSource.clip = musicsOutside[Random.Range(0, musicsOutside.Length)];
                musicAudioSource.Play();
            }


        }
    }


    private void TransitionToInterior()
    {
        //MusicTransition(musicInside, true);

        musicAudioSource.loop = false;
        musicAudioSource.volume = musicVolume;
        interior = true;
        musicAudioSource.Stop();
    }

    private void TransitionToExterior()
    {
        //MusicTransition(musicsOutside[Random.Range(0, musicsOutside.Length)], false);
        
        musicAudioSource.loop = false;
        musicAudioSource.volume = musicVolume;
        interior = false;
        musicAudioSource.Stop();
    }


    IEnumerator MusicTransition(AudioClip nextAudioClip, bool looping)
    {
        finishedTransition = false;

        Interpolator fadeLerp = new Interpolator(1f, Interpolator.Type.SIN);
        fadeLerp.ToMin();

        while (!fadeLerp.IsMin)
        {
            fadeLerp.Update(Time.deltaTime);

            musicAudioSource.volume = fadeLerp.Value * musicVolume;

            yield return null;
        }

        musicAudioSource.clip = nextAudioClip;

        fadeLerp = new Interpolator(1f, Interpolator.Type.SIN);
        fadeLerp.ToMax();
        while (!fadeLerp.IsMax)
        {
            fadeLerp.Update(Time.deltaTime);
            musicAudioSource.volume = fadeLerp.Value * musicVolume;
            yield return null;
        }

        finishedTransition = true;
        musicAudioSource.loop = looping;

    }


    private void TransitionToTeleportMenu()
    {
        interior = true;
        musicAudioSource.Stop();

        musicAudioSource.clip = teleportMusic;
        musicAudioSource.volume = teleportMusicVolume;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

}
