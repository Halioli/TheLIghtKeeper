using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    // Public Attributes
    protected Vector2 mousePosition = new Vector2();
    protected Vector2 mouseWorldPosition = new Vector2();


    // Methods
    public bool PlayerClickedMineButton()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    public void SetNewMousePosition()
    {
        mousePosition = Input.mousePosition;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    }

}
