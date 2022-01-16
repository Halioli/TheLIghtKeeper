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
    public AudioSource soundEffectsSource2;
    public AudioSource soundEffectsSource3;
    public AudioClip miningOreSound;
    public AudioClip breaksOreSound;
    public AudioClip itemIsPickedUpSound;

    public AudioClip turnOnLanternSound;
    public AudioClip turnOffLanternSound;
    public AudioClip turnOnLanternDroneSound;
    public AudioClip turnOffLanternDroneSound;
    public AudioClip lanternDroneSound;

    private void OnEnable()
    {
        //PlayerMovement.playPlayerWalkingSoundEvent += PlayWalkingSound;
        //PlayerMovement.pausePlayerWalkingSoundEvent += PauseWalkingSound;

        //PlayerMiner.playerMiningBuildUpSoundEvent += PlayMiningBuildUpSound;
        //PlayerMiner.successCriticalMiningSoundEvent += PlaySuccessCriticalMiningSound;
        //PlayerMiner.failCriticalMiningSoundEvent += PlayFailCriticalMiningSound;
        //PlayerMiner.playerMinesOreEvent += PlayMiningOreSound;
        //PlayerMiner.playerBreaksOreEvent += PlayBreaksOreSound;

        //Lamp.turnOnLanternSoundEvent += PlayTurnOnLampSound;
        //Lamp.turnOffLanternSoundEvent += PlayTurnOffLampSound;
        //Lamp.turnOnLanternDroneSoundEvent += PlayerTurnOnLanternDroneSound;
        //Lamp.turnOffLanternDroneSoundEvent += PlayerTurnOffLanternDroneSound;
        //Lamp.playLanternDroneSoundEvent += PlayLanternDroneSound;
        //Lamp.stopLanternDroneSoundEvent += StopLanternDroneSound;

        //PlayerInventory.playerPicksUpItemEvent += PlayPicksUpItemSound;
    }

    private void OnDisable()
    {
        //PlayerMovement.playPlayerWalkingSoundEvent -= PlayWalkingSound;
        //PlayerMovement.pausePlayerWalkingSoundEvent -= PauseWalkingSound;

        //PlayerMiner.playerMiningBuildUpSoundEvent -= PlayMiningBuildUpSound;
        //PlayerMiner.successCriticalMiningSoundEvent -= PlaySuccessCriticalMiningSound;
        //PlayerMiner.failCriticalMiningSoundEvent -= PlayFailCriticalMiningSound;
        //PlayerMiner.playerMinesOreEvent -= PlayMiningOreSound;
        //PlayerMiner.playerBreaksOreEvent -= PlayBreaksOreSound;

        //Lamp.turnOnLanternSoundEvent -= PlayTurnOnLampSound;
        //Lamp.turnOffLanternSoundEvent -= PlayTurnOffLampSound;
        //Lamp.turnOnLanternDroneSoundEvent -= PlayerTurnOnLanternDroneSound;
        //Lamp.turnOffLanternDroneSoundEvent -= PlayerTurnOffLanternDroneSound;
        //Lamp.playLanternDroneSoundEvent -= PlayLanternDroneSound;
        //Lamp.stopLanternDroneSoundEvent -= StopLanternDroneSound;

        //PlayerInventory.playerPicksUpItemEvent -= PlayPicksUpItemSound;
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
        soundEffectsSource.clip = turnOnLanternSound;
        soundEffectsSource.Play();
    }

    private void PlayTurnOffLampSound()
    {
        soundEffectsSource.clip = turnOffLanternSound;
        soundEffectsSource.Play();
    }

    private void PlayerTurnOnLanternDroneSound()
    {
        soundEffectsSource2.clip = turnOnLanternDroneSound;
        soundEffectsSource2.Play();
    }

    private void PlayerTurnOffLanternDroneSound()
    {
        soundEffectsSource2.clip = turnOffLanternDroneSound;
        soundEffectsSource2.Play();
    }

    private void PlayLanternDroneSound()
    {
        if (soundEffectsSource3.isPlaying) return;

        soundEffectsSource3.clip = lanternDroneSound;
        soundEffectsSource3.Play();
    }

    private void StopLanternDroneSound()
    {
        soundEffectsSource3.clip = lanternDroneSound;
        soundEffectsSource3.Stop();
    }
}
