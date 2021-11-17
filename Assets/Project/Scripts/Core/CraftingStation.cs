using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStation : InteractStation
{
    // Public Attributes
    public GameObject interactText;
    public GameObject craftingCanvasGameObject;
    public GameObject playerHUDGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        // If player enters the trigger area the interactionText will appears
        if (playerInsideTriggerArea)
        {
            GetInput();            //Waits the input from interactStation 
            PopUpAppears();
        }
        else
        {
            PopUpDisappears();
        }
    }

    //From InteractStation script
    public override void StationFunction()
    {
        if (!craftingCanvasGameObject.activeInHierarchy)
        {
            playerHUDGameObject.SetActive(false);
            craftingCanvasGameObject.SetActive(true);
        }
        else
        {
            playerHUDGameObject.SetActive(true);
            craftingCanvasGameObject.SetActive(false);
        }
    }

    //Interactive pop up disappears
    private void PopUpAppears()
    {
        interactText.SetActive(true);
    }

    //Interactive pop up disappears
    private void PopUpDisappears()
    {
        interactText.SetActive(false);
    }
}
