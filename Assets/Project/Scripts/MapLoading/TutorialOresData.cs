using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialOresData", menuName = "Other/OresData")]

public class TutorialOresData : ScriptableObject
{
    public bool[] oresExist;


    public void SaveOreData(GameObject[] ores)
    {
        oresExist = new bool[ores.Length];

        for (int i = 0; i < ores.Length; ++i)
        {
            oresExist[i] = ores[i] != null;
        }
    }


}
