using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Teleporter : InteractStation
{
    // Private Attributes
    private Vector2 spawnPosition;
    private Animator animatior;
    private string[] messagesToShow = { "", "No dark essence found", "Dark essence consumed" };

    // Public Attributes
    //Station
    public GameObject interactText;
    public GameObject canvasTeleportSelection;
    public GameObject hudGameObject;
    public TextMeshProUGUI mssgText;

    //Teleport
    public Item darkEssence;
    public string teleportName;
    public Vector3 teleportTransformPosition;
    public bool activated = false;
    public GameObject[] teleporterLights;

    //Events / Actions
    public delegate void TeleportActivation(string teleportName);
    public static event TeleportActivation OnActivation;

    private void Start()
    {
        teleportTransformPosition = GetComponent<Transform>().position;
        teleportTransformPosition.y -= 1.3f;
        spawnPosition = transform.position;
        animatior = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerInsideTriggerArea)
        {
            GetInput();
            PopUpAppears();
        }
        else
        {
            PopUpDisappears();
        }
    }

    // Interactive pop up disappears
    private void PopUpAppears()
    {
        interactText.SetActive(true);
        OnActivation(teleportName);
    }

    // Interactive pop up disappears
    private void PopUpDisappears()
    {
        interactText.SetActive(false);
        mssgText.text = messagesToShow[0];
    }

    public override void StationFunction()
    {
        if (!activated && playerInventory.InventoryContainsItem(darkEssence))
        {
            playerInventory.SubstractItemToInventory(darkEssence);
            mssgText.text = messagesToShow[2];

            PlayerInputs.instance.canMove = false;
            animatior.SetBool("isActivated", true);
            OnActivation(teleportName);
        }
        else if (!activated && !playerInventory.InventoryContainsItem(darkEssence))
        {
            mssgText.text = messagesToShow[1];
        }
        else
        {
            if (!canvasTeleportSelection.activeInHierarchy)
            {
                OnActivation(teleportName);
                hudGameObject.SetActive(false);
                canvasTeleportSelection.SetActive(true);
                PauseMenu.gameIsPaused = true;
            }
            else
            {
                hudGameObject.SetActive(true);
                canvasTeleportSelection.SetActive(false);
                PauseMenu.gameIsPaused = false;
            }
        }
    }

    public void SetTeleporterActive()
    {
        activated = true;
        PlayerInputs.instance.canMove = true;
        mssgText.text = messagesToShow[0];

        if (OnActivation != null)
            OnActivation(teleportName);
    }
}