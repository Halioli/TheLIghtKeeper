using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Private Attributes
    private float mouseScrollDirection = 0f;

    // Public Attributes
    public HotbarInventory hotbarInventory { get; private set; }


    // Events
    public delegate void PlayPlayerSound();
    public static event PlayPlayerSound playerPicksUpItemEvent;

    public delegate void InventoryAction();
    public static event InventoryAction OnInventoryOpen;
    public static event InventoryAction OnInventoryClose;


    private void Start()
    {
        hotbarInventory = GetComponentInChildren<HotbarInventory>();
    }

    void Update()
    {
        DoInputsHotbarInventory();
    }


    private void OnEnable()
    {
        InventoryUpgrade.OnInventoryUpgrade += UpgradeInventory;
    }

    private void OnDisable()
    {
        InventoryUpgrade.OnInventoryUpgrade -= UpgradeInventory;
    }

    private void DoInputsHotbarInventory()
    {
        mouseScrollDirection = PlayerInputs.instance.PlayerMouseScroll().y;
        if (mouseScrollDirection < 0f)
        {
            hotbarInventory.CycleRightSelectedItemIndex();  
        }
        else if (mouseScrollDirection > 0f)
        {
            hotbarInventory.CycleLeftSelectedItemIndex();
        }

        if (PlayerInputs.instance.PlayerPressedUseButton())
        {
            hotbarInventory.UseSelectedConsumibleItem();
        }
    }


    private void UpgradeInventory()
    {
        hotbarInventory.UpgradeInventory();
    }
}
