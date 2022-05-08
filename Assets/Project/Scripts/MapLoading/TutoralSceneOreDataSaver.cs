using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class TutoralSceneOreDataSaver : MonoBehaviour
{
    [SerializeField] string fileName;
    [SerializeField] GameObject[] ores;


    private void OnEnable()
    {
        BrokenFurnace.OnTutorialFinish += SaveOreData;
    }

    private void OnDisable()
    {
        BrokenFurnace.OnTutorialFinish -= SaveOreData;
    }

    private string GetFilePath()
    {
        return Application.dataPath + "/safeData/" + fileName + ".json";
    }

    private void SaveOreData()
    {
        OreSaveData saveInventoryFileData = new OreSaveData(ores);

        string json = JsonUtility.ToJson(saveInventoryFileData);
        File.WriteAllText(GetFilePath(), json);
    }


}
