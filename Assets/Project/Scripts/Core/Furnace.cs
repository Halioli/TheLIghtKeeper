using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Furnace : InteractStation
{

    // Public Attributes
    public bool countdownActive { get; set; }

    public Item fuelItem;
    public Item upgradeItem;
    public GameObject player;
    FURNACE_EVENTS furnaceEvents;

    //TextMesh gameobjects
    public GameObject interactText;
    public GameObject warning;
    public GameObject endGameMessage;
    public ParticleSystem addCoalParticleSystem;

    //Text references
    public TextMeshProUGUI numCoalAddedText;
    public TextMeshProUGUI currentFuelText;

    //Core light 
    public GameObject coreLight;


    // Private Attributes
    private const int MAX_FUEL_AMOUNT = 250;
    private const int STARTING_FUEL_AMOUNT = 35;
    private const int LOW_FUEL_AMOUNT = 30;
    private const int MAX_CORE_LEVEL = 3;
    private const float MAX_TIME_TEXT_ON_SCREEN = 1.5f; 
    private enum FURNACE_EVENTS { CALM, NEEDS_COAL, NEEDS_REPAIRS };

    //Core light 
    private int lightLevel = 0;

    //Fuel variables
    private int currentFuel = STARTING_FUEL_AMOUNT;
    private int numCoalAdded = 0;
    
    //Allways false here
    private bool couroutineStartedAddCoal = false;
    private bool couroutineStartedConsumeCoal = false;

    //Scalation variables
    private Vector3 scaleChange = new Vector3(0.5f, 0.5f, 0f);

    private float fuelDurationInSeconds = 2.5f;
    private int fuelConsumedByTime = 1;
    private int fuelAmountPerCoalUnit = 10;
    private float currentTextTime = 0f;

    // Methods
    private void Start()
    {
        furnaceEvents = FURNACE_EVENTS.CALM;
        addCoalParticleSystem.Stop();
        numCoalAddedText.text = "";
        interactText.SetActive(false);
        warning.SetActive(false);
        endGameMessage.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            furnaceEvents = FURNACE_EVENTS.CALM;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            furnaceEvents = FURNACE_EVENTS.NEEDS_COAL;
        }

        SwitchFurnaceEvents();

        CheckForEndGame();

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
        if (playerInventory.SubstractItemToInventory(fuelItem) && currentFuel < MAX_FUEL_AMOUNT)
        {
            FuelAdded();
        }
        else
        {
            NoFuelToAdd();
        }
    }

    //Interactive pop up appears
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
        if (currentFuel > MAX_FUEL_AMOUNT)
        {
            currentFuel = MAX_FUEL_AMOUNT;
        }

        numCoalAdded += 1;
        numCoalAddedText.text = "Added " + numCoalAdded.ToString() + " Coal";
        addCoalParticleSystem.Play();

        if (!couroutineStartedAddCoal)
        {
            StartCoroutine(UsingYieldAddCoal(1));
        }
    }

    private void NoFuelToAdd()
    {
        numCoalAddedText.text = "No coal to add";
        if (!couroutineStartedAddCoal)
        {
            StartCoroutine(UsingYieldAddCoal(1));
        }

    }

    //Function that consumes fuel
    private void ConsumesFuel()
    {
        if (!couroutineStartedConsumeCoal)
        {
            StartCoroutine(UsingYieldCosumeCoal(fuelDurationInSeconds));
        }
    }

    private bool CheckIfNoFuelLeft()
    {
        return currentFuel <= 0;
    }

    private bool CheckIfMaxCoreLevel()
    {
        return lightLevel >= MAX_CORE_LEVEL;
    }

    private void CheckForEndGame()
    {
        if (CheckIfNoFuelLeft())
        {
            endGameMessage.SetActive(true);
            endGameMessage.GetComponent<TextMeshProUGUI>().text = "GAME OVER";
            currentTextTime += Time.deltaTime;
            if (currentTextTime >= MAX_TIME_TEXT_ON_SCREEN)
            {
                SceneManager.LoadScene("V0.1");
            }
        }
        else if (CheckIfMaxCoreLevel())
        {
            endGameMessage.SetActive(true);
            endGameMessage.GetComponent<TextMeshProUGUI>().text = "YOU WIN";
            currentTextTime += Time.deltaTime;
            if (currentTextTime >= MAX_TIME_TEXT_ON_SCREEN)
            {
                SceneManager.LoadScene("V0.1");
            }
        }
    }

    private void CheckWarningMessageAppears()
    {
        //Warning if currentFuel is low
        if (currentFuel <= LOW_FUEL_AMOUNT)
        {
            warning.SetActive(true);
        }
        else
        {
            warning.SetActive(false);
        }
    }

    private void SwitchFurnaceEvents()
    {
        switch (furnaceEvents)
        {
            case FURNACE_EVENTS.CALM:
                countdownActive = false;

                if (couroutineStartedConsumeCoal)
                {
                    couroutineStartedConsumeCoal = false;
                    StopCoroutine(UsingYieldCosumeCoal(fuelDurationInSeconds));
                }
                break;

            case FURNACE_EVENTS.NEEDS_COAL:
                countdownActive = true;
                
                ConsumesFuel();
                CheckWarningMessageAppears();

                if (currentFuel >= MAX_FUEL_AMOUNT)
                {
                    furnaceEvents = FURNACE_EVENTS.CALM;
                }
                break;

            case FURNACE_EVENTS.NEEDS_REPAIRS:
                countdownActive = true;

                ConsumesFuel();
                CheckWarningMessageAppears();
                break;

            default:
                countdownActive = false;

                if (couroutineStartedConsumeCoal)
                {
                    couroutineStartedConsumeCoal = false;
                    StopCoroutine(UsingYieldCosumeCoal(fuelDurationInSeconds));
                }
                break;
        }
    }

    // Public Methods
    public override void UpgradeFunction()
    {
        if(lightLevel < MAX_CORE_LEVEL)
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

    public int GetMaxFuel()
    {
        return MAX_FUEL_AMOUNT;
    }

    public int GetCurrentFuel()
    {
        return currentFuel;
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
        if (currentFuel > 0)
        {
            currentFuel -= fuelConsumedByTime;

        }
        couroutineStartedConsumeCoal = false;
    }
}
