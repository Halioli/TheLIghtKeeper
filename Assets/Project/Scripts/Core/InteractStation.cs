using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractStation : MonoBehaviour
{
    public BoxCollider2D triggerArea;

    protected bool playerInsideTriggerArea;

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
    }

    virtual public void StationFunction()
    {
        //Code from child
    }
}
