using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    // Private Attributes
    private Vector2 spawnPosition;
    private Animator animatior;
    private bool playerOnTrigger = false;
    private bool canvasIsActive;

    // Public Attributes
    public string teleportName;
    public Vector3 teleportTransformPosition;
    public bool activated = false;
    public GameObject[] teleporterLights;
    public GameObject canvasTeleportSelection;

    private void Start()
    {
        teleportTransformPosition = GetComponent<Transform>().position;
        spawnPosition = transform.position;
        animatior = GetComponent<Animator>();
        canvasIsActive = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerOnTrigger)
        {
            //Do the activate teleport animation and stay teleport
            if (!activated)
            {
                animatior.SetBool("isActivated", true);
                activated = true;
            }
            else
            {
                if (canvasIsActive)
                {
                    canvasTeleportSelection.SetActive(false);
                    canvasIsActive = false;
                }
                else
                {
                    canvasTeleportSelection.SetActive(true);
                    canvasIsActive = true;
                }
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

}