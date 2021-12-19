using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractStation : PlayerInputs
{
    public BoxCollider2D triggerArea;

    protected bool playerInsideTriggerArea;
    protected Inventory playerInventory;

    private void Awake()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInsideTriggerArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInsideTriggerArea = false;
        }
    }

    public void GetInput()
    {
        if (PlayerPressedInteractButton())
        {
            Debug.Log("player pressed");
            StationFunction();
        }
    }

    virtual public void StationFunction()
    {
        //Code from child
    }

    virtual public void UpgradeFunction()
    {
        //Code from child
    }
}
