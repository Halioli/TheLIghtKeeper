using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class UpgardesDataSaver : MonoBehaviour
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



    private string GetFilePath()
    {
        return Application.dataPath + "/safeData/" + fileName + ".json";
    }




    public int[] GetLoadedUpgradesData()
    {
        if (!File.Exists(GetFilePath())) return null;

        string json = File.ReadAllText(GetFilePath());
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
        File.WriteAllText(GetFilePath(), json);        
    }


}