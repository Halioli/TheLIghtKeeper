using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryQuickAccess : MonoBehaviour
{
    private Inventory playerInventory;
    private PlayerInputs playerInputs;
    private int mouseScrollDirection;

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
        }
        else if (mouseScrollDirection < 0)
        {
            playerInventory.CycleLeftSelectedItemIndex();
        }
        Debug.Log(playerInventory.indexOfSelectedInventorySlot);
        Debug.Log(playerInventory.inventory[playerInventory.indexOfSelectedInventorySlot].itemInStack.name);
    }
}
