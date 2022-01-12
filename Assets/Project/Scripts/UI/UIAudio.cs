using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    [SerializeField] AudioSource inventoryButtonAudioSource;


    //[SerializeField] AudioClip attackAudioSound;




    private void OnEnable()
    {
        Inventory.OnItemMove += PlayItemMoveSound;
        InteractStation.OnInteractOpen += PlayInteractStationOpenSound;
        InteractStation.OnInteractClose += PlayInteractStationCloseSound;
    }


    private void OnDisable()
    {
        Inventory.OnItemMove -= PlayItemMoveSound;
        InteractStation.OnInteractOpen -= PlayInteractStationOpenSound;
        InteractStation.OnInteractClose -= PlayInteractStationCloseSound;
    }


    private void PlayItemMoveSound()
    {
        inventoryButtonAudioSource.pitch = Random.Range(0.8f, 1.2f);
        inventoryButtonAudioSource.Play();
    }

    private void PlayInteractStationOpenSound()
    {
        inventoryButtonAudioSource.pitch = 1.7f;
        inventoryButtonAudioSource.Play();
    }

    private void PlayInteractStationCloseSound()
    {
        inventoryButtonAudioSource.pitch = 0.3f;
        inventoryButtonAudioSource.Play();
    }
}
