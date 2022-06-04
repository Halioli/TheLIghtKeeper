using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class UpgradesDataSaver : MonoBehaviour
{
    [SerializeField] string fileName;


    public class UpgardesData
    {
        public UpgardesData()
        {
            upgradeBranchesLastUpgrade = new int[0];
        }

        public UpgardesData(int[] upgradeBranchesLastUpgrade)
        {
            this.upgradeBranchesLastUpgrade = upgradeBranchesLastUpgrade;
        }

        public int[] upgradeBranchesLastUpgrade;
    }





    public int[] GetLoadedUpgradesData()
    {
        if (!File.Exists(DataSavingUtils.GetJsonFilePath(fileName))) return null;

        string json = File.ReadAllText(DataSavingUtils.GetJsonFilePath(fileName));
        UpgardesData loadedUpgardesData = JsonUtility.FromJson<UpgardesData>(json);

        if (loadedUpgardesData == null) return null;


        int[] upgardesData = new int[loadedUpgardesData.upgradeBranchesLastUpgrade.Length];

        for (int i = 0; i < loadedUpgardesData.upgradeBranchesLastUpgrade.Length; ++i)
        {
            upgardesData[i] = loadedUpgardesData.upgradeBranchesLastUpgrade[i];
        }

        return upgardesData;
    }

    public void SaveUpgradesData(int[] upgardesData)
    {
        UpgardesData saveInventoryFileData = new UpgardesData(upgardesData);

        string json = JsonUtility.ToJson(saveInventoryFileData);
        File.WriteAllText(DataSavingUtils.GetJsonFilePath(fileName), json);        
    }


}