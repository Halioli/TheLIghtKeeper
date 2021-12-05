using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportButton : MonoBehaviour
{
    public int buttonNumber;

    // Events
    public delegate void TeleportSelected(int teleportIndex);
    public static event TeleportSelected OnSelection;


    public void TeleportToLocation()
    {
        if (OnSelection != null)
        {
            OnSelection(buttonNumber);
        }
    }

}
