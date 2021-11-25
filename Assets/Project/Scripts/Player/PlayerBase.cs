using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    // Protected methods
    protected PlayerInputs playerInputs;

    void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
    }

}
