using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceFootsteps;
    [SerializeField] AudioSource audioSourceEnvironment;
    [SerializeField] AudioSource audioSourceCries;
    [SerializeField] AudioSource audioSourceCries2;
    [SerializeField] AudioClip scaredAudioClip;
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


    public void PlayScaredAudio()
    {
        audioSourceCries2.clip = scaredAudioClip;
        audioSourceCries2.pitch = Random.Range(0.8f, 1.3f);
        audioSourceCries2.Play();
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
