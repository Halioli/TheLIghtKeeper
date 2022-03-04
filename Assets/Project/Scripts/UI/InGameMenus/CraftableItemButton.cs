using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftableItemButton : HoverButton
{
    public int buttonNumber = 0;

    public delegate void ClickedRecepieButtonAction(int number);
    public static event ClickedRecepieButtonAction OnClickedRecepieButton;

    public static event ClickedRecepieButtonAction OnHoverRecepieButton;

    public delegate void ExitRecepieButtonAction();
    public static event ExitRecepieButtonAction OnRecepieButtonHoverExit;


    public void OnClick()
    {
        if (OnClickedRecepieButton != null) OnClickedRecepieButton(buttonNumber);
    }

    public void DoOnHoverRecepieButton()
    {
        if (OnHoverRecepieButton != null) OnHoverRecepieButton(buttonNumber);
    }

    public void DoOnHoverExitRecepieButton()
    {
        if (OnRecepieButtonHoverExit != null) OnRecepieButtonHoverExit();
    }
}
