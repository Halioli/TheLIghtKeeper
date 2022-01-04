using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairCursor : MonoBehaviour
{
    private GameObject playerGameObject;
    private Vector2 cursorHotspot;
    private CursorMode cursorMode;

    public Texture2D defaultCursorTexture;
    public Texture2D greenCursorTexture;
    public Texture2D redCursorTexture;

    private void Awake()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");

        cursorHotspot = new Vector2(15, 15);
        cursorMode = CursorMode.Auto;
    }

    private void OnEnable()
    {
        MouseHoverable.OnMouseHoverExit += SetDefaultCursorTexture;
        MineMouseHoverable.OnMineMouseHoverStay += DecideMineCursorTexture;
        CombatMouseHoverable.OnCombatMouseHoverStay += DecideCombatCursorTexture;
    }

    private void OnDisable()
    {
        MouseHoverable.OnMouseHoverExit -= SetDefaultCursorTexture;
        MineMouseHoverable.OnMineMouseHoverStay -= DecideMineCursorTexture;
        CombatMouseHoverable.OnCombatMouseHoverStay -= DecideCombatCursorTexture;
    }

    private void SetDefaultCursorTexture()
    {
        Cursor.SetCursor(defaultCursorTexture, cursorHotspot, cursorMode);
    }

    private void SetGreenMineCursorTexture()
    {
        Cursor.SetCursor(greenCursorTexture, cursorHotspot, cursorMode);
    }

    private void SetRedMineCursorTexture()
    {
        Cursor.SetCursor(redCursorTexture, cursorHotspot, cursorMode);
    }

    private void DecideMineCursorTexture()
    {
        if (Mathf.Abs(Vector2.Distance(PlayerInputs.instance.GetMousePositionInWorld(), playerGameObject.transform.position)) <= PlayerInputs.instance.playerReach)
        {
            SetGreenMineCursorTexture();
        }
        else
        {
            SetRedMineCursorTexture();
        }
    }

    private void SetGreenCombatCursorTexture()
    {
        Cursor.SetCursor(greenCursorTexture, cursorHotspot, cursorMode);
    }

    private void SetRedCombatCursorTexture()
    {
        Cursor.SetCursor(redCursorTexture, cursorHotspot, cursorMode);
    }

    private void DecideCombatCursorTexture()
    {
        if (Mathf.Abs(Vector2.Distance(PlayerInputs.instance.GetMousePositionInWorld(), playerGameObject.transform.position)) <= PlayerInputs.instance.playerReach)
        {
            SetGreenCombatCursorTexture();
        }
        else
        {
            SetRedCombatCursorTexture();
        }
    }
}
