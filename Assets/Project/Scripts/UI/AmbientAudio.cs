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

    [SerializeField] AudioReverbZone audioReverbZone;
    AudioReverbPreset reverbInLight = AudioReverbPreset.Generic;
    AudioReverbPreset reverbInDarknessWithLantern = AudioReverbPreset.Hallway;
    AudioReverbPreset reverbInDarknessWithoutLantern = AudioReverbPreset.StoneCorridor;

    [SerializeField] AudioSource torchPilarAudioSource;

    [SerializeField] AudioClip heartBeatsClip;
    [SerializeField] AudioClip faintWindClip;


    private float musicVolume = 0.1f;
    private bool finishedTransition = false;
    private bool interior = false;
    private const float teleportMusicVolume = 0.5f;

    enum MusicFade { NORMAL, NONE, FADING_OUT, FADING_IN };
    MusicFade musicFade = MusicFade.NORMAL;
    private const float minMusicVolume = 0.02f;
    private const float maxMusicVolume = 0.1f;
    private const float musicFadeOutDuration = 5f;
    private const float musicFadeInDuration = 2.5f;


    private void Awake()
    {
        NormalAudioReverbZone();
    }

    void Start()
    {
        StartCoroutine(AmbientSounds());
        StartCoroutine(Music());
    }



    private void OnEnable()
    {
        ShipEntry.OnEntry += TransitionToInterior;
        ShipExit.OnExit += TransitionToExterior;
        ShipEntry.OnEntry += StopAmbientSounds;
        ShipExit.OnExit += PlayAmbientSounds;

        Teleporter.OnMenuEnter += TransitionToTeleportMenu;
        Teleporter.OnMenuExit += TransitionToExterior;

        PlayerLightChecker.OnPlayerEntersLight += DoMusicFadeIn;
        DarknessSystem.OnPlayerEntersLight += DoMusicFadeIn;
        PlayerLightChecker.OnPlayerInDarknessNoLantern += DoMusicFadeOut;

        DarknessSystem.OnPlayerEntersLight += NormalAudioReverbZone;
        DarknessSystem.OnPlayerNotInLight += DarknessWithLightAudioReverbZone;
        PlayerLightChecker.OnPlayerInDarknessNoLantern += DarknessWithoutLightAudioReverbZone;

        //PlayerLightChecker.OnPlayerEntersLight += PlayAmbientSounds;
        //DarknessSystem.OnPlayerEntersLight += PlayAmbientSounds;
        //PlayerLightChecker.OnPlayerInDarknessNoLantern += StopAmbientSounds;

        Torch.OnTorchStartActivation += TorchPilarIsActivatedSound;

        DarknessFaint.OnHeartBeatsStart += PlayHeartBeatsSound;
        DarknessFaint.OnFaintEnd += PlayFaintWindSound;
        DarknessFaint.OnFaintStop += StopFaintSounds;

        FogSystem.OnPlayerCaughtStart += PlayFaintWindSound;
        FogSystem.OnPlayerCaughtEnd += StopFaintSounds;
    }

    private void OnDisable()
    {
        ShipEntry.OnEntry -= TransitionToInterior;
        ShipExit.OnExit -= TransitionToExterior;
        ShipEntry.OnEntry -= StopAmbientSounds;
        ShipExit.OnExit -= PlayAmbientSounds;

        Teleporter.OnMenuEnter -= TransitionToTeleportMenu;
        Teleporter.OnMenuExit -= TransitionToExterior;

        PlayerLightChecker.OnPlayerEntersLight -= DoMusicFadeIn;
        DarknessSystem.OnPlayerEntersLight -= DoMusicFadeIn;
        PlayerLightChecker.OnPlayerInDarknessNoLantern -= DoMusicFadeOut;

        DarknessSystem.OnPlayerEntersLight -= NormalAudioReverbZone;
        DarknessSystem.OnPlayerNotInLight -= DarknessWithLightAudioReverbZone;
        PlayerLightChecker.OnPlayerInDarknessNoLantern -= DarknessWithoutLightAudioReverbZone;

        //PlayerLightChecker.OnPlayerEntersLight -= PlayAmbientSounds;
        //DarknessSystem.OnPlayerEntersLight -= PlayAmbientSounds;
        //PlayerLightChecker.OnPlayerInDarknessNoLantern -= StopAmbientSounds;

        Torch.OnTorchStartActivation -= TorchPilarIsActivatedSound;

        DarknessFaint.OnHeartBeatsStart -= PlayHeartBeatsSound;
        DarknessFaint.OnFaintEnd -= PlayFaintWindSound;
        DarknessFaint.OnFaintStop -= StopFaintSounds; 
        
        FogSystem.OnPlayerCaughtStart -= PlayFaintWindSound;
        FogSystem.OnPlayerCaughtEnd -= StopFaintSounds;
    }



    private void PlayAmbientSounds()
    {
        if (ambientCanPlay) return;

        ambientCanPlay = true;
        StartCoroutine("AmbientSounds");
    }

    private void StopAmbientSounds()
    {
        if (!ambientCanPlay) return;

        ambientCanPlay = false;
        StopCoroutine("AmbientSounds");
    }

    IEnumerator AmbientSounds()
    {
        ambientAudioSource.volume = 0.1f;
        while (ambientCanPlay)
        {
            yield return new WaitForSeconds(ambientCooldown);

            if (ambientCanPlay && Random.Range(0, randomChance) == 0)
            {
                ambientAudioSource.clip = ambientAudioClips[Random.Range(0, ambientAudioClips.Length)];
                ambientAudioSource.Play();
            }

            yield return new WaitUntil(() => !ambientAudioSource.isPlaying);
            yield return new WaitForSeconds(ambientCooldown);
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
        musicVolume = maxMusicVolume;
        musicAudioSource.volume = musicVolume;
        interior = true;
        musicAudioSource.Stop();
    }

    private void TransitionToExterior()
    {
        //MusicTransition(musicsOutside[Random.Range(0, musicsOutside.Length)], false);
        
        musicAudioSource.loop = false;
        musicVolume = maxMusicVolume;
        musicAudioSource.volume = musicVolume;
        interior = false;
        musicAudioSource.Stop();
    }


    //IEnumerator MusicTransition(AudioClip nextAudioClip, bool looping)
    //{
    //    finishedTransition = false;

    //    Interpolator fadeLerp = new Interpolator(1f, Interpolator.Type.SIN);
    //    fadeLerp.ToMin();

    //    while (!fadeLerp.IsMin)
    //    {
    //        fadeLerp.Update(Time.deltaTime);

    //        musicAudioSource.volume = fadeLerp.Value * musicVolume;

    //        yield return null;
    //    }

    //    musicAudioSource.clip = nextAudioClip;

    //    fadeLerp = new Interpolator(1f, Interpolator.Type.SIN);
    //    fadeLerp.ToMax();
    //    while (!fadeLerp.IsMax)
    //    {
    //        fadeLerp.Update(Time.deltaTime);
    //        musicAudioSource.volume = fadeLerp.Value * musicVolume;
    //        yield return null;
    //    }

    //    finishedTransition = true;
    //    musicAudioSource.loop = looping;

    //}


    private void TransitionToTeleportMenu()
    {
        interior = true;
        musicAudioSource.Stop();

        musicAudioSource.clip = teleportMusic;
        musicAudioSource.volume = teleportMusicVolume;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }



    private void DoMusicFadeOut()
    {
        if (musicFade == MusicFade.FADING_OUT || musicFade == MusicFade.NONE) return;
        else if (musicFade == MusicFade.FADING_IN) StopCoroutine("MusicFadeIn");

        StartCoroutine("MusicFadeOut");
    }

    IEnumerator MusicFadeOut()
    {
        musicFade = MusicFade.FADING_OUT;

        Interpolator fadeOutInterpolator = new Interpolator(musicFadeOutDuration, Interpolator.Type.SMOOTH);
        fadeOutInterpolator.ToMax();
        while (!fadeOutInterpolator.isMaxPrecise)
        {
            fadeOutInterpolator.Update(Time.deltaTime);
            musicVolume = fadeOutInterpolator.Inverse * maxMusicVolume;
            if (musicVolume > minMusicVolume) musicAudioSource.volume = musicVolume;
            yield return null;
        }

        musicVolume = minMusicVolume;
        musicAudioSource.volume = musicVolume;
        musicFade = MusicFade.NONE;
    }


    private void DoMusicFadeIn()
    {
        if (musicFade == MusicFade.FADING_IN || musicFade == MusicFade.NORMAL) return;
        else if (musicFade == MusicFade.FADING_OUT) StopCoroutine("MusicFadeOut");

        StartCoroutine("MusicFadeIn");
    }

    IEnumerator MusicFadeIn()
    {
        musicFade = MusicFade.FADING_IN;

        Interpolator fadeOutInterpolator = new Interpolator(musicFadeInDuration, Interpolator.Type.QUADRATIC);
        fadeOutInterpolator.ToMax();
        while (!fadeOutInterpolator.isMaxPrecise)
        {
            fadeOutInterpolator.Update(Time.deltaTime);
            musicVolume = fadeOutInterpolator.Value * maxMusicVolume;
            if (musicVolume < maxMusicVolume) musicAudioSource.volume = musicVolume;
            yield return null;
        }

        musicVolume = maxMusicVolume;
        musicAudioSource.volume = musicVolume;
        musicFade = MusicFade.NORMAL;
    }


    public void NormalAudioReverbZone()
    {
        audioReverbZone.reverbPreset = reverbInLight;
    }

    public void DarknessWithLightAudioReverbZone()
    {
        audioReverbZone.reverbPreset = reverbInDarknessWithLantern;
    }

    public void DarknessWithoutLightAudioReverbZone()
    {
        audioReverbZone.reverbPreset = reverbInDarknessWithoutLantern;
    }



    // Torches and  Pilar
    private void TorchPilarIsActivatedSound()
    {
        torchPilarAudioSource.Play();
    }


    // Faint
    private void PlayHeartBeatsSound()
    {
        StopAmbientSounds();

        ambientAudioSource.volume = 1f;
        ambientAudioSource.clip = heartBeatsClip;
        ambientAudioSource.Play();
    }

    private void PlayFaintWindSound()
    {
        ambientAudioSource.volume = 1f;
        ambientAudioSource.clip = faintWindClip;
        ambientAudioSource.Play();
    }

    private void StopFaintSounds()
    {
        ambientAudioSource.Stop();

        PlayAmbientSounds();
    }

}
