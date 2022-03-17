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

    private void OnEnable()
    {
        OnItemMove += SetInventroyMenuSelectedSlotIndex;
        CraftingSystem.OnCrafting += SetInventroyMenuSelectedSlotIndex;
    }

    private void OnDisable()
    {
        OnItemMove -= SetInventroyMenuSelectedSlotIndex;
        CraftingSystem.OnCrafting -= SetInventroyMenuSelectedSlotIndex;
    }
    private void Start()
    {
        StartCoroutine(KEKWAIT());
    }

    IEnumerator KEKWAIT()
    {
        yield return new WaitForSeconds(2f);
        playerInventoryData.SaveInventoryItems(inventory);
    }

    public override void InitInventory()
    {
        base.InitInventory();
        playerInventoryData.LoadInventoryItems(this, transform);

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
        if (inventory[indexOfSelectedInventorySlot].itemInStack.itemType == ItemType.CONSUMIBLE)
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


}
