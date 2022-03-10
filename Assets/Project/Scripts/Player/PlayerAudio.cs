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
    [SerializeField] AudioSource lanternDroneAudioSource;
    [SerializeField] AudioSource lanternDroneOnOffAudioSource;
    [SerializeField] AudioSource lanternOnOffAudioSource;

    [SerializeField] AudioClip normalWalkingSound;
    [SerializeField] AudioClip grassWalkingSound;

    [SerializeField] AudioClip attackAudioSound;
    [SerializeField] AudioClip missAttackAudioSound;
    [SerializeField] AudioClip mineAudioSound;
    [SerializeField] AudioClip mineBreakAudioSound;

    [SerializeField] AudioClip miningBuildUpSound;
    [SerializeField] AudioClip successCriticalMiningSound;
    [SerializeField] AudioClip failCriticalMiningSound;

    [SerializeField] AudioClip turnOnLanternSound;
    [SerializeField] AudioClip turnOffLanternSound;
    [SerializeField] AudioClip turnOnLanternDroneSound;
    [SerializeField] AudioClip turnOffLanternDroneSound;
    [SerializeField] AudioClip refillLanternSound;



    private void OnEnable()
    {
        // Walking sound
        PlayerMovement.playPlayerWalkingSoundEvent += PlayWalkingSound;
        PlayerMovement.pausePlayerWalkingSoundEvent += StopWalkingSound;
        LandscapeInteractor.OnGrassEnter += SetGrassWalkingSound;
        LandscapeInteractor.OnGrassExit += SetNormalWalkingSound;

        // ItemPickUp sound
        PlayerInventory.playerPicksUpItemEvent += PlayItemPickUpSound;

        // Attack sound
        PlayerCombat.playerAttackEvent += PlayAttackSound;

        // ReceiveDamage sound
        PlayerCombat.playerReceivesDamageEvent += PlayReceiveDamageSound;

        // Mine sound
        PlayerMiner.playerMineEvent += PlayMineSound;
        PlayerMiner.playerBreaksOreEvent += PlayMineBreakSound;

        // MineBuildUp sound
        PlayerMiner.playerMinesEvent += PlayMineBuildUpSound;
        PlayerMiner.playerSucceessfulMineEvent += PlaySuccessfulMineSound;
        PlayerMiner.playerFailMineEvent += PlayFailMineSound;

        // Lantern sound
        Lamp.turnOnLanternEvent += PlayLanternDroneSound;
        Lamp.turnOffLanternEvent += StopLanternDroneSound;
        Lamp.turnOnLanternDroneSoundEvent += PlayTurnOnLanternDroneSound;
        Lamp.turnOffLanternDroneSoundEvent += PlayTurnOffLanternDroneSound;
        Lamp.turnOnLanternEvent += PlayTurnOnLanternSound;
        Lamp.turnOffLanternEvent += PlayTurnOffLanternSound;
        LanternFuelGameObject.onLanternFuelRefill += PlayRefillLanternSound;
    }


    private void OnDisable()
    {
        // Walking sound
        PlayerMovement.playPlayerWalkingSoundEvent -= PlayWalkingSound;
        PlayerMovement.pausePlayerWalkingSoundEvent -= StopWalkingSound;
        LandscapeInteractor.OnGrassEnter -= SetGrassWalkingSound;
        LandscapeInteractor.OnGrassExit -= SetNormalWalkingSound;

        // ItemPickUp sound
        PlayerInventory.playerPicksUpItemEvent -= PlayItemPickUpSound;

        // Attack sound
        PlayerCombat.playerAttackEvent -= PlayAttackSound;

        // ReceiveDamage sound
        PlayerCombat.playerReceivesDamageEvent -= PlayReceiveDamageSound;

        // Mine sound
        PlayerMiner.playerMineEvent -= PlayMineSound;
        PlayerMiner.playerBreaksOreEvent -= PlayMineBreakSound;

        // MineBuildUp sound
        PlayerMiner.playerMinesEvent -= PlayMineBuildUpSound;
        PlayerMiner.playerSucceessfulMineEvent -= PlaySuccessfulMineSound;
        PlayerMiner.playerFailMineEvent -= PlayFailMineSound;

        // Lantern sound
        Lamp.turnOnLanternEvent -= PlayLanternDroneSound;
        Lamp.turnOffLanternEvent -= StopLanternDroneSound;
        Lamp.turnOnLanternDroneSoundEvent -= PlayTurnOnLanternDroneSound;
        Lamp.turnOffLanternDroneSoundEvent -= PlayTurnOffLanternDroneSound;
        Lamp.turnOnLanternEvent -= PlayTurnOnLanternSound;
        Lamp.turnOffLanternEvent -= PlayTurnOffLanternSound;
        LanternFuelGameObject.onLanternFuelRefill -= PlayRefillLanternSound;

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

    private void SetNormalWalkingSound()
    {
        walkingAudioSource.clip = normalWalkingSound;
        walkingAudioSource.Play();
    }

    private void SetGrassWalkingSound()
    {
        walkingAudioSource.clip = grassWalkingSound;
        walkingAudioSource.Play();
    }



    // ItemPickUp sound
    public void PlayItemPickUpSound()
    {
        itemPickUpAudioSource.Play();
    }


    // Attack sound
    public void PlayAttackSound()
    {
        attackAndMineAudioSource.pitch = Random.Range(0.8f, 1.3f);
        attackAndMineAudioSource.clip = attackAudioSound;
        attackAndMineAudioSource.Play();
    }
    public void PlayMissAttackSound()
    {
        attackAndMineAudioSource.clip = missAttackAudioSound;
        attackAndMineAudioSource.pitch = Random.Range(0.8f, 1.3f);
        attackAndMineAudioSource.Play();
    }

    // ReceiveDamage sound
    public void PlayReceiveDamageSound()
    {
        receiveDamageAudioSource.pitch = Random.Range(1.8f, 2f);
        receiveDamageAudioSource.Play();
    }


    // Mine sound
    public void PlayMineSound()
    {
        attackAndMineAudioSource.clip = mineAudioSound;
        attackAndMineAudioSource.pitch = Random.Range(0.3f, 0.5f);
        attackAndMineAudioSource.Play();
    }

    public void PlayMineBreakSound()
    {
        attackAndMineAudioSource.clip = mineBreakAudioSound;
        attackAndMineAudioSource.pitch = Random.Range(0.8f, 1.2f);
        attackAndMineAudioSource.Play();
    }

    // MineBuildUp sound
    public void PlayMineBuildUpSound()
    {
        mineBuildUpAudioSource.clip = miningBuildUpSound;
        mineBuildUpAudioSource.pitch = 0.7f;
        mineBuildUpAudioSource.Play();
    }

    public void PlaySuccessfulMineSound()
    {
        mineBuildUpAudioSource.clip = successCriticalMiningSound;
        mineBuildUpAudioSource.pitch = 1f;
        mineBuildUpAudioSource.Play();
    }

    public void PlayFailMineSound()
    {
        mineBuildUpAudioSource.clip = failCriticalMiningSound;
        mineBuildUpAudioSource.pitch = 1.2f;
        mineBuildUpAudioSource.Play();
    }


    // Lantern sound
    public void PlayLanternDroneSound()
    {
        lanternDroneAudioSource.Play();
    }

    public void StopLanternDroneSound()
    {
        lanternDroneAudioSource.Stop();
    }

    private void PlayTurnOnLanternDroneSound()
    {
        lanternDroneOnOffAudioSource.clip = turnOnLanternDroneSound;
        lanternDroneOnOffAudioSource.Play();
    }

    private void PlayTurnOffLanternDroneSound()
    {
        lanternDroneOnOffAudioSource.clip = turnOffLanternDroneSound;
        lanternDroneOnOffAudioSource.Play();
    }

    private void PlayTurnOnLanternSound()
    {
        lanternOnOffAudioSource.clip = turnOnLanternSound;
        lanternOnOffAudioSource.pitch = 1f;
        lanternOnOffAudioSource.volume = 1f;
        lanternOnOffAudioSource.Play();
    }

    private void PlayTurnOffLanternSound()
    {
        lanternOnOffAudioSource.clip = turnOffLanternSound;
        lanternOnOffAudioSource.pitch = 1f;
        lanternOnOffAudioSource.volume = 1f;
        lanternOnOffAudioSource.Play();
    }

    private void PlayRefillLanternSound()
    {
        lanternOnOffAudioSource.clip = refillLanternSound;
        lanternOnOffAudioSource.pitch = Random.Range(0.8f, 1.2f);
        lanternOnOffAudioSource.volume = 0.25f;
        lanternOnOffAudioSource.Play();
    }

}