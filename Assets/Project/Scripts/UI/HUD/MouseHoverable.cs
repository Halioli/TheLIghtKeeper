using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoverable : MonoBehaviour
{
    public delegate void MouseHoverAction();
    public static event MouseHoverAction OnMouseHoverExit;


    private void OnMouseExit()
    {
        if (OnMouseHoverExit != null)
            OnMouseHoverExit();
    }

}
