using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class OreSaveData
{
    public OreSaveData()
    {
        oresExist = new bool[0];
    }

    public OreSaveData(GameObject[] ores)
    {
        oresExist = new bool[ores.Length];

        for (int i = 0; i < ores.Length; ++i)
        {
            oresExist[i] = ores[i].activeInHierarchy;

            Debug.Log(i + " " + oresExist[i]);
        }
    }


    public bool[] oresExist;

}

public class PostTutorialSceneOreLoader : MonoBehaviour
{
    [SerializeField] string fileName;
    [SerializeField] GameObject[] ores;

    private void OnEnable()
    {
        LoadBaseScenes.OnKeepBlackFade += LoadOreData;
        BrokenFurnace.OnTutorialFinish += SaveOreData;
        PauseMenu.OnGameExit += SaveOreData;

        LoadOreData();
    }

    private void OnDisable()
    {
        LoadBaseScenes.OnKeepBlackFade -= LoadOreData;
        BrokenFurnace.OnTutorialFinish -= SaveOreData;
        PauseMenu.OnGameExit -= SaveOreData;
    }



    private void LoadOreData()
    {
        string json = File.ReadAllText(DataSavingUtils.GetJsonFilePath(fileName));
        OreSaveData loadedOreData = JsonUtility.FromJson<OreSaveData>(json);

        if (loadedOreData == null) return;

        for (int i = 0; i < loadedOreData.oresExist.Length; ++i)
        {
            ores[i].SetActive(loadedOreData.oresExist[i]);
        }
    }

    private void SaveOreData()
    {
        OreSaveData saveInventoryFileData = new OreSaveData(ores);

        string json = JsonUtility.ToJson(saveInventoryFileData);
        File.WriteAllText(DataSavingUtils.GetJsonFilePath(fileName), json);
    }

}
