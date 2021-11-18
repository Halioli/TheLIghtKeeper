using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDHandler : MonoBehaviour
{
    // Private Attributes
    private int playerHealthValue;
    private int lampTimeValue;
    private int coreTimeValue;

    // Public Attributes
    public HUDBar healthBar;
    public HUDBar lampBar;
    public HUDBar coreBar;

    public HUDItem itemRight;
    public HUDItem itemCenter;
    public HUDItem itemLeft;

    public HealthSystem playerhealthSystem;
    public Lamp lamp;
    public Furnace furnace;

    // Start is called before the first frame update
    private void Start()
    {
        // Initalize health variables
        playerHealthValue = playerhealthSystem.GetMaxHealth();
        healthBar.SetMaxValue(playerHealthValue);
        healthBar.UpdateText(CheckTextForZeros(playerHealthValue.ToString()));

        // Initialize lamp variables
        lampTimeValue = (int)lamp.GetLampTimeRemaining();
        lampBar.SetMaxValue(lampTimeValue);
        lampBar.UpdateText(CheckTextForZeros(lampTimeValue.ToString()));

        // Initialize core variables
        coreTimeValue = furnace.GetMaxFuel();
        coreBar.SetMaxValue(coreTimeValue);
        coreBar.UpdateText(CheckTextForZeros(coreTimeValue.ToString()));
    }

    // Update is called once per frame
    private void Update()
    {
        playerHealthValue = playerhealthSystem.GetHealth();
        ChangeValueInHUD(healthBar, playerHealthValue, playerHealthValue.ToString());

        lampTimeValue = (int)lamp.GetLampTimeRemaining();
        ChangeValueInHUD(lampBar, lampTimeValue, lampTimeValue.ToString());

        coreTimeValue = furnace.GetCurrentFuel();
        ChangeValueInHUD(coreBar, coreTimeValue, coreTimeValue.ToString());
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
}
