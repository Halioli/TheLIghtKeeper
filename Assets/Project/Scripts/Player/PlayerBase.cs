using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    // Protected attributes
    protected PlayerStates playerStates;


    void Awake()
    {
        playerStates = GetComponent<PlayerStates>();
    }


}
