using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    // Protected methods
    protected PlayerInputs playerInputs;
    protected PlayerStates playerStates;

    void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
        playerStates = GetComponent<PlayerStates>();
    }

}
