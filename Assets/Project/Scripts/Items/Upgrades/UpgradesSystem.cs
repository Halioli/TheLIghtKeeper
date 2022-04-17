using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradesSystem : MonoBehaviour
{
    private Dictionary<Item, int> playerInventoryItems;
    Inventory playerInventory;
    [SerializeField] public List<UpgradeBranch> upgradeBranches;

    // Events
    public delegate void UpgardeAction();
    public static event UpgardeAction OnUpgrade;
    public static event UpgardeAction OnUpgradeFail;

    private void Awake()
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

    public bool UpgradeBranchIsSelected(int index)
    {
        if (upgradeBranches[index].IsCompleted()) return false;

        UpdatePlayerInventoryData();

        if (PlayerHasEnoughItemsToUpgrade(upgradeBranches[index].GetCurrentUpgrade()))
        {
            RemoveUpgradeRequiredItems(upgradeBranches[index].GetCurrentUpgrade());

            upgradeBranches[index].Upgrade();

            if (OnUpgrade != null) OnUpgrade();

            return true;
        }


        if (OnUpgradeFail != null) OnUpgradeFail();
        
        return false;
    }

    public void AlwaysCompleteUpgradeBranchIsSelected(int index)
    {
        if (upgradeBranches[index].IsCompleted()) return;


        upgradeBranches[index].Upgrade();
        //if (OnUpgrade != null) OnUpgrade();
    }

    public void UpdatePlayerInventoryData()
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

    public bool PlayerHasEnoughItemsToUpgrade(Upgrade upgrade)
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

    public void DoOnUpgardeFail()
    {
        if (OnUpgradeFail != null) OnUpgradeFail();
    }


    public bool UpgradeBranchIsCompleted(int upgradeBranchIndex)
    {
        return upgradeBranches[upgradeBranchIndex].IsCompleted();
    }


}
