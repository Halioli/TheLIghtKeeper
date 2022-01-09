using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioSource walkingAudioSource;
    [SerializeField] AudioSource itemPickUpAudioSource;
    [SerializeField] AudioSource attackAndMineAudioSource;
    [SerializeField] AudioSource receiveDamageAudioSource;
    [SerializeField] AudioSource mineBuildUpAudioSource;

    [SerializeField] AudioClip attackAudioSound;
    [SerializeField] AudioClip mineAudioSound;

    [SerializeField] AudioClip miningBuildUpSound;
    [SerializeField] AudioClip successCriticalMiningSound;
    [SerializeField] AudioClip failCriticalMiningSound;



    private void OnEnable()
    {
        // Walking sound
        PlayerMovement.playPlayerWalkingSoundEvent += PlayWalkingSound;
        PlayerMovement.pausePlayerWalkingSoundEvent += StopWalkingSound;

        // ItemPickUp sound
        PlayerInventory.playerPicksUpItemEvent += PlayItemPickUpSound;

        // Attack sound
        PlayerCombat.playerAttackEvent += PlayAttackSound;

        // ReceiveDamage sound
        PlayerCombat.playerReceivesDamageEvent += PlayReceiveDamageSound;

        // Mine sound
        PlayerMiner.playerMineEvent -= PlayMineSound;

        // MineBuildUp sound
        PlayerMiner.playerMinesEvent += PlayMineBuildUpSound;
        PlayerMiner.playerSucceessfulMineEvent += PlaySuccessfulMineSound;
        PlayerMiner.playerFailMineEvent += PlayFailMineSound;
    }


    private void OnDisable()
    {
        // Walking sound
        PlayerMovement.playPlayerWalkingSoundEvent -= PlayWalkingSound;
        PlayerMovement.pausePlayerWalkingSoundEvent -= StopWalkingSound;

        // ItemPickUp sound
        PlayerInventory.playerPicksUpItemEvent -= PlayItemPickUpSound;

        // Attack sound
        PlayerCombat.playerAttackEvent -= PlayAttackSound;

        // ReceiveDamage sound
        PlayerCombat.playerReceivesDamageEvent -= PlayReceiveDamageSound;

        // Mine sound
        PlayerMiner.playerMineEvent -= PlayMineSound;

        // MineBuildUp sound
        PlayerMiner.playerMinesEvent -= PlayMineBuildUpSound;
        PlayerMiner.playerSucceessfulMineEvent -= PlaySuccessfulMineSound;
        PlayerMiner.playerFailMineEvent -= PlayFailMineSound;
    }



    // Walking sound
    public void PlayWalkingSound()
    {
        walkingAudioSource.Play();
    }

    public void StopWalkingSound()
    {
        walkingAudioSource.Stop();
    }


    // ItemPickUp sound
    public void PlayItemPickUpSound()
    {
        itemPickUpAudioSource.Play();
    }


    // Attack sound
    public void PlayAttackSound()
    {
        attackAndMineAudioSource.clip = attackAudioSound;
        attackAndMineAudioSource.pitch = Random.Range(0.8f, 1.3f);
        attackAndMineAudioSource.Play();
    }

    // ReceiveDamage sound
    public void PlayReceiveDamageSound()
    {
        receiveDamageAudioSource.pitch = Random.Range(0.8f, 1.3f);
        receiveDamageAudioSource.Play();
    }


    // Mine sound
    public void PlayMineSound()
    {
        attackAndMineAudioSource.clip = mineAudioSound;
        attackAndMineAudioSource.Play();
    }

    // MineBuildUp sound
    public void PlayMineBuildUpSound()
    {
        mineBuildUpAudioSource.clip = miningBuildUpSound;
        mineBuildUpAudioSource.Play();
    }

    public void PlaySuccessfulMineSound()
    {
        mineBuildUpAudioSource.clip = successCriticalMiningSound;
        mineBuildUpAudioSource.Play();
    }

    public void PlayFailMineSound()
    {
        mineBuildUpAudioSource.clip = failCriticalMiningSound;
        mineBuildUpAudioSource.Play();
    }


}