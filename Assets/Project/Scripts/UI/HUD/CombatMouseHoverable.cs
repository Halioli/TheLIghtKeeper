using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMouseHoverable : MouseHoverable
{
    public static event MouseHoverAction OnCombatMouseHoverStay;
    //public static event MouseHoverAction OnCombatMouseHoverExit;


    private void OnMouseOver()
    {
        if (OnCombatMouseHoverStay != null)
            OnCombatMouseHoverStay();
    }

    //private void OnMouseExit()
    //{
    //    if (OnCombatMouseHoverExit != null)
    //        OnCombatMouseHoverExit();
    //}
}
