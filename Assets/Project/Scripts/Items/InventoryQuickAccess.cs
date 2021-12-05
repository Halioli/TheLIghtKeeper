using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryQuickAccess : MonoBehaviour
{
    private Inventory playerInventory;
    private PlayerInputs playerInputs;
    private int mouseScrollDirection;
    
    private bool printedAlready = true;

    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        playerInputs = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputs>();
        mouseScrollDirection = 0;
    }

    void Update()
    {
        mouseScrollDirection = (int)playerInputs.PlayerMouseScroll().y;

        if (mouseScrollDirection > 0){
            playerInventory.CycleRightSelectedItemIndex();
            printedAlready = false;
        }
        else if (mouseScrollDirection < 0)
        {
            playerInventory.CycleLeftSelectedItemIndex();
            printedAlready = false;
        }
        if (!printedAlready)
        {
            Debug.Log(playerInventory.indexOfSelectedInventorySlot);
            printedAlready = true;
        }

        if (playerInputs.PlayerPressedUseButton())
        {
            playerInventory.UseSelectedConsumibleItem();
        }
    }
}
