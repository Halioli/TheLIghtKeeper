using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMouseHoverable : MouseHoverable
{
    public static event MouseHoverAction OnMineMouseHoverStay;

    private void OnMouseOver()
    {
        if (OnMineMouseHoverStay != null)
            OnMineMouseHoverStay();
    }

}
