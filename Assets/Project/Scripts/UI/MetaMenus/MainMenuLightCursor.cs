using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLightCursor : MonoBehaviour
{
    void Update()
    {
        transform.position = PlayerInputs.instance.GetMousePositionInWorld();
    }
}
