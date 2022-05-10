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


    private void SaveOreData()
    {
        OreSaveData saveOresData = new OreSaveData(ores);

        string json = JsonUtility.ToJson(saveOresData);
        File.WriteAllText(DataSavingUtils.GetJsonFilePath(fileName), json);
    }


}
