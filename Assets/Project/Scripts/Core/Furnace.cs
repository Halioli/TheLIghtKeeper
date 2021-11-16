using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Furnace : InteractStation
{
    //TextMesh gameobjects
    public GameObject InteractText;
    public GameObject coalAddedText;
    public GameObject warning;
    public ParticleSystem addCoalParticleSystem;

    //Text references
    public Text numCoalAddedText;
    public Text currentFuelText;

    //Core light 
    public GameObject coreLight;
    private int lightLevel = 0;

    //Fuel vars
    private int currentFuel = 25;
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
    }
    // Update is called once per frame
    void Update()
    {
        ConsumesFuel();
        //Warning if currentFuel is low
        if(currentFuel < maxFuel / 3)
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
        numCoalAddedText.text = numCoalAdded.ToString();
        addCoalParticleSystem.Play();
        //Debug.Log(currentFuel);
        //Debug.Log(numCoalAdded);
        coalAddedText.SetActive(true);
        if (!couroutineStartedAddCoal)
        {
            StartCoroutine(UsingYieldAddCoal(3));
        }

    }

    //Waits x seconds to pop up disappear
    IEnumerator UsingYieldAddCoal(int seconds)
    {
        couroutineStartedAddCoal = true;

        yield return new WaitForSeconds(seconds);
        
        coalAddedText.SetActive(false);
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
        currentFuelText.text = currentFuel.ToString() + "/" + maxFuel.ToString();
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

}
