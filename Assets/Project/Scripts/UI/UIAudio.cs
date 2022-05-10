using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    [SerializeField] AudioSource inventoryAudioSource;
    [SerializeField] AudioClip inventorySlotClickAudioClip;
    [SerializeField] AudioClip openInventoryClickAudioClip;
    [SerializeField] AudioClip closeInventoryClickAudioClip;

    [SerializeField] AudioSource upgradeAndCraftAudioSource;
    [SerializeField] AudioSource buttonHoverAudioSource;
    [SerializeField] AudioClip upgradeAudioClip;
    [SerializeField] AudioClip craftAudioClip;
    [SerializeField] AudioClip failCraftAudioClip;
    [SerializeField] AudioClip nextMessageAudioClip;

    bool canPlaybuttonHoverSound = true;
    float buttonHoverSoundCooldown = 0.1f;

    private void OnEnable()
    {
        Inventory.OnItemMove += PlayItemMoveSound;
        Inventory.OnItemMoveFail += PlayItemMoveFailSound;
        InteractStation.OnInteractOpen += PlayInteractStationOpenSound;
        InteractStation.OnInteractClose += PlayInteractStationCloseSound;

        Almanac.OnAlmanacMenuEnter += PlayInteractStationOpenSound;
        Almanac.OnAlmanacMenuExit += PlayInteractStationCloseSound;

        HoverButton.OnHover += PlayButtonHoverSound;

        UpgradeMenuCanvas.OnSubmenuEnter += PlayMenuButtonClickSound;
        UpgradesSystem.OnUpgrade += PlayUpgradeSound;
        UpgradesSystem.OnUpgradeFail += PlayFailCraftingSound;

        CraftingSystem.OnCrafting += PlayCraftingSound;
        CraftingSystem.OnCraftingFail += PlayFailCraftingSound;

        ChatBox.OnChatNextInput += PlayNextMessageSound;
    }


    private void OnDisable()
    {
        Inventory.OnItemMove -= PlayItemMoveSound;
        Inventory.OnItemMoveFail -= PlayItemMoveFailSound;
        InteractStation.OnInteractOpen -= PlayInteractStationOpenSound;
        InteractStation.OnInteractClose -= PlayInteractStationCloseSound;

        Almanac.OnAlmanacMenuEnter -= PlayInteractStationOpenSound;
        Almanac.OnAlmanacMenuExit -= PlayInteractStationCloseSound;

        HoverButton.OnHover -= PlayButtonHoverSound;

        UpgradeMenuCanvas.OnSubmenuEnter -= PlayMenuButtonClickSound;
        UpgradesSystem.OnUpgrade -= PlayUpgradeSound;
        UpgradesSystem.OnUpgradeFail -= PlayFailCraftingSound;

        CraftingSystem.OnCrafting -= PlayCraftingSound;
        CraftingSystem.OnCraftingFail -= PlayFailCraftingSound;

        ChatBox.OnChatNextInput -= PlayNextMessageSound;
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

    private void PlayUpgradeSound()
    {
        //if (upgradeAndCraftAudioSource.isPlaying) return;

        upgradeAndCraftAudioSource.volume = 0.5f;
        upgradeAndCraftAudioSource.clip = upgradeAudioClip;
        upgradeAndCraftAudioSource.pitch = 1f;
        upgradeAndCraftAudioSource.Play();
    }

    private void PlayCraftingSound()
    {
        //if (upgradeAndCraftAudioSource.isPlaying) return;

        upgradeAndCraftAudioSource.volume = 1f;
        upgradeAndCraftAudioSource.clip = craftAudioClip;
        upgradeAndCraftAudioSource.pitch = 0.7f;
        upgradeAndCraftAudioSource.Play();
    }

    private void PlayFailCraftingSound()
    {
        //if (upgradeAndCraftAudioSource.isPlaying) return;

        upgradeAndCraftAudioSource.volume = 0.3f;
        upgradeAndCraftAudioSource.clip = failCraftAudioClip;
        upgradeAndCraftAudioSource.pitch = 1f;
        upgradeAndCraftAudioSource.Play();
    }

    private void PlayButtonHoverSound()
    {
        if (!canPlaybuttonHoverSound) return;

        StartCoroutine(ButtonHoverSoundCooldown());

        buttonHoverAudioSource.volume = 0.1f;
        buttonHoverAudioSource.pitch = Random.Range(1.3f, 1.4f);
        buttonHoverAudioSource.Play();
    }

    private void PlayMenuButtonClickSound()
    {
        upgradeAndCraftAudioSource.volume = 0.2f;
        upgradeAndCraftAudioSource.clip = openInventoryClickAudioClip;
        upgradeAndCraftAudioSource.pitch = 1f;
        upgradeAndCraftAudioSource.Play();
    }



    private void PlayNextMessageSound()
    {
        if (upgradeAndCraftAudioSource.isPlaying) return;

        upgradeAndCraftAudioSource.volume = 0.05f;
        upgradeAndCraftAudioSource.clip = nextMessageAudioClip;
        upgradeAndCraftAudioSource.pitch = 1.0f;
        upgradeAndCraftAudioSource.Play();
    }

    IEnumerator ButtonHoverSoundCooldown()
    {
        canPlaybuttonHoverSound = false;
        yield return new WaitForSecondsRealtime(buttonHoverSoundCooldown);
        canPlaybuttonHoverSound = true;
    }
    
}
