using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternAttackSound : MonoBehaviour
{
    [SerializeField] AudioSource droneAudioSource;
    [SerializeField] AudioSource startEndAudioSource;


    private void OnEnable()
    {
        LanternAttack.OnLanternAttackStart += PlayDroneSound;
        LanternAttack.OnLanternAttackStart += PlayStartSound;

        LanternAttack.OnLanternAttackEnd += StopDroneSound;
        LanternAttack.OnLanternAttackEnd += PlayEndSound;
    }

    private void OnDisable()
    {
        LanternAttack.OnLanternAttackStart -= PlayDroneSound;
        LanternAttack.OnLanternAttackStart -= PlayStartSound;

        LanternAttack.OnLanternAttackEnd -= StopDroneSound;
        LanternAttack.OnLanternAttackEnd -= PlayEndSound;
    }



    private void PlayDroneSound()
    {
        droneAudioSource.Play();
    }

    private void StopDroneSound()
    {
        droneAudioSource.Stop();
    }

    private void PlayStartSound()
    {
        startEndAudioSource.pitch = 1f;
        startEndAudioSource.Play();
    }

    private void PlayEndSound()
    {
        startEndAudioSource.pitch = 0.5f;
        startEndAudioSource.Play();
    }



}
