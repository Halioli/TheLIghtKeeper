using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradesSystem : MonoBehaviour
{
    private Dictionary<Item, int> playerInventoryItems;
    Inventory playerInventory;
    [SerializeField] public List<UpgradeBranch> upgradeBranches;


    private void Start()
    {
        playerInventoryItems = new Dictionary<Item, int>();

        for (int i = 0; i < upgradeBranches.Count; ++i)
        {
            upgradeBranches[i].Init(i);
        }
    }

    public void Init(Inventory playerInventory)
    {
        this.playerInventory = playerInventory;
    }


    public void UpgradeBranchIsSelected(int index)
    {
        UpdatePlayerInventoryData();
        if (PlayerHasEnoughItemsToUpgrade(upgradeBranches[index].GetCurrentUpgrade()))
        {
            RemoveUpgradeRequiredItems(upgradeBranches[index].GetCurrentUpgrade());

            upgradeBranches[index].Upgrade();
        }
    }



    private void UpdatePlayerInventoryData()
    {
        playerInventoryItems.Clear();

        foreach (ItemStack playerInventoryItemStack in playerInventory.inventory)
        {
            if (!playerInventoryItemStack.StackIsEmpty())
            {
                if (!playerInventoryItems.ContainsKey(playerInventoryItemStack.itemInStack))
                {
                    playerInventoryItems[playerInventoryItemStack.itemInStack] = 0;
                }
                playerInventoryItems[playerInventoryItemStack.itemInStack] += playerInventoryItemStack.amountInStack;
            }
        }

    }

    private bool PlayerHasEnoughItemsToUpgrade(Upgrade upgrade)
    {
        foreach (KeyValuePair<Item, int> requiredItem in upgrade.requiredItems)
        {
            if (!playerInventory.InventoryContainsItemAndAmount(requiredItem.Key, requiredItem.Value))
            {
                return false;
            }
        }
        return true;
    }

    private void RemoveUpgradeRequiredItems(Upgrade upgrade)
    {
        foreach (KeyValuePair<Item, int> requiredItem in upgrade.requiredItems)
        {
            playerInventory.SubstractNItemsFromInventory(requiredItem.Key, requiredItem.Value);
        }
    }

}
