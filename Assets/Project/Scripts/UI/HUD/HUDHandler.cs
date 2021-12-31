using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDHandler : MonoBehaviour
{
    // Private Attributes
    private const float FADE_TIME = 2f;
    private int coreTimeValue;
    private CanvasGroup coreGroup;

    // Public Attributes
    public HUDBar coreBar;
    public Furnace furnace;

    private void Start()
    {
        // Initialize core variables
        coreGroup = GetComponentInChildren<CanvasGroup>();
        coreTimeValue = furnace.GetMaxFuel();
        coreBar.SetMaxValue(coreTimeValue);
        coreBar.UpdateText(CheckTextForZeros(coreTimeValue.ToString()));
    }

    private void Update()
    {
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
