using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    // Protected attributes
    protected PlayerInputs playerInputs;
    protected PlayerStates playerStates;



    void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
        playerStates = GetComponent<PlayerStates>();
    }


}
