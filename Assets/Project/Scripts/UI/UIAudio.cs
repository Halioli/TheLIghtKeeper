using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    [SerializeField] AudioSource inventoryAudioSource;
    [SerializeField] AudioClip inventorySlotClickAudioClip;
    [SerializeField] AudioClip openInventoryClickAudioClip;
    [SerializeField] AudioClip closeInventoryClickAudioClip;

    [SerializeField] AudioSource upgardeAndCraftAudioSource;
    [SerializeField] AudioSource buttonHoverAudioSource;
    [SerializeField] AudioClip upgradeAudioClip;
    [SerializeField] AudioClip craftAudioClip;
    [SerializeField] AudioClip failCraftAudioClip;


    bool canPlaybuttonHoverSound = true;
    float buttonHoverSoundCooldown = 0.1f;

    private void OnEnable()
    {
        Inventory.OnItemMove += PlayItemMoveSound;
        Inventory.OnItemMoveFail += PlayItemMoveFailSound;
        InteractStation.OnInteractOpen += PlayInteractStationOpenSound;
        InteractStation.OnInteractClose += PlayInteractStationCloseSound;

        HoverButton.OnHover += PlayButtonHoverSound;

        UpgradesSystem.OnUpgrade += PlayUpgardeSound;
        UpgradesSystem.OnUpgradeFail += PlayFailCraftingSound;

        CraftingSystem.OnCrafting += PlayCraftingSound;
        CraftingSystem.OnCraftingFail += PlayFailCraftingSound;
    }


    private void OnDisable()
    {
        Inventory.OnItemMove -= PlayItemMoveSound;
        Inventory.OnItemMoveFail -= PlayItemMoveFailSound;
        InteractStation.OnInteractOpen -= PlayInteractStationOpenSound;
        InteractStation.OnInteractClose -= PlayInteractStationCloseSound;

        HoverButton.OnHover -= PlayButtonHoverSound;

        UpgradesSystem.OnUpgrade -= PlayUpgardeSound;
        UpgradesSystem.OnUpgradeFail -= PlayFailCraftingSound;

        CraftingSystem.OnCrafting -= PlayCraftingSound;
        CraftingSystem.OnCraftingFail -= PlayFailCraftingSound;
    }


    private void PlayItemMoveSound()
    {
        inventoryAudioSource.clip = openInventoryClickAudioClip;
        inventoryAudioSource.pitch = Random.Range(1.3f, 1.4f);
        inventoryAudioSource.Play();
    }

    private void PlayItemMoveFailSound()
    {
        inventoryAudioSource.clip = closeInventoryClickAudioClip;
        inventoryAudioSource.pitch = Random.Range(0.3f, 0.4f);
        inventoryAudioSource.Play();
    }

    private void PlayInteractStationOpenSound()
    {
        inventoryAudioSource.clip = openInventoryClickAudioClip;
        inventoryAudioSource.pitch = 1f;
        inventoryAudioSource.Play();
    }

    private void PlayInteractStationCloseSound()
    {
        inventoryAudioSource.clip = closeInventoryClickAudioClip;
        inventoryAudioSource.pitch = 0.7f;
        inventoryAudioSource.Play();
    }

    private void PlayUpgardeSound()
    {
        if (upgardeAndCraftAudioSource.isPlaying) return;

        upgardeAndCraftAudioSource.volume = 0.5f;
        upgardeAndCraftAudioSource.clip = upgradeAudioClip;
        upgardeAndCraftAudioSource.pitch = 1f;
        upgardeAndCraftAudioSource.Play();
    }

    private void PlayCraftingSound()
    {
        if (upgardeAndCraftAudioSource.isPlaying) return;

        upgardeAndCraftAudioSource.volume = 1f;
        upgardeAndCraftAudioSource.clip = craftAudioClip;
        upgardeAndCraftAudioSource.pitch = 0.7f;
        upgardeAndCraftAudioSource.Play();
    }

    private void PlayFailCraftingSound()
    {
        if (upgardeAndCraftAudioSource.isPlaying) return;

        upgardeAndCraftAudioSource.volume = 0.3f;
        upgardeAndCraftAudioSource.clip = failCraftAudioClip;
        upgardeAndCraftAudioSource.pitch = 1f;
        upgardeAndCraftAudioSource.Play();
    }

    private void PlayButtonHoverSound()
    {
        if (!canPlaybuttonHoverSound) return;

        StartCoroutine(ButtonHoverSoundCooldown());

        buttonHoverAudioSource.volume = 0.1f;
        buttonHoverAudioSource.pitch = Random.Range(1.3f, 1.4f);
        buttonHoverAudioSource.Play();
    }

    IEnumerator ButtonHoverSoundCooldown()
    {
        canPlaybuttonHoverSound = false;
        yield return new WaitForSecondsRealtime(buttonHoverSoundCooldown);
        canPlaybuttonHoverSound = true;
    }
    
}
