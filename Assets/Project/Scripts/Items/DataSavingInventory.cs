using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataSavingUtils
{
    public static string GetJsonFilePath(string fileName)
    {
        //return Application.dataPath + "/LightKeeper_Data/" + fileName + ".json";
        return Application.dataPath + "/" + fileName + ".json";
    }
}

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


    public void SaveInventory()
    {
        InventoryFileData saveInventoryFileData = new InventoryFileData(GetInventoryData());

        string json = JsonUtility.ToJson(saveInventoryFileData);
        File.WriteAllText(DataSavingUtils.GetJsonFilePath(storeFileName), json);

    }

    public void LoadInventory()
    {
        if (!File.Exists(DataSavingUtils.GetJsonFilePath(storeFileName))) return;

        string json = File.ReadAllText(DataSavingUtils.GetJsonFilePath(storeFileName));
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
