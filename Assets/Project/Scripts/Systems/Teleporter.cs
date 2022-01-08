using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    // Private Attributes
    private Vector2 spawnPosition;
    private Animator animatior;
    private bool playerOnTrigger = false;

    // Public Attributes
    public string teleportName;
    public Vector3 teleportTransformPosition;
    public bool activated = false;
    public GameObject[] teleporterLights;
    public GameObject canvasTeleportSelection;

    // Events
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
        if (PlayerInputs.instance.PlayerPressedInteractButton() && playerOnTrigger)
        {
            //Do the activate teleport animation and stay teleport
            if (activated)
            {
                if (canvasTeleportSelection.activeInHierarchy)
                {
                    PlayerInputs.instance.canMove = true;
                    PauseMenu.gameIsPaused = false;

                    canvasTeleportSelection.SetActive(false);
                }
                else
                {
                    PlayerInputs.instance.canMove = false;
                    PauseMenu.gameIsPaused = true;

                    canvasTeleportSelection.SetActive(true);
                    OnActivation(teleportName);
                }
            }
            else
            {
                PlayerInputs.instance.canMove = false;

                animatior.SetBool("isActivated", true);
                OnActivation(teleportName);
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerOnTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerOnTrigger = false;
    }


    public void SetTeleporterActive()
    {
        activated = true;

        canvasTeleportSelection.SetActive(true);

        if (OnActivation != null)
            OnActivation(teleportName);
    }
}