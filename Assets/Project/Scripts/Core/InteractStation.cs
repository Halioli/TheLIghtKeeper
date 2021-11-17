using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractStation : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            StationFunction();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            UpgradeFunction();
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
