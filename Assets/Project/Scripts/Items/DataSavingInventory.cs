using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataSavingInventory : Inventory
{
    [SerializeField] string storeFileName;

    private class InventoryFileData
    {
        public InventoryFileData()
        {
            itemIDs = new int[0];
            itemAmounts = new int[0];
        }

        public InventoryFileData(Dictionary<int, int> inventoryData)
        {
            itemIDs = new int[inventoryData.Count];
            itemAmounts = new int[inventoryData.Count];

            int i = 0;
            foreach (KeyValuePair<int, int> stackData in inventoryData)
            {
                itemIDs[i] = stackData.Key;
                itemAmounts[i] = stackData.Value;
                ++i;
            }
        }

        public int[] itemIDs;
        public int[] itemAmounts;

    }



    protected void OnEnable()
    {
        BrokenFurnace.OnTutorialFinish += SaveInventory;
        PauseMenu.OnGameExit += SaveInventory;
    }

    protected void OnDisable()
    {
        BrokenFurnace.OnTutorialFinish -= SaveInventory;
        PauseMenu.OnGameExit -= SaveInventory;
    }


    public override void InitInventory()
    {
        base.InitInventory(); // Resets the whole inventory (all stacks are empty)
        LoadInventory();

        gotChanged = true;
        inventoryIsEmpty = false;
    }


    private string GetFilePath()
    {
        return Application.dataPath + "/safeData/" + storeFileName + ".json";
    }


    public void SaveInventory()
    {
        InventoryFileData saveInventoryFileData = new InventoryFileData(GetInventoryData());

        string json = JsonUtility.ToJson(saveInventoryFileData);
        File.WriteAllText(GetFilePath(), json);

    }

    public void LoadInventory()
    {
        string json = File.ReadAllText(GetFilePath());
        InventoryFileData loadedInventoryFileData = JsonUtility.FromJson<InventoryFileData>(json);

        if (loadedInventoryFileData == null) return;

        for (int i = 0; i < loadedInventoryFileData.itemIDs.Length; ++i)
        {
            Item item = ItemLibrary.instance.GetItem(loadedInventoryFileData.itemIDs[i]);
            AddNItemsToInventory(item, loadedInventoryFileData.itemAmounts[i]);
            //Debug.Log("Item: " + item.itemName + " x" + loadedInventoryFileData.itemAmounts[i]);
        }
    }

}
