using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : InteractStation
{
    // Private Attributes
    private Vector2 spawnPosition;
    private Animator animatior;

    // Public Attributes
    //Station
    public GameObject interactText;
    public GameObject canvasTeleportSelection;
    public GameObject hudGameObject;

    //Teleport
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
            if (canvasTeleportSelection.activeInHierarchy)
            {
                hudGameObject.SetActive(true);
                canvasTeleportSelection.SetActive(false);
            }
        }
    }

    // Interactive pop up disappears
    private void PopUpAppears()
    {
        interactText.SetActive(true);
    }

    // Interactive pop up disappears
    private void PopUpDisappears()
    {
        interactText.SetActive(false);
    }

    public override void StationFunction()
    {
        if (!activated)
        {
            PlayerInputs.instance.canMove = false;
            animatior.SetBool("isActivated", true);
            OnActivation(teleportName);
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

        if (OnActivation != null)
            OnActivation(teleportName);
    }
}