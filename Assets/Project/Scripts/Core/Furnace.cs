using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Furnace : InteractStation
{
    //TextMesh gameobjects
    public GameObject InteractText;
    public GameObject warning;
    public ParticleSystem addCoalParticleSystem;

    //Text references
    public TextMeshProUGUI numCoalAddedText;
    public TextMeshProUGUI currentFuelText;

    //Core light 
    public GameObject coreLight;
    private int lightLevel = 0;

    //Fuel vars
    private int currentFuel = 35;
    private int numCoalAdded = 0;
    private int maxFuel = 250;
    
    //Allways false here
    private bool couroutineStartedAddCoal = false;
    private bool couroutineStartedConsumeCoal = false;

    //Scalation vars
    private Vector3 scaleChange = new Vector3(5, 5, 5);


    private void Start()
    {
        addCoalParticleSystem.Stop();
        numCoalAddedText.text = "";
        InteractText.SetActive(false);
        warning.SetActive(false);
    }

    
    void Update()
    {
        ConsumesFuel();
        
        //Warning if currentFuel is low
        if(currentFuel <= 30)//< maxFuel / 3)
        {
            Debug.Log("Warning Low Fuel");
            warning.SetActive(true);
        }
        else
        {
            warning.SetActive(false);
        }

        //If player enters the trigger area the interactionText will appears
        if (playerInsideTriggerArea)
        {
            Debug.Log("playerInsideTriggerArea");
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
        CoalAdded();
    }

    //Interactive pop up disappears
    private void PopUpAppears()
    {
        InteractText.SetActive(true);
    }

    //Interactive pop up disappears
    private void PopUpDisappears()
    {
        InteractText.SetActive(false);
    }


    //Ads coal and show pop up
    private void CoalAdded()
    {
        currentFuel += 1;
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
    IEnumerator UsingYieldCosumeCoal(int seconds)
    {
        couroutineStartedConsumeCoal = true;

        yield return new WaitForSeconds(seconds);
        if(currentFuel > 0)
        {
            currentFuel -= 1;

        }
        couroutineStartedConsumeCoal = false;
    }

    //Function that consumes fuel
    private void ConsumesFuel()
    {
        if (!couroutineStartedConsumeCoal)
        {
            StartCoroutine(UsingYieldCosumeCoal(2));
        }
    }

    public override void UpgradeFunction()
    {
        if(lightLevel < 3)
        {
            Debug.Log("Core upgraded");
            coreLight.transform.localScale += scaleChange;
            lightLevel += 1;
        }
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
