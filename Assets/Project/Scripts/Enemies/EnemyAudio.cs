using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceFootsteps;
    [SerializeField] AudioSource audioSourceEnvironment;
    [SerializeField] AudioSource audioSourceCries;
    [SerializeField] AudioClip banishAudioClip;
    [SerializeField] AudioClip receiveDamageAudioClip;
    [SerializeField] AudioClip deathAudioClip;
    [SerializeField] AudioClip attackAudioClip;


    public void PlayFootstepsAudio()
    {
        audioSourceFootsteps.Play();
    }

    public void StopFootstepsAudio()
    {
        audioSourceFootsteps.Stop();
    }

    public void PlayDashAudio()
    {
        audioSourceEnvironment.Play();
    }


    public void PlayBanishAudio()
    {
        audioSourceCries.clip = banishAudioClip;
        audioSourceCries.pitch = Random.Range(0.8f, 1.3f);
        audioSourceCries.Play();
    }

    public void PlayReceiveDamageAudio()
    {
        audioSourceCries.clip = receiveDamageAudioClip;
        audioSourceCries.pitch = Random.Range(0.8f, 1.3f);
        audioSourceCries.Play();
    }

    public void PlayDeathAudio()
    {
        audioSourceCries.clip = deathAudioClip;
        audioSourceCries.pitch = Random.Range(0.8f, 1.3f);
        audioSourceCries.Play();
    }

    public void PlayAttackAudio()
    {
        audioSourceCries.clip = attackAudioClip;
        audioSourceCries.pitch = Random.Range(0.8f, 1.3f);
        audioSourceCries.Play();
    }

}
