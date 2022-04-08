using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Teleporter : InteractStation
{
    // Private Attributes
    private Vector2 spawnPosition;
    private Animator animator;
    private string[] messagesToShow = { "Press E to interact", "No dark essence found", "Dark essence consumed" };

    // Public Attributes
    //Station
    public PopUp popUp;
    public GameObject canvasTeleportSelection;
    public GameObject hudGameObject;
    public TextMeshProUGUI mssgText;
    public Inventory inventory;

    //Teleport
    public Item darkEssence;
    public string teleportName;
    public Vector3 teleportTransformPosition;
    public bool activated = false;
    public GameObject teleportSprite;
    public GameObject teleportLight;

    [SerializeField] AudioSource teleportAudioSource;

    //Events / Actions
    public delegate void TeleportActivation(string teleportName);
    public static event TeleportActivation OnActivation;

    public delegate void TeleportInteraction(string teleportName);
    public static event TeleportInteraction OnInteraction;


    public delegate void TeleportMenuAction();
    public static event TeleportMenuAction OnMenuEnter;
    public static event TeleportMenuAction OnMenuExit;

    void Awake()
    {
        SaveSystem.teleporters.Add(this);
        teleportTransformPosition = GetComponent<Transform>().position;
        teleportTransformPosition.y -= 1.3f;
        spawnPosition = transform.position;
        animator = GetComponent<Animator>();
        teleportSprite.SetActive(true);
        teleportLight.SetActive(false);
    }

    private void Update()
    {
        if (playerInsideTriggerArea)
        {
            if (OnInteraction != null)
                OnInteraction(teleportName);

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
        if (!activated)
        {
            popUp.ShowInteraction();
        }

        popUp.ShowMessage();
    }

    // Interactive pop up disappears
    private void PopUpDisappears()
    {
        popUp.HideAll();
        mssgText.text = messagesToShow[0];
    }

    public override void StationFunction()
    {
        if (!activated && inventory.InventoryContainsItem(darkEssence))
        {
            activated = true;
            popUp.HideAll();
            teleportAudioSource.Play();

            inventory.SubstractItemFromInventory(darkEssence);
            popUp.GetComponent<PopUp>().ShowMessage();
            mssgText.text = messagesToShow[2];

            PlayerInputs.instance.canMove = false;
            animator.SetBool("isActivated", true);
            teleportLight.SetActive(true);
            //SaveSystem.SaveTeleporters();
        }
        else if (!activated && !inventory.InventoryContainsItem(darkEssence))
        {
            popUp.GetComponent<PopUp>().ShowMessage();
            mssgText.text = messagesToShow[1];

            InvokeOnNotEnoughMaterials();
        }
        else
        {
            if (!canvasTeleportSelection.activeInHierarchy)
            {
                hudGameObject.SetActive(false);
                canvasTeleportSelection.SetActive(true);
                PauseMenu.gameIsPaused = true;

                if (OnMenuEnter != null) 
                    OnMenuEnter();
            }
            else
            {
                hudGameObject.SetActive(true);
                canvasTeleportSelection.SetActive(false);
                PauseMenu.gameIsPaused = false;

                if (OnMenuExit != null) 
                    OnMenuExit();
            }
        }
    }

    public void SetTeleporterActive()
    {
        activated = true;
        PlayerInputs.instance.canMove = true;
        popUp.GetComponent<PopUp>().HideMessage();
        mssgText.text = messagesToShow[0];

        if (OnActivation != null)
            OnActivation(teleportName);
    }

    private void DesactivateSprite()
    {
        teleportSprite.SetActive(false);
    }
}
