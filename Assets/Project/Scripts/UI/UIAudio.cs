using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    [SerializeField] AudioSource inventoryAudioSource;
    [SerializeField] AudioClip inventorySlotClickAudioClip;
    [SerializeField] AudioClip openCloseInventoryClickAudioClip;

    [SerializeField] AudioSource upgardeAndCraftAudioSource;
    [SerializeField] AudioClip buttonHoverAucioClip;
    [SerializeField] AudioClip upgradeAudioClip;
    [SerializeField] AudioClip craftAudioClip;
    [SerializeField] AudioClip failCraftAudioClip;


    private void OnEnable()
    {
        Inventory.OnItemMove += PlayItemMoveSound;
        PlayerInventory.OnInventoryOpen += PlayInteractStationOpenSound;
        PlayerInventory.OnInventoryClose += PlayInteractStationCloseSound;

        HoverButton.OnHover += PlayButtonHoverSound;

        UpgradesSystem.OnUpgrade += PlayUpgardeSound;
        UpgradesSystem.OnUpgradeFail += PlayFailCraftingSound;

        CraftingSystem.OnCrafting += PlayCraftingSound;
        CraftingSystem.OnCraftingFail += PlayFailCraftingSound;
    }


    private void OnDisable()
    {
        Inventory.OnItemMove -= PlayItemMoveSound;
        PlayerInventory.OnInventoryOpen -= PlayInteractStationOpenSound;
        PlayerInventory.OnInventoryClose -= PlayInteractStationCloseSound;

        HoverButton.OnHover += PlayButtonHoverSound;

        UpgradesSystem.OnUpgrade -= PlayUpgardeSound;
        UpgradesSystem.OnUpgradeFail -= PlayFailCraftingSound;

        CraftingSystem.OnCrafting -= PlayCraftingSound;
        CraftingSystem.OnCraftingFail -= PlayFailCraftingSound;
    }


    private void PlayItemMoveSound()
    {
        inventoryAudioSource.clip = inventorySlotClickAudioClip;
        inventoryAudioSource.pitch = Random.Range(0.8f, 1.2f);
        inventoryAudioSource.Play();
    }

    private void PlayInteractStationOpenSound()
    {
        inventoryAudioSource.clip = openCloseInventoryClickAudioClip;
        inventoryAudioSource.pitch = 1f;
        inventoryAudioSource.Play();
    }

    private void PlayInteractStationCloseSound()
    {
        inventoryAudioSource.clip = openCloseInventoryClickAudioClip;
        inventoryAudioSource.pitch = 1.5f;
        inventoryAudioSource.Play();
    }

    private void PlayUpgardeSound()
    {
        upgardeAndCraftAudioSource.volume = 0.5f;
        upgardeAndCraftAudioSource.clip = upgradeAudioClip;
        upgardeAndCraftAudioSource.pitch = 1f;
        upgardeAndCraftAudioSource.Play();
    }

    private void PlayCraftingSound()
    {
        upgardeAndCraftAudioSource.volume = 1f;
        upgardeAndCraftAudioSource.clip = craftAudioClip;
        upgardeAndCraftAudioSource.pitch = 0.7f;
        upgardeAndCraftAudioSource.Play();
    }

    private void PlayFailCraftingSound()
    {
        upgardeAndCraftAudioSource.volume = 0.75f;
        upgardeAndCraftAudioSource.clip = failCraftAudioClip;
        upgardeAndCraftAudioSource.pitch = 1f;
        upgardeAndCraftAudioSource.Play();
    }

    private void PlayButtonHoverSound()
    {
        upgardeAndCraftAudioSource.volume = 0.1f;
        upgardeAndCraftAudioSource.clip = buttonHoverAucioClip;
        upgardeAndCraftAudioSource.pitch = Random.Range(0.95f, 1.05f);
        upgardeAndCraftAudioSource.Play();
    }
    
}
