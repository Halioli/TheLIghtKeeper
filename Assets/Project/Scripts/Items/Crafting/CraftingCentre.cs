using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingCentre : MonoBehaviour
{
    // Private Attributes
    public CraftingRecepiesCollection availableRecepies;
    private Inventory playerInventory;

    // Public Attributes
    public int currentLevel;
    public List<CraftingRecepiesCollection> listoOfRecepiesByLevel;


    private void Awake()
    {
        availableRecepies = new CraftingRecepiesCollection();

        currentLevel = 1;
    }

    private void Start()
    {
        availableRecepies.AddAllRecepiesFromOtherCollection(listoOfRecepiesByLevel[0]);
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
    }

    public void PlayerChoosesRecepie(int selectedRecepieIndex)
    {
        // Get player inventory
        // Show available recepies

        if (PlayerHasEnoughMaterials(selectedRecepieIndex))
        {
            Item resultingItem = availableRecepies.GetRecepieWithIndex(selectedRecepieIndex).resultingItem;
            int resultingAmount = availableRecepies.GetRecepieWithIndex(selectedRecepieIndex).resultingItemAmount;

            if (PlayerHasInventorySpace(resultingItem, resultingAmount))
            {
                Debug.Log("Has inventory space");
                // Player can craft!
                CraftSelectedRecepie(selectedRecepieIndex);
            }
        }

    }


    private bool PlayerHasEnoughMaterials(int selectedRecepieIndex)
    {
        // Define the recepie that the player wants to craft
        CraftingRecepie recepieToCraft = availableRecepies.GetRecepieWithIndex(selectedRecepieIndex);

        // Define the list of required items of said recepie
        List<ItemStack> listOfRequiredItems = recepieToCraft.GetRequiredItemStacks();

        // Iterate over all elements of said list
        Item itemToSearch;
        int amountOfItemToSearch;

        bool enoughMaterials = true;
        int i = 0;

        while (enoughMaterials && i < listOfRequiredItems.Count)
        {
            itemToSearch = listOfRequiredItems[i].GetItemInStack();
            amountOfItemToSearch = listOfRequiredItems[i].GetAmountInStack();
            // The player DOES NOT have that material and its required amount
            if (!playerInventory.InventoryContainsItemAndAmount(itemToSearch, amountOfItemToSearch))
            {
                enoughMaterials = false;
            }

            i++;
        }

        if (enoughMaterials)
        {
            // The player HAS enought materials to craft this recepie
        }
        else
        {
            // The player DOES NOT HAVE enought materials to craft this recepie
        }

        return enoughMaterials;
    }

    private bool PlayerHasInventorySpace(Item itemToAdd, int amountOfItemsToAdd)
    {
        bool hasInventorySpace = true;

        int inventorySlotIndex = 0;
        while (inventorySlotIndex != -1 && amountOfItemsToAdd > 0)
        {
            inventorySlotIndex = playerInventory.NextInventorySlotWithAvailableSpaceToAddItem(itemToAdd);

            if (inventorySlotIndex != -1)
            {
                int maxStackCapacity = playerInventory.inventory[inventorySlotIndex].GetItemStackSize();
                int currentStackCapacity = playerInventory.inventory[inventorySlotIndex].GetAmountInStack();

                amountOfItemsToAdd -= maxStackCapacity - currentStackCapacity;
            }
            
        }

        if (amountOfItemsToAdd > 0 && playerInventory.NextEmptyInventorySlot() == -1)
        {
            hasInventorySpace = false;
        }
        
        if (hasInventorySpace)
        {
            // The player HAS inventory space for recepie resulting item
        }
        else
        {
            // The player DOES NOT HAVE inventory space for recepie resulting item
        }

        return hasInventorySpace;
    }


    private void CraftSelectedRecepie(int selectedRecepieIndex)
    {
        // First remove player materials
        RemovePlayerRequiredItems(selectedRecepieIndex);

        // Second give player items
        GivePlayerResultingItems(selectedRecepieIndex);
    }
    
    private void RemovePlayerRequiredItems(int selectedRecepieIndex)
    {
        // Define the recepie that the player wants to craft
        CraftingRecepie recepieToCraft = availableRecepies.GetRecepieWithIndex(selectedRecepieIndex);

        // Define the list of required items of said recepie
        List<ItemStack> listOfRequiredItems = recepieToCraft.GetRequiredItemStacks();

        for (int i = 0; i < listOfRequiredItems.Count; i++)
        {
            for (int j = 0; j < listOfRequiredItems[i].amountInStack; j++)
            {
                playerInventory.SubstractItemToInventory(listOfRequiredItems[i].itemInStack);
            }
        }
    }

    private void GivePlayerResultingItems(int selectedRecepieIndex)
    {
        // Define the recepie that the player wants to craft
        CraftingRecepie recepieToCraft = availableRecepies.GetRecepieWithIndex(selectedRecepieIndex);

        // Define the resulting items of said recepie
        ItemStack resultingItems = recepieToCraft.GetResultingItemStack();

        for (int i = 0; i < resultingItems.amountInStack; i++)
        {
            playerInventory.AddItemToInventory(resultingItems.itemInStack);
        }
    }



    public void LevelUp()
    {
        currentLevel++;
        AddCurrentLevelRecepiesToAvailableRecepies();
    }

    private void AddCurrentLevelRecepiesToAvailableRecepies()
    {
        availableRecepies.AddAllRecepiesFromOtherCollection(listoOfRecepiesByLevel[currentLevel-1]);
    }

}
