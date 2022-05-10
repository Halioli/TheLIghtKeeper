using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AlmanacDataSaver : MonoBehaviour
{
    [SerializeField] string fileName;
    [SerializeField] AlmanacMaterialsChecker almanacMaterialsChecker;
    [SerializeField] AlmanacEnvironmentChecker almanacEnvironmentChecker;

    private class AlmanacData
    {
        public AlmanacData()
        {
            materialItems = new bool[0];
            environmentItems = new bool[0];
        }

        public AlmanacData(AlmanacScriptableObject[] materialItems, AlmanacScriptableObject[] environmentItems)
        {
            this.materialItems = new bool[materialItems.Length];
            for (int i = 0; i < materialItems.Length; ++i)
            {
                this.materialItems[i] = materialItems[i].hasBeenFound;
            }

            this.environmentItems = new bool[environmentItems.Length];
            for (int i = 0; i < environmentItems.Length; ++i)
            {
                this.environmentItems[i] = environmentItems[i].hasBeenFound;
            }
        }

        public bool[] materialItems;
        public bool[] environmentItems;

    }

    private void Start()
    {
        LoadData();
    }


    private void OnEnable()
    {
        PauseMenu.OnGameExit += SaveData;
    }

    private void OnDisable()
    {
        PauseMenu.OnGameExit -= SaveData;
    }





    public void SaveData()
    {

        AlmanacData saveAlmanacData = new AlmanacData(almanacMaterialsChecker.GetItems(), almanacEnvironmentChecker.GetItems());

        string json = JsonUtility.ToJson(saveAlmanacData);
        File.WriteAllText(DataSavingUtils.GetJsonFilePath(fileName), json);

    }

    public void LoadData()
    {
        if (!File.Exists(DataSavingUtils.GetJsonFilePath(fileName))) return;

        string json = File.ReadAllText(DataSavingUtils.GetJsonFilePath(fileName));
        AlmanacData loadedAlmanacData = JsonUtility.FromJson<AlmanacData>(json);

        if (loadedAlmanacData == null) return;

        if (loadedAlmanacData.materialItems.Length > 0) almanacMaterialsChecker.InitItems(loadedAlmanacData.materialItems);
        if (loadedAlmanacData.environmentItems.Length > 0) almanacEnvironmentChecker.InitItems(loadedAlmanacData.environmentItems);

    }







}
