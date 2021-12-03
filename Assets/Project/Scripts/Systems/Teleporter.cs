using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public string teleportName;
    public Vector3 teleportTransformPosition;

    private bool activated = false;
    private Vector2 spawnPosition;
    private Animator animatior;
    private bool playerOnTrigger = false;
    public GameObject[] teleporterLights;

    private void Start()
    {
        teleportTransformPosition = GetComponent<Transform>().position;
        spawnPosition = transform.position;
        animatior = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerOnTrigger)
        {
            //Do the activate teleport animation and stay teleport
            if (!activated)
            {
                Debug.Log("AAAAAAA");
                animatior.SetBool("isActivated", true);
                activated = true;
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