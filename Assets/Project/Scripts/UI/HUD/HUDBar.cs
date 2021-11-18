using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDBar : MonoBehaviour
{
    // Public
    public Slider slider;
    public TextMeshProUGUI textToShow;

    public void SetValue(int value)
    {
        slider.value = value;
    }

    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void UpdateText(string text)
    {
        textToShow.text = text;
    }
}
