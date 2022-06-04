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

    //public Item fuelItem;
    //public Item repairsItem;
    public GameObject liftOffButtonGameObject;

    //TextMesh gameobjects
    public GameObject popUpGameObject;
    public ParticleSystem addCoalParticleSystem;

    //Core light 
    [SerializeField] ConeLightCircleInteriorLight coneCoreLight;
    [SerializeField] FirstShipUpgradeMessage firstShipUpgradeMessage;

    // Private Attributes
    private enum FurnaceEvents { CALM, NEEDS_COAL, NEEDS_REPAIRS, STABILIZING };
    FurnaceEvents furnaceEvents;
    private const int MAX_FUEL_AMOUNT = 150;
    private const int STARTING_FUEL_AMOUNT = 50;
    private const int LOW_FUEL_AMOUNT = 30;
    [SerializeField] private const int MAX_CORE_LEVEL = 5;
    private const float MAX_TIME_TEXT_ON_SCREEN = 1.5f;
    

    //Fuel variables
    private int currentFuel = STARTING_FUEL_AMOUNT;
    private int numElementAdded = 0;
    
    //Allways false here
    private bool couroutineStartedAddCoal = false;
    private bool couroutineStartedConsumeCoal = false;

    //Scalation variables
    public int lightLevel = 0;
    private float[] UPGRADE_LIGHT_DISTANCE = { 15f, 20f, 25f, 30f, 35f };
    private float START_UPGRADE_LIGHT_DISTANCE = 10f;
    private float currentColliderRadius;
    //[SerializeField] private CircleCollider2D coreLightCollider;
    //private Vector3 scaleChange = new Vector3(0.5f, 0.5f, 0f);

    private float fuelDurationInSeconds = 2.5f;
    private int fuelConsumedByTime = 1;
    private int fuelAmountPerCoalUnit = 20;
    private int fuelAmountPerIronUnit = 30;
    private float currentTextTime = 0f;

    private PopUp popUp;
    private string[] eventTextToDisplay = { "", "Needs Coal", "Needs Iron", "Stabilizing..." };
    private string[] elementInputTextsToDisplay = { "Furnace is stable", "Press E to add 1 Coal", "Press E to add 1 Iron", "Stabilizing..." };
    private string[] numElementAddedTextsToDisplay = { " NULL", " Coal", " Iron", " NULL" };

    // Methods
    private void Awake()
    {
        furnaceEvents = FurnaceEvents.CALM;
        //coreLightCollider = coreLight.GetComponent<CircleCollider2D>();
        popUp = popUpGameObject.GetComponent<PopUp>();
        addCoalParticleSystem.Stop();
        popUp.ChangeMessageText("");

        popUp.HideAll();
        popUpGameObject.SetActive(false);


        currentColliderRadius = START_UPGRADE_LIGHT_DISTANCE;
        coneCoreLight.SetDistance(currentColliderRadius);
        coneCoreLight.Expand(1f);

    }



    void Update()
    {
        //SwitchFurnaceEvents();

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

    //From InteractStation script
    public override void StationFunction()
    {
        switch (furnaceEvents)
        {
            case FurnaceEvents.CALM:
            case FurnaceEvents.STABILIZING:
                break;

            //case FurnaceEvents.NEEDS_COAL:
            //    if (playerInventory.SubstractItemFromInventory(fuelItem) && currentFuel < MAX_FUEL_AMOUNT)
            //    {
            //        FuelAdded((int)furnaceEvents);
            //    }
            //    else
            //    {
            //        NoFuelToAdd((int)furnaceEvents);
            //    }
            //    break;

            //case FurnaceEvents.NEEDS_REPAIRS:
            //    if (playerInventory.SubstractItemFromInventory(repairsItem) && currentFuel < MAX_FUEL_AMOUNT)
            //    {
            //        FuelAdded((int)furnaceEvents);
            //    }
            //    else
            //    {
            //        NoFuelToAdd((int)furnaceEvents);
            //    }
            //    break;

            default:
                break;
        }
    }

    //Interactive pop up appears
    private void PopUpAppears(int eventState)
    {
        popUpGameObject.SetActive(true);
        popUp.ShowInteraction();

        // Different text depending on event
        popUp.ChangeInteractionText(elementInputTextsToDisplay[eventState]);
    }

    //Interactive pop up disappears
    private void PopUpDisappears()
    {
        popUpGameObject.GetComponent<PopUp>().HideAll();
        popUpGameObject.SetActive(false);
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
        popUp.ChangeMessageText("Added " + numElementAdded.ToString() + numElementAddedTextsToDisplay[eventState]);
        addCoalParticleSystem.Play();

        if (!couroutineStartedAddCoal)
        {
            StartCoroutine(UsingYieldAddCoal(1));
        }
    }

    private void NoFuelToAdd(int eventState)
    {
        popUp.ChangeMessageText("No" + numElementAddedTextsToDisplay[eventState] + " to add");
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

    //private void SwitchFurnaceEvents()
    //{
    //    switch (furnaceEvents)
    //    {
    //        case FurnaceEvents.CALM:
    //            countdownActive = false;
    //            eventText.text = eventTextToDisplay[0];
    //            break;

    //        case FurnaceEvents.NEEDS_COAL:
    //            countdownActive = true;
    //            eventText.text = eventTextToDisplay[1];

    //            ConsumesFuel();
    //            CheckWarningMessageAppears();

    //            if (currentFuel >= MAX_FUEL_AMOUNT)
    //            {
    //                furnaceEvents = FurnaceEvents.STABILIZING;
    //            }
    //            break;

    //        case FurnaceEvents.NEEDS_REPAIRS:
    //            countdownActive = true;
    //            eventText.text = eventTextToDisplay[2];

    //            ConsumesFuel();
    //            CheckWarningMessageAppears();

    //            if (currentFuel >= MAX_FUEL_AMOUNT)
    //            {
    //                furnaceEvents = FurnaceEvents.STABILIZING;
    //            }
    //            break;

    //        case FurnaceEvents.STABILIZING:
    //            countdownActive = false;
    //            eventText.text = eventTextToDisplay[3];

    //            if (couroutineStartedConsumeCoal)
    //            {
    //                couroutineStartedConsumeCoal = false;
    //                StopCoroutine(UsingYieldCosumeCoal(fuelDurationInSeconds));
    //            }

    //            currentFuel = STARTING_FUEL_AMOUNT;
    //            furnaceEvents = FurnaceEvents.CALM;
    //            break;

    //        default:
    //            furnaceEvents = FurnaceEvents.STABILIZING;
    //            break;
    //    }
    //}

    private void OnEnable()
    {
        CoreUpgrade.OnCoreUpgrade += UpgradeFunction;
    }

    private void OnDisable()
    {
        CoreUpgrade.OnCoreUpgrade -= UpgradeFunction;
    }

    // Public Methods
    public override void UpgradeFunction()
    {
        if (lightLevel >= MAX_CORE_LEVEL) return;


        //popUp.ChangeMessageText("Luxinite Added");
        //coreLightCollider.radius = currentColliderRadius;

        currentColliderRadius = UPGRADE_LIGHT_DISTANCE[lightLevel];


        coneCoreLight.SetDistance(currentColliderRadius);
        coneCoreLight.ExtraExpand(400, 400, 0.8f);

        if (!couroutineStartedAddCoal)
        {
            StartCoroutine(UsingYieldAddCoal(1));
        }


        ++lightLevel;



        if (lightLevel == 1)
        {
            firstShipUpgradeMessage.EnableFirstShipUpgradeMessege();
            PlayerPrefs.SetInt("FirstTime", 0);
            PlayerPrefs.SetInt("TutorialFinished", 1);
        }
        else if (lightLevel == MAX_CORE_LEVEL)
        {
            if (!liftOffButtonGameObject.activeInHierarchy)
            {
                liftOffButtonGameObject.SetActive(true);
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

    public int GetLightLevel()
    {
        return lightLevel;
    }

    //Waits x seconds to pop up disappear
    IEnumerator UsingYieldAddCoal(int seconds)
    {
        couroutineStartedAddCoal = true;

        yield return new WaitForSeconds(seconds);

        popUp.ChangeMessageText("");
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
