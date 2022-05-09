    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputs : MonoBehaviour
{
    // Public Attributes
    public static PlayerInputs instance;

    public Vector2 mousePosition = new Vector2();
    public Vector2 mouseWorldPosition = new Vector2();
    public bool facingLeft = true;
    public bool canFlip = true;
    public bool canMove = true;
    public bool canMoveLantern = true;
    public bool isLanternPaused = false;
    public float playerReach = 3f;

    public bool canMine = true;
    public bool canAttack = true;
    public bool canPause = true;

    public bool ignoreLights = false;

    public bool isInGameMenu = false;

    public GameObject selectSpotGameObject;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }

        instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    // Methods
    public void SetInGameMenuOpenInputs()
    {
        isInGameMenu = true;
        
        canMine = false;
        canAttack = false;
        canPause = false;
        canMoveLantern = false;
    }

    public void SetInGameMenuCloseInputs()
    {
        isInGameMenu = false;

        canMine = true;
        canAttack = true;
        //canPause = true;
        StartCoroutine(lateCanPause(true));
        canMoveLantern = true;
    }

    IEnumerator lateCanPause(bool canPause)
    {
        yield return null;
        this.canPause = canPause;
    }


    public bool PlayerClickedMineButton()
    {
        if (PauseMenu.gameIsPaused || !instance.canMine || TutorialMessages.tutorialOpened) { return false; }

        return Input.GetButton("Fire1");
    }

    public bool IsAttackButtonDown()
    {
        if (PauseMenu.gameIsPaused || !instance.canAttack) { return false; }

        return Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Joystick1Button4);
    }

    public bool IsAttackButtonUp()
    {
        if (PauseMenu.gameIsPaused || !instance.canAttack) { return false; }

        return Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Joystick1Button4);
    }

    public bool PlayerClickedCloseLamp()
    {
        if (PauseMenu.gameIsPaused) { return false; }

        return Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button3);
    }

    public void SetNewMousePosition()
    {
        mousePosition = Input.mousePosition;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public Vector2 GetMousePositionInWorld()
    {
        SetNewMousePosition();
        return mouseWorldPosition;
    }

    public bool PlayerPressedInteractButton()
    {
        return !PauseMenu.gameIsPaused && Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button1);
    }

    public bool PlayerPressedInteractExitButton()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    public bool PlayerPressedUseButton()
    {
        if (PauseMenu.gameIsPaused) { return false; }

        return Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Joystick1Button0);
    }

    public bool PlayerPressedDropButton()
    {
        if (PauseMenu.gameIsPaused) { return false; }

        return Input.GetKeyDown(KeyCode.X);
    }

    public bool PlayerPressedPauseButton()
    {
        return canPause && Input.GetKeyDown(KeyCode.Escape) ||  Input.GetKeyDown(KeyCode.Joystick1Button7);
    }

    public Vector2 PlayerPressedMovementButtons()
    {
        if (canMove && !PauseMenu.gameIsPaused && !TutorialMessages.tutorialOpened)
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else
        {
            return Vector2.zero;
        }
    }

    public bool PlayerPressedAlmanacButton()
    {
        return Input.GetKeyDown(KeyCode.Tab);
    }


    public Vector2 PlayerMouseScroll()
    {
        return Input.mouseScrollDelta;
    }

    public void SpawnSelectSpotAtTransform(Transform transform)
    {
        GameObject selectedSpot = Instantiate(selectSpotGameObject, transform);
        selectedSpot.GetComponent<SelectSpot>().DoSelectLoop();
    }

    public void FlipSprite(Vector2 direction)
    {
        if (!instance.canFlip)
            return;

        if ((direction.x > 0 && !instance.facingLeft) || direction.x < 0 && instance.facingLeft)
        {
            instance.facingLeft = !instance.facingLeft;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Closing application...");
        Application.Quit();
    }


}
