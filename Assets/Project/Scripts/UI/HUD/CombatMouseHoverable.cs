using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMouseHoverable : MouseHoverable
{
    public static event MouseHoverAction OnCombatMouseHoverStay;

    private void OnMouseOver()
    {
        if (OnCombatMouseHoverStay != null)
            OnCombatMouseHoverStay();
    }

}
