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
    private Lamp lamp;

    // Public Attributes
    public float fadeSpeed = 0.003f;

    public HUDBar healthBar;
    public HUDBar lampBar;
    public HUDBar coreBar;

    public HUDItem itemRight;
    public HUDItem itemCenter;
    public HUDItem itemLeft;

    public Furnace furnace;

    // Start is called before the first frame update
    private void Start()
    {
        currentFadeTime = 0f;
        playerHealthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
        lamp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Lamp>();

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
            StartCoroutine("ChangeCanvasGroupAlphaToZero", healthGroup);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            
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

    IEnumerator ChangeCanvasGroupAlphaToZero(CanvasGroup canvasGroup)
    {
        float lerpAmount = 0f;

        while (lerpAmount < 1)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, lerpAmount);
            lerpAmount += fadeSpeed;
        }

        yield return null;
    }
}
