using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMouseHoverable : MouseHoverable
{
    public static event MouseHoverAction OnMineMouseHoverStay;
    //public static event MouseHoverAction OnMineMouseHoverExit;


    private void OnMouseOver()
    {
        if (OnMineMouseHoverStay != null)
            OnMineMouseHoverStay();
    }

    //private void OnMouseExit()
    //{
    //    if (OnMineMouseHoverExit != null)
    //        OnMineMouseHoverExit();
    //}
}
