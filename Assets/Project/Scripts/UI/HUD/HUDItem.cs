using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDItem : MonoBehaviour
{
    // Public
    public Image image;
    public Text textQuantity;
    public Text textName;

    public void SetImage (Sprite itemSprite)
    {
        image.sprite = itemSprite;
    }

    public void UpdateTextQuantity(string text)
    {
        textQuantity.text = text;
    }

    public void UpdateTextName(string text)
    {
        textName.text = text;
    }
}
