using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Furnace : InteractStation
{
    // Public Attributes
    public Item fuelItem;
    public Item upgradeItem;

    //TextMesh gameobjects
    public GameObject interactText;
    public GameObject warning;
    public ParticleSystem addCoalParticleSystem;

    //Text references
    public TextMeshProUGUI numCoalAddedText;
    public TextMeshProUGUI currentFuelText;

    //Core light 
    public GameObject coreLight;
    private int lightLevel = 0;

    //Fuel variables
    private const int STARTING_FUEL_AMOUNT = 35;
    private const int LOW_FUEL_AMOUNT = 30;
    private int currentFuel = STARTING_FUEL_AMOUNT;
    private int numCoalAdded = 0;
    private int maxFuel = 250;
    
    //Allways false here
    private bool couroutineStartedAddCoal = false;
    private bool couroutineStartedConsumeCoal = false;

    //Scalation variables
    private Vector3 scaleChange = new Vector3(5, 5, 5);

    private float fuelDurationInSeconds = 2f;
    private int fuelConsumedByTime = 1;
    private int fuelAmountPerCoalUnit = 10;

    private void Start()
    {
        addCoalParticleSystem.Stop();
        numCoalAddedText.text = "";
        interactText.SetActive(false);
        warning.SetActive(false);
    }

    
    void Update()
    {
        ConsumesFuel();
        
        //Warning if currentFuel is low
        if(currentFuel <= LOW_FUEL_AMOUNT)
        {
            warning.SetActive(true);
        }
        else
        {
            warning.SetActive(false);
        }

        //If player enters the trigger area the interactionText will appears
        if (playerInsideTriggerArea)
        {
            if (CheckPlayerInventoryForLuxinite())
            {
                UpgradeFunction();
            }

            GetInput();            //Waits the input from interactStation 
            PopUpAppears();        
        }
        else
        {
            numCoalAdded = 0;
            PopUpDisappears();
        }

    }

    //From InteractStation script
    public override void StationFunction()
    {
        if (playerInventory.SubstractItemToInventory(fuelItem))
        {
            FuelAdded();
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


    //Ads coal and show pop up
    private void FuelAdded()
    {
        currentFuel += fuelAmountPerCoalUnit;
        numCoalAdded += 1;
        numCoalAddedText.text = "Added " + numCoalAdded.ToString() + " Coal";
        addCoalParticleSystem.Play();
        
        if (!couroutineStartedAddCoal)
        {
            StartCoroutine(UsingYieldAddCoal(1));
        }

    }

    //Waits x seconds to pop up disappear
    IEnumerator UsingYieldAddCoal(int seconds)
    {
        couroutineStartedAddCoal = true;

        yield return new WaitForSeconds(seconds);
        
        numCoalAddedText.text = "";
        addCoalParticleSystem.Stop();

        couroutineStartedAddCoal = false;
    }

    //Waits x seconds to consume coal
    IEnumerator UsingYieldCosumeCoal(float seconds)
    {
        couroutineStartedConsumeCoal = true;

        yield return new WaitForSeconds(seconds);
        if(currentFuel > 0)
        {
            currentFuel -= fuelConsumedByTime;

        }
        couroutineStartedConsumeCoal = false;
    }

    //Function that consumes fuel
    private void ConsumesFuel()
    {
        if (!couroutineStartedConsumeCoal)
        {
            StartCoroutine(UsingYieldCosumeCoal(fuelDurationInSeconds));
        }
    }

    public override void UpgradeFunction()
    {
        if(lightLevel < 3)
        {
            numCoalAddedText.text = "Luxinite Added";
            coreLight.transform.localScale += scaleChange;
            lightLevel += 1;

            if (!couroutineStartedAddCoal)
            {
                StartCoroutine(UsingYieldAddCoal(1));
            }
        }
    }

    private bool CheckPlayerInventoryForLuxinite()
    {
        return playerInventory.SubstractItemToInventory(upgradeItem);
    }

    public int GetMaxFuel()
    {
        return maxFuel;
    }

    public int GetCurrentFuel()
    {
        return currentFuel;
    }
}
