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

    public AudioSource audioSource;
    public AudioClip miningOreSound;
    public AudioClip breaksOreSound;
    public AudioClip itemIsPickedUpSound;



    private void OnEnable()
    {
        PlayerMovement.playPlayerWalkingSoundEvent += PlayWalkingSound;
        PlayerMovement.pausePlayerWalkingSoundEvent += PauseWalkingSound;

        PlayerMiner.playerMiningBuildUpSoundEvent += PlayMiningBuildUpSound;
        PlayerMiner.successCriticalMiningSoundEvent += PlaySuccessCriticalMiningSound;
        PlayerMiner.failCriticalMiningSoundEvent += PlayFailCriticalMiningSound;
        PlayerMiner.playerMinesOreEvent += PlayMiningOreSound;
        PlayerMiner.playerBreaksOreEvent += PlayBreaksOreSound;

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
        audioSource.clip = miningOreSound;
        audioSource.Play();
    }

    private void PlayBreaksOreSound()
    {
        audioSource.clip = breaksOreSound;
        audioSource.Play();
    }

    private void PlayPicksUpItemSound()
    {
        audioSource.clip = itemIsPickedUpSound;
        audioSource.Play();
    }
}
