using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAlmanacItem", menuName = "Almanac/ItemEntry")]

public class AlmanacScriptableObject : ScriptableObject
{
    public string name;
    public int ID;
    public string tag;
    [TextArea(5, 20)] public string description;
    public bool hasFound;
}
