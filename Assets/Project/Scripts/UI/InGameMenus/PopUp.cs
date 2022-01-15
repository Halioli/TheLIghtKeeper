using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUp : MonoBehaviour
{
    public CanvasGroup interactionCanvasGroup;
    public CanvasGroup messageCanvasGroup;
    public TextMeshProUGUI interactionText;
    public TextMeshProUGUI messageText;

    public void ShowAll()
    {
        ShowInteraction();
        ShowMessage();
    }

    public void ShowInteraction()
    {
        interactionCanvasGroup.alpha = 1f;
    }

    public void ShowMessage()
    {
        messageCanvasGroup.alpha = 1f;
    }

    public void HideAll()
    {
        HideInteraction();
        HideMessage();
    }

    public void HideInteraction()
    {
        interactionCanvasGroup.alpha = 0f;
    }

    public void HideMessage()
    {
        messageCanvasGroup.alpha = 0f;
    }

    public void ChangeInteractionText(string newText)
    {
        interactionText.text = newText;
    }

    public void ChangeMessageText(string newText)
    {
        messageText.text = newText;
    }
}
