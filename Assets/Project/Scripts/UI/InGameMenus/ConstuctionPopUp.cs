using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConstuctionPopUp : PopUp
{
    public TextMeshProUGUI firstItemQuantityText;
    public TextMeshProUGUI secondItemQuantityText;

    public int firstItemQuantityValue;
    public int secondItemQuantityValue;

    private void Start()
    {
        firstItemQuantityText.text = firstItemQuantityValue.ToString();
        secondItemQuantityText.text = secondItemQuantityValue.ToString();
    }

    public void SetFirstQuantityValue (int newValue)
    {
        firstItemQuantityValue = newValue;
        firstItemQuantityText.text = firstItemQuantityValue.ToString();
    }

    public void SetSecondQuantityValue (int newValue)
    {
        secondItemQuantityValue = newValue;
        secondItemQuantityText.text = secondItemQuantityValue.ToString();
    }

    public void SetAllValue (int newValue)
    {
        firstItemQuantityValue = newValue;
        firstItemQuantityText.text = firstItemQuantityValue.ToString();

        secondItemQuantityValue = newValue;
        secondItemQuantityText.text = secondItemQuantityValue.ToString();
    }
}
