using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarInventory : Inventory
{
    [SerializeField] InventoryMenu hotbarInventoryMenu;
    [SerializeField] RectTransform hotbarRectTransform;

    private int currentUpgrade = -1; // -1 = not upgraded
    private int[] extraSlotsOnUpgrade = { 1, 1, 2 };
    private float[] hotbarWidthOnUpgrade = { 674f, 798f, 1042f };

    [SerializeField] InventoryData playerInventoryData;


    public delegate void HotbarInventoryUse();
    public static event HotbarInventoryUse OnInventoryItemDrop;




    private void OnEnable()
    {
        OnItemMove += SetInventroyMenuSelectedSlotIndex;
        CraftingSystem.OnCrafting += SetInventroyMenuSelectedSlotIndex;
        BrokenFurnace.OnTutorialFinish += SaveInventory;
    }

    private void OnDisable()
    {
        OnItemMove -= SetInventroyMenuSelectedSlotIndex;
        CraftingSystem.OnCrafting -= SetInventroyMenuSelectedSlotIndex;
        BrokenFurnace.OnTutorialFinish -= SaveInventory;
    }

    public void SaveInventory()
    {
        playerInventoryData.SaveInventoryItems(inventory);
    }

    public override void InitInventory()
    {
        base.InitInventory();
        playerInventoryData.LoadInventoryItems(this);

        gotChanged = true;
        inventoryIsEmpty = false;
    }


    public void CycleLeftSelectedItemIndex()
    {
        --indexOfSelectedInventorySlot;
        indexOfSelectedInventorySlot = indexOfSelectedInventorySlot < 0 ? numberOfInventorySlots - 1 : indexOfSelectedInventorySlot;
        
        SetInventroyMenuSelectedSlotIndex();
    }

    public void CycleRightSelectedItemIndex()
    {
        indexOfSelectedInventorySlot = (indexOfSelectedInventorySlot + 1) % numberOfInventorySlots;

        SetInventroyMenuSelectedSlotIndex();
    }

    public void UseSelectedConsumibleItem()
    {
        Debug.Log(PlayerInputs.instance.isInGameMenu);

        if (inventory[indexOfSelectedInventorySlot].itemInStack.itemType == ItemType.CONSUMIBLE && !PlayerInputs.instance.isInGameMenu)
        {
            inventory[indexOfSelectedInventorySlot].itemInStack.prefab.GetComponent<ItemGameObject>().DoFunctionality();
            //GameObject consumibleItem = Instantiate(inventory[indexOfSelectedInventorySlot].itemInStack.prefab, transform.position, Quaternion.identity);
            //consumibleItem.GetComponent<ItemGameObject>().DoFunctionality();

            SubstractItemFromInventorySlot(indexOfSelectedInventorySlot);
            SetInventroyMenuSelectedSlotIndex();
        }
    }


    private void SetInventroyMenuSelectedSlotIndex()
    {
        hotbarInventoryMenu.SetSelectedInventorySlotIndex(indexOfSelectedInventorySlot);
    }


    public void DropSelectedItem()
    {
        if (!inventory[indexOfSelectedInventorySlot].StackIsEmpty())
        {
            ItemGameObject itemGameObject = Instantiate(inventory[indexOfSelectedInventorySlot].itemInStack.prefab, transform).GetComponent<ItemGameObject>();

            itemGameObject.DropsRandom(!itemGameObject.item.isSpecial, 1.5f, 20f);
            itemGameObject.MakeNotPickupableForDuration(2f);

            SubstractItemFromInventorySlot(indexOfSelectedInventorySlot);
            SetInventroyMenuSelectedSlotIndex();

            if (OnInventoryItemDrop != null) OnInventoryItemDrop();
        }
    }



    // Modifier Methods
    public void UpgradeInventory()
    {
        if (numberOfInventorySlots < maxNumberOfSlots)
        {
            numberOfInventorySlots += extraSlotsOnUpgrade[++currentUpgrade];
            for (int i = 0; i < extraSlotsOnUpgrade[currentUpgrade]; ++i)
            {
                inventory.Add(Instantiate(emptyStack, transform));
            }

            hotbarRectTransform.sizeDelta = new Vector2(hotbarWidthOnUpgrade[currentUpgrade], hotbarRectTransform.sizeDelta.y);
            gotChanged = true;
        }
    }



    // Other

    public Vector2 GetStackTransformPosition(int stackIndex)
    {
        return hotbarInventoryMenu.GetItemCellTransformPosition(stackIndex);
    }


}
