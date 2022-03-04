using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RequiredItemDisplay : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemAmountText;

    public void Init(int itemID, int itemAmount)
    {
        itemImage.sprite = ItemLibrary.instance.GetItem(itemID).sprite;
        itemAmountText.text = itemAmount.ToString();
    }

}
