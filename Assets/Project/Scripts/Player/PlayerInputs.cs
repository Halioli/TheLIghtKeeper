using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    // Public Attributes
    public Vector2 mousePosition = new Vector2();
    public Vector2 mouseWorldPosition = new Vector2();
    public bool facingRight = false;

    // Methods
    public bool PlayerClickedMineButton()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    public bool PlayerClickedAttackButton()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    public void SetNewMousePosition()
    {
        mousePosition = Input.mousePosition;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public bool PlayerPressedInteractButton()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    public bool PlayerPressedUseButton()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }

    public bool PlayerPressedInventoryButton()
    {
        return Input.GetKeyDown(KeyCode.Tab);
    }

    public Vector2 PlayerPressedMovementButtons()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public Vector2 PlayerMouseScroll()
    {
        return Input.mouseScrollDelta;
    }
}
