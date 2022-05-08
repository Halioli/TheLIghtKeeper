using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class DataFilesReseter : MonoBehaviour
{
    [SerializeField] string[] fileNames;


    private string GetFilePath(string fileName)
    {
        return Application.dataPath + "/safeData/" + fileName + ".json";
    }


    public void RestetAllFiles()
    {
        for (int i = 0; i < fileNames.Length; ++i)
        {
            File.Delete(GetFilePath(fileNames[i]));
        }


        // Player Data file
        File.Delete(Application.persistentDataPath + "player.dat");

    }

}
