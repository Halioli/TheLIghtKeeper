using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameToolTips : MonoBehaviour
{
    public bool toolTipsActive;
    public TextMeshProUGUI toolTipText;
    public CanvasGroup toolTipsCanvasGroup;

    private void Start()
    {
        toolTipsActive = false;
    }

    void Update()
    {
        transform.position = PlayerInputs.instance.GetMousePositionInWorld();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (toolTipsActive)
        {
            ShowToolTips();
            switch (collision.tag)
            {
                case "Ore":
                    toolTipText.text = "Can be mined to yeld recources";
                    break;

                case "Enemy":
                    toolTipText.text = "A creaure, might be dangerous";
                    break;

                default:
                    HideToolTips();
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HideToolTips();
    }

    private void ShowToolTips()
    {
        toolTipsCanvasGroup.alpha = 1f;
    }

    private void HideToolTips()
    {
        toolTipsCanvasGroup.alpha = 0f;
    }

    public void SetTooltipsState(bool state)
    {
        toolTipsActive = state;
    }
}
