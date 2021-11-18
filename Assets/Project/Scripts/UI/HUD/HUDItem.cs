using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDItem : MonoBehaviour
{
    // Public
    public Image image;
    public TextMeshProUGUI textQuantity;
    public TextMeshProUGUI textName;

    public void SetImage (Sprite itemSprite)
    {
        image.sprite = itemSprite;
    }

    public void SetTextQuantity(string text)
    {
        textQuantity.text = text;
    }

    public void SetTextName(string text)
    {
        textName.text = text;
    }
}
