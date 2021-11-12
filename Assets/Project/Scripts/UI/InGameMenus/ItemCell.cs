using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour
{
    public Image itemImage;
    public Text itemAmount;
    public Button button;

    public void SetItemImage(Image image)
    {
        itemImage = image;
    }

    public void SetItemAmount(int amount)
    {
        itemAmount.text = amount.ToString();
    }

    public void ClickedButton()
    {

    }

    public void SetToEmpty()
    {
        //itemImage = empty;
        itemAmount.text = " ";
    }
}
