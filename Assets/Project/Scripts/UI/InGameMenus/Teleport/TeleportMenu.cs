using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeleportMenu : MonoBehaviour
{
    // Private Attributes

    // Public Attributes
    public Button[] teleportButtonsGameObjects;
    public Sprite[] currentTeleportSprites;
    public Image currentTeleportRuneImage;
    public TeleportSystem teleportSystem;
    public GameObject hudGameObject;

    void Start()
    {
        for (int i = 0; i < teleportSystem.teleports.Count; ++i)
        {
            if (!teleportSystem.teleports[i].activated)
            {
                teleportButtonsGameObjects[i].interactable = false;
            }
        }
    }

    private void OnEnable()
    {
        Teleporter.OnActivation += UpdateTeleportSelectionMenu;
        Teleporter.OnInteraction += UpdateTeleportSelectionMenu;

        TeleportButton.OnSelection += DeactivateSelf;
    }

    private void OnDisable()
    {
        Teleporter.OnActivation -= UpdateTeleportSelectionMenu;
        Teleporter.OnInteraction -= UpdateTeleportSelectionMenu;

        TeleportButton.OnSelection -= DeactivateSelf;
    }

    public void UpdateTeleportSelectionMenu(string currentTeleportInUse)
    {
        currentTeleportRuneImage.sprite = currentTeleportSprites[teleportSystem.currentTeleportInUse];

        for (int i = 0; i < teleportSystem.teleports.Count; ++i)
        {
            if (teleportSystem.teleports[i].activated)
            {
                teleportButtonsGameObjects[i].interactable = true;

                if (i == teleportSystem.currentTeleportInUse)
                {
                    teleportButtonsGameObjects[i].GetComponent<Button>().interactable = false;
                }
                else if (!teleportButtonsGameObjects[i].GetComponent<Button>().interactable)
                {
                    teleportButtonsGameObjects[i].GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    private void DeactivateSelf(int teleportIndex)
    {
        hudGameObject.SetActive(true);
        PauseMenu.gameIsPaused = false;

        gameObject.SetActive(false);
    }
}
