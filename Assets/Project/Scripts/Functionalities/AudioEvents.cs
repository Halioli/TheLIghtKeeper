using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvents : MonoBehaviour
{
    // Public Attributes
    public AudioSource audioSource;
    public AudioClip miningOreSound;
    public AudioClip breaksOreSound;
    public AudioClip itemIsPickedUpSound;



    private void OnEnable()
    {
        PlayerMiner.playerMinesOreEvent += PlayMiningOreSound;
        PlayerMiner.playerBreaksOreEvent += PlayBreaksOreSound;
        PlayerInventory.playerPicksUpItemEvent += PlayPicksUpItemSound;
    }

    private void OnDisable()
    {
        PlayerMiner.playerMinesOreEvent -= PlayMiningOreSound;
        PlayerMiner.playerBreaksOreEvent -= PlayBreaksOreSound;
        PlayerInventory.playerPicksUpItemEvent -= PlayPicksUpItemSound;
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
