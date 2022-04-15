using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConstuctionPopUp : PopUp
{
    public TextMeshProUGUI[] itemQuantityText;

    public int[] itemQuantityValue;

    private void Start()
    {
        for (int i = 0; i < itemQuantityText.Length; i++)
        {
            itemQuantityText[i].text = itemQuantityValue[i].ToString();
        }
    }

    public void SetSpecificQuantityValue (int itemId, int newValue)
    {
        itemQuantityValue[itemId] = newValue;
        itemQuantityText[itemId].text = itemQuantityValue[itemId].ToString();
    }

    public void SetAllValue (int newValue)
    {
        for (int i = 0; i < itemQuantityText.Length; i++)
        {
            itemQuantityValue[i] = newValue;
            itemQuantityText[i].text = itemQuantityValue[i].ToString();
        }
    }
}
