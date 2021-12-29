using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesStation : InteractStation
{
    UpgradesSystem upgradesSystem;
    [SerializeField] GameObject upgradesMenu;

    void Start()
    {
        upgradesSystem = GetComponent<UpgradesSystem>();
        upgradesSystem.Init(playerInventory);
    }

    
    public override void StationFunction()
    {
        // Open menu
        upgradesMenu.SetActive(true);
    }



    private void InitUpgradesMenu()
    {

    }


}
