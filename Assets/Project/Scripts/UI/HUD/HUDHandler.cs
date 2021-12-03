using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDHandler : MonoBehaviour
{
    // Private Attributes
    private const float FADE_TIME = 2f;
    
    private float currentFadeTime;
    private int playerHealthValue;
    private int lampTimeValue;
    private int coreTimeValue;
    private CanvasGroup healthGroup;
    private CanvasGroup lampGroup;
    private CanvasGroup coreGroup;
    private CanvasGroup quickAccessGroup;
    private HealthSystem playerHealthSystem;

    // Public Attributes
    public HUDBar healthBar;
    public HUDBar lampBar;
    public HUDBar coreBar;

    public HUDItem itemRight;
    public HUDItem itemCenter;
    public HUDItem itemLeft;

    public Furnace furnace;
    public Lamp lamp;

    // Start is called before the first frame update
    private void Start()
    {
        currentFadeTime = 0f;
        playerHealthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
        //lamp = GameObject.FindGameObjectWithTag("Player").GetComponent<Lamp>();

        // Initalize health variables
        healthGroup = GetComponentsInChildren<CanvasGroup>()[0];
        playerHealthValue = playerHealthSystem.GetMaxHealth();
        healthBar.SetMaxValue(playerHealthValue);
        healthBar.UpdateText(CheckTextForZeros(playerHealthValue.ToString()));

        // Initialize lamp variables
        lampGroup = GetComponentsInChildren<CanvasGroup>()[1];
        lampTimeValue = (int)lamp.GetLampTimeRemaining();
        lampBar.SetMaxValue(lampTimeValue);
        lampBar.UpdateText(CheckTextForZeros(lampTimeValue.ToString()));

        // Initialize core variables
        coreGroup = GetComponentsInChildren<CanvasGroup>()[2];
        coreTimeValue = furnace.GetMaxFuel();
        coreBar.SetMaxValue(coreTimeValue);
        coreBar.UpdateText(CheckTextForZeros(coreTimeValue.ToString()));

        // Initialize quick access variables
        quickAccessGroup = GetComponentsInChildren<CanvasGroup>()[3];
    }

    private void Update()
    {
        playerHealthValue = playerHealthSystem.GetHealth();
        ChangeValueInHUD(healthBar, playerHealthValue, playerHealthValue.ToString());

        lampTimeValue = (int)lamp.GetLampTimeRemaining();
        ChangeValueInHUD(lampBar, lampTimeValue, lampTimeValue.ToString());

        coreTimeValue = furnace.GetCurrentFuel();
        ChangeValueInHUD(coreBar, coreTimeValue, coreTimeValue.ToString());

        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeCanvasGroupAlphaToOne(healthGroup);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            ChangeCanvasGroupAlphaToZero(healthGroup);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeCanvasGroupAlphaToValue(healthGroup, 0.5f);
        }
    }

    private string CheckTextForZeros(string text)
    {
        string zero = "0";

        if (text.Length < 2)
        {
            text = zero + text;
        }

        return text;
    }

    private void ChangeValueInHUD(HUDBar bar, int value, string text)
    {
        bar.SetValue(value);
        bar.UpdateText(CheckTextForZeros(text));
    }

    public void ChangeCanvasGroupAlphaToZero(CanvasGroup canvasGroup)
    {
        while (currentFadeTime < FADE_TIME)
        {
            currentFadeTime += Time.deltaTime;

            canvasGroup.alpha = 1f - Mathf.Clamp01(currentFadeTime / FADE_TIME);
        }
        currentFadeTime = 0f;
    }

    public void ChangeCanvasGroupAlphaToValue(CanvasGroup canvasGroup, float value)
    {
        while (currentFadeTime < FADE_TIME && canvasGroup.alpha > value)
        {
            currentFadeTime += Time.deltaTime;

            canvasGroup.alpha = 1f - Mathf.Clamp01(currentFadeTime / FADE_TIME);
        }
        currentFadeTime = 0f;
    }

    public void ChangeCanvasGroupAlphaToOne(CanvasGroup canvasGroup)
    {
        while (currentFadeTime < FADE_TIME)
        {
            currentFadeTime += Time.deltaTime;

            canvasGroup.alpha = 1f + Mathf.Clamp01(currentFadeTime / FADE_TIME);
        }
        currentFadeTime = 0f;
    }
}
