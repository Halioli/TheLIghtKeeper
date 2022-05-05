using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    // Private Attributes
    private const int MAX_LEVEL = 5;
    private int currentLevel;

    private Inventory playerInventory;
    private Dictionary<Item, int> playerInventoryItems;
    private int numberOfEmptySlotsInPlayerInventory;

    private Vector2 droppedItemPosition;

    private bool canCraft = false;

    [SerializeField] Inventory storageStationInventory;

    // Public Attributes
    public List<RecepieCollection> recepiesLvl;
    public List<Recepie> availableRecepies;
    // public ParticleSystem[] craftingParticles;

    //Events

    public delegate void CraftAction();
    public static event CraftAction OnCrafting;
    public static event CraftAction OnCraftingFail;

    public delegate void CraftAction2(int receipeIndex);
    public static event CraftAction2 OnReceipeCraftingSuccess;

    public delegate void ItemCraftedAction(int itemID);
    public static event ItemCraftedAction OnItemCraft;

    public delegate void ItemSentToStorageAction();
    public static event ItemSentToStorageAction OnItemSentToStorage;


    void Awake()
    {
        currentLevel = 0;
        InitAllRecepies();

        availableRecepies = new List<Recepie>();
        AddAvailableRecepies();

        droppedItemPosition = new Vector2(transform.position.x, transform.position.y - 1f);

        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        playerInventoryItems = new Dictionary<Item, int>();


        //foreach (ParticleSystem particle in craftingParticles)
        //{
        //    particle.Stop();
        //}
    }


    private void OnEnable()
    {
        CraftableItemButton.OnClickedRecepieButton += RecepieWasSelected;
        CoreUpgrade.OnCoreUpgrade += LevelUp;
    }

    private void OnDisable()
    {
        CraftableItemButton.OnClickedRecepieButton -= RecepieWasSelected;
        CoreUpgrade.OnCoreUpgrade -= LevelUp;
    }

    private void InitAllRecepies()
    {
        foreach (RecepieCollection recepieCollection in recepiesLvl)
        {
            recepieCollection.InitRecepies();
        }
    }


    private void AddAvailableRecepies()
    {
        foreach (Recepie recepie in recepiesLvl[currentLevel].recepies)
        {
            availableRecepies.Add(recepie);
        }
    }
    
    public void LevelUp()
    {
        if (++currentLevel >= MAX_LEVEL) return;

        AddAvailableRecepies();
    }

    private void UpdatePlayerInventoryData()
    {
        playerInventoryItems.Clear();
        numberOfEmptySlotsInPlayerInventory = 0;

        foreach (ItemStack playerInventoryItemStack in playerInventory.inventory)
        {
            if (playerInventoryItemStack.StackIsEmpty())
            {
                ++numberOfEmptySlotsInPlayerInventory;
            }
            else
            {
                if (!playerInventoryItems.ContainsKey(playerInventoryItemStack.itemInStack))
                {
                    playerInventoryItems[playerInventoryItemStack.itemInStack] = 0;
                }
                playerInventoryItems[playerInventoryItemStack.itemInStack] += playerInventoryItemStack.amountInStack;
            }
        }

    }

    private bool PlayerHasEnoughItemsToCraftRecepie(Recepie recepieToCraft, int[] amountsInInventory)
    {
        bool hasEnoughItems = true;
        int i = 0;

        foreach (KeyValuePair<Item, int> requiredItem in recepieToCraft.requiredItems)
        {
            amountsInInventory[i] = 0;

            if (!playerInventory.InventoryContainsItemAndAmount(requiredItem.Key, requiredItem.Value, out amountsInInventory[i]))
            {
                hasEnoughItems = false;
            }

            ++i;
        }
        return hasEnoughItems;
    }

    private void RemoveRecepieRequiredItems(Recepie recepieToCraft)
    {
        foreach (KeyValuePair<Item, int> requiredItem in recepieToCraft.requiredItems)
        {
            playerInventory.SubstractNItemsFromInventory(requiredItem.Key, requiredItem.Value);
        }
    }

    private void AddRecepieResultingItems(Recepie recepieToCraft)
    {
        for (int i = 0; i < recepieToCraft.resultingItem.Value; ++i)
        {
            if (playerInventory.AddItemToInventory(recepieToCraft.resultingItem.Key))
            {
                InvokeOnItemCraft(recepieToCraft.resultingItemUnit.ID);
            }
            else
            {
                AddRecepieResultingItemToStorage(recepieToCraft);
            }
        }
        
    }

    private void AddRecepieResultingItemToStorage(Recepie recepieToCraft)
    {
        if (storageStationInventory.AddItemToInventory(recepieToCraft.resultingItem.Key))
        {
            InvokeOnItemSentToStorage();
        }
        else
        {
            GameObject item = Instantiate(recepieToCraft.resultingItem.Key.prefab, droppedItemPosition, Quaternion.identity);
            item.GetComponent<ItemGameObject>().DropsRandom(30f);
        }
    }


    public void TestSelectedReceipe(int selectedRecepieIndex, int[] amountsInInventory)
    {
        UpdatePlayerInventoryData();

        canCraft = PlayerHasEnoughItemsToCraftRecepie(availableRecepies[selectedRecepieIndex], amountsInInventory);        
    }


    public void RecepieWasSelected(int selectedRecepieIndex)
    {
        UpdatePlayerInventoryData();
        if (canCraft)
        {
            RemoveRecepieRequiredItems(availableRecepies[selectedRecepieIndex]);
            AddRecepieResultingItems(availableRecepies[selectedRecepieIndex]);
            if (OnCrafting != null) OnCrafting();
            if (OnReceipeCraftingSuccess != null) OnReceipeCraftingSuccess(selectedRecepieIndex);
        }
        else
        {
            if (OnCraftingFail != null) OnCraftingFail();
        }
    }


    private void InvokeOnItemCraft(int itemID)
    {
        if (OnItemCraft != null) OnItemCraft(itemID);
    }

    private void InvokeOnItemSentToStorage()
    {
        if (OnItemSentToStorage != null) 
            OnItemSentToStorage(); //I put the items you crafted in the Storage, since you had no inventory space.
    }
}
