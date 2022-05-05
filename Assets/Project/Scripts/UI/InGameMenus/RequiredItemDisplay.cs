using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RequiredItemDisplay : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemAmountText;
    [SerializeField] TextMeshProUGUI amountInInventoryText;

    [SerializeField] Color normalColor;
    [SerializeField] Color errorColor;

    public void Init(int itemID, int itemAmount, int amountInInventory)
    {
        itemImage.sprite = ItemLibrary.instance.GetItem(itemID).sprite;
        itemAmountText.text = "/" + itemAmount.ToString();

        if (amountInInventory >= itemAmount)
        {
            amountInInventoryText.color = normalColor;
        }
        else
        {
            amountInInventoryText.color = errorColor;
        }
        amountInInventoryText.text = amountInInventory.ToString();

    }

}
