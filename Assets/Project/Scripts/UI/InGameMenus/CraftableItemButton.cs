using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftableItemButton : HoverButton
{
    // Private Attributes

    // Public Attributes
    public int buttonNumber = 0;
    public delegate void ClickedRecepieButtonAction(int numb);
    public static event ClickedRecepieButtonAction OnClickedRecepieButton;


    public void OnClick()
    {
        if (OnClickedRecepieButton != null)
            OnClickedRecepieButton(buttonNumber);
    }



}
