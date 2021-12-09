using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvents : MonoBehaviour
{
    // Public Attributes
    public AudioSource audioSourceWalking;
    public AudioClip playerWalkingSound;

    public AudioSource audioSourceCriticalMining;
    public AudioClip miningBuildUpSound;
    public AudioClip successCriticalMiningSound;
    public AudioClip failCriticalMiningSound;

    public AudioSource soundEffectsSource;
    public AudioClip miningOreSound;
    public AudioClip breaksOreSound;
    public AudioClip itemIsPickedUpSound;

    public AudioClip turnOnLampSound;
    public AudioClip turnOffLampSound;


    private void OnEnable()
    {
        PlayerMovement.playPlayerWalkingSoundEvent += PlayWalkingSound;
        PlayerMovement.pausePlayerWalkingSoundEvent += PauseWalkingSound;

        PlayerMiner.playerMiningBuildUpSoundEvent += PlayMiningBuildUpSound;
        PlayerMiner.successCriticalMiningSoundEvent += PlaySuccessCriticalMiningSound;
        PlayerMiner.failCriticalMiningSoundEvent += PlayFailCriticalMiningSound;
        PlayerMiner.playerMinesOreEvent += PlayMiningOreSound;
        PlayerMiner.playerBreaksOreEvent += PlayBreaksOreSound;

        Lamp.turnOnLanternSoundEvent += PlayTurnOnLampSound;
        Lamp.turnOffLanternSoundEvent += PlayTurnOffLampSound;

        PlayerInventory.playerPicksUpItemEvent += PlayPicksUpItemSound;
    }

    private void OnDisable()
    {
        PlayerMovement.playPlayerWalkingSoundEvent -= PlayWalkingSound;
        PlayerMovement.pausePlayerWalkingSoundEvent -= PauseWalkingSound;

        PlayerMiner.playerMiningBuildUpSoundEvent -= PlayMiningBuildUpSound;
        PlayerMiner.successCriticalMiningSoundEvent -= PlaySuccessCriticalMiningSound;
        PlayerMiner.failCriticalMiningSoundEvent -= PlayFailCriticalMiningSound;
        PlayerMiner.playerMinesOreEvent -= PlayMiningOreSound;
        PlayerMiner.playerBreaksOreEvent -= PlayBreaksOreSound;

        Lamp.turnOnLanternSoundEvent -= PlayTurnOnLampSound;
        Lamp.turnOffLanternSoundEvent -= PlayTurnOffLampSound;

        PlayerInventory.playerPicksUpItemEvent -= PlayPicksUpItemSound;
    }



    private void PlayWalkingSound()
    {
        if (audioSourceWalking.isPlaying) return;

        audioSourceWalking.pitch = Random.Range(0.8f, 1.3f);
        audioSourceWalking.clip = playerWalkingSound;
        audioSourceWalking.Play();
    }

    private void PauseWalkingSound()
    {
        if (!audioSourceWalking.isPlaying) return;
        audioSourceWalking.Pause();
    }


    private void PlayMiningBuildUpSound()
    {
        audioSourceCriticalMining.clip = miningBuildUpSound;
        audioSourceCriticalMining.Play();
    }

    private void PlaySuccessCriticalMiningSound()
    {
        audioSourceCriticalMining.clip = successCriticalMiningSound;
        audioSourceCriticalMining.Play();
    }

    private void PlayFailCriticalMiningSound()
    {
        audioSourceCriticalMining.clip = failCriticalMiningSound;
        audioSourceCriticalMining.Play();
    }

    private void PlayMiningOreSound()
    {
        soundEffectsSource.clip = miningOreSound;
        soundEffectsSource.Play();
    }

    private void PlayBreaksOreSound()
    {
        soundEffectsSource.clip = breaksOreSound;
        soundEffectsSource.Play();
    }

    private void PlayPicksUpItemSound()
    {
        soundEffectsSource.clip = itemIsPickedUpSound;
        soundEffectsSource.Play();
    }

    private void PlayTurnOnLampSound()
    {
        soundEffectsSource.clip = turnOnLampSound;
        soundEffectsSource.Play();
    }

    private void PlayTurnOffLampSound()
    {
        soundEffectsSource.clip = turnOffLampSound;
        soundEffectsSource.Play();
    }
}
