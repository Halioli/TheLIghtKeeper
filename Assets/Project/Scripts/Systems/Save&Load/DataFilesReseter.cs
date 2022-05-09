using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class DataFilesReseter : MonoBehaviour
{
    [SerializeField] string[] fileNames;


    public void RestetAllFiles()
    {
        for (int i = 0; i < fileNames.Length; ++i)
        {
            File.Delete(DataSavingUtils.GetJsonFilePath(fileNames[i]));
        }


        // Player Data file
        File.Delete(Application.persistentDataPath + "player.dat");

    }

}
