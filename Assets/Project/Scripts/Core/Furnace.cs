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
    public Item repairsItem;
    public Item upgradeItem;
    public GameObject player;

    //TextMesh gameobjects
    public GameObject interactText;
    public GameObject warning;
    public GameObject endGameMessage;
    public ParticleSystem addCoalParticleSystem;

    //Text references
    public TextMeshProUGUI numElementAddedText;
    public TextMeshProUGUI currentFuelText;
    public TextMeshProUGUI eventText;

    //Core light 
    public GameObject coreLight;

    // Private Attributes
    private const int MAX_FUEL_AMOUNT = 150;
    private const int STARTING_FUEL_AMOUNT = 50;
    private const int LOW_FUEL_AMOUNT = 30;
    private const int MAX_CORE_LEVEL = 5;
    private const float MAX_TIME_TEXT_ON_SCREEN = 1.5f; 
    private enum FurnaceEvents { CALM, NEEDS_COAL, NEEDS_REPAIRS, STABILIZING };
    FurnaceEvents furnaceEvents;

    //Core light 
    private int lightLevel = 0;

    //Fuel variables
    private int currentFuel = STARTING_FUEL_AMOUNT;
    private int numElementAdded = 0;
    
    //Allways false here
    private bool couroutineStartedAddCoal = false;
    private bool couroutineStartedConsumeCoal = false;

    //Scalation variables
    private Vector3 scaleChange = new Vector3(0.5f, 0.5f, 0f);

    private TextMeshProUGUI elementInputText;
    private float fuelDurationInSeconds = 2.5f;
    private int fuelConsumedByTime = 1;
    private int fuelAmountPerCoalUnit = 10;
    private int fuelAmountPerIronUnit = 25;
    private float currentTextTime = 0f;

    private string[] eventTextToDisplay = { "", "Needs Coal", "Needs Iron", "Sabilizing..." };
    private string[] elementInputTextsToDisplay = { "Furnace is stable", "Press E to add 1 Coal", "Press E to add 1 Iron", "Sabilizing..." };
    private string[] numElementAddedTextsToDisplay = { " NULL", " Coal", " Iron", " NULL" };

    // Methods
    private void Start()
    {
        furnaceEvents = FurnaceEvents.CALM;
        addCoalParticleSystem.Stop();
        numElementAddedText.text = "";
        elementInputText = interactText.GetComponent<TextMeshProUGUI>();
        
        interactText.SetActive(false);
        warning.SetActive(false);
        endGameMessage.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            furnaceEvents = FurnaceEvents.CALM;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            StartEvent(1);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            StartEvent(2);
        }

        SwitchFurnaceEvents();

        CheckForEndGame();

        //If player enters the trigger area the interactionText will appears
        if (playerInsideTriggerArea)
        {
            GetInput(); //Waits the input from interactStation 
            PopUpAppears((int)furnaceEvents);
        }
        else
        {
            numElementAdded = 0;
            PopUpDisappears();
        }
    }

    private void OnEnable()
    {
        CoreUpgrade.OnCoreUpgrade += UpgradeFunction;
    }

    private void OnDisable()
    {
        CoreUpgrade.OnCoreUpgrade -= UpgradeFunction;
    }

    //From InteractStation script
    public override void StationFunction()
    {
        switch (furnaceEvents)
        {
            case FurnaceEvents.CALM:
            case FurnaceEvents.STABILIZING:
                break;

            case FurnaceEvents.NEEDS_COAL:
                if (playerInventory.SubstractItemToInventory(fuelItem) && currentFuel < MAX_FUEL_AMOUNT)
                {
                    FuelAdded((int)furnaceEvents);
                }
                else
                {
                    NoFuelToAdd((int)furnaceEvents);
                }
                break;

            case FurnaceEvents.NEEDS_REPAIRS:
                if (playerInventory.SubstractItemToInventory(repairsItem) && currentFuel < MAX_FUEL_AMOUNT)
                {
                    FuelAdded((int)furnaceEvents);
                }
                else
                {
                    NoFuelToAdd((int)furnaceEvents);
                }
                break;

            default:
                break;
        }
    }

    //Interactive pop up appears
    private void PopUpAppears(int eventState)
    {
        interactText.SetActive(true);

        // Different text depending on event
        elementInputText.text = elementInputTextsToDisplay[eventState];
    }

    //Interactive pop up disappears
    private void PopUpDisappears()
    {
        interactText.SetActive(false);
    }

    //Ads coal and show pop up
    private void FuelAdded(int eventState)
    {
        switch (eventState)
        {
            case 1:
                currentFuel += fuelAmountPerCoalUnit;
                break;

            case 2:
                currentFuel += fuelAmountPerIronUnit;
                break;

            default:
                break;
        }

        if (currentFuel > MAX_FUEL_AMOUNT)
        {
            currentFuel = MAX_FUEL_AMOUNT;
        }

        numElementAdded += 1;
        numElementAddedText.text = "Added " + numElementAdded.ToString() + numElementAddedTextsToDisplay[eventState];
        addCoalParticleSystem.Play();

        if (!couroutineStartedAddCoal)
        {
            StartCoroutine(UsingYieldAddCoal(1));
        }
    }

    private void NoFuelToAdd(int eventState)
    {
        numElementAddedText.text = "No" + numElementAddedTextsToDisplay[eventState] + " to add";
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
            case FurnaceEvents.CALM:
                countdownActive = false;
                eventText.text = eventTextToDisplay[0];
                break;

            case FurnaceEvents.NEEDS_COAL:
                countdownActive = true;
                eventText.text = eventTextToDisplay[1];

                ConsumesFuel();
                CheckWarningMessageAppears();

                if (currentFuel >= MAX_FUEL_AMOUNT)
                {
                    furnaceEvents = FurnaceEvents.STABILIZING;
                }
                break;

            case FurnaceEvents.NEEDS_REPAIRS:
                countdownActive = true;
                eventText.text = eventTextToDisplay[2];

                ConsumesFuel();
                CheckWarningMessageAppears();

                if (currentFuel >= MAX_FUEL_AMOUNT)
                {
                    furnaceEvents = FurnaceEvents.STABILIZING;
                }
                break;

            case FurnaceEvents.STABILIZING:
                countdownActive = false;
                eventText.text = eventTextToDisplay[3];

                if (couroutineStartedConsumeCoal)
                {
                    couroutineStartedConsumeCoal = false;
                    StopCoroutine(UsingYieldCosumeCoal(fuelDurationInSeconds));
                }

                currentFuel = STARTING_FUEL_AMOUNT;
                furnaceEvents = FurnaceEvents.CALM;
                break;

            default:
                furnaceEvents = FurnaceEvents.STABILIZING;
                break;
        }
    }

    // Public Methods
    public override void UpgradeFunction()
    {
        if(lightLevel < MAX_CORE_LEVEL)
        {
            numElementAddedText.text = "Luxinite Added";
            coreLight.transform.localScale += scaleChange;
            ++lightLevel;

            if (!couroutineStartedAddCoal)
            {
                StartCoroutine(UsingYieldAddCoal(1));
            }
        }
    }

    public void StartEvent(int eventID)
    {
        furnaceEvents = (FurnaceEvents)eventID;
    }

    public void StopEvent()
    {
        furnaceEvents = FurnaceEvents.STABILIZING;
    }

    public int GetCurrentEventID()
    {
        return (int)furnaceEvents;
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

        numElementAddedText.text = "";
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
