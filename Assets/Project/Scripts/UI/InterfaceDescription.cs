using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceDescription : MonoBehaviour
{
    [SerializeField] CanvasGroup descriptionCanvasGameObject;
    [SerializeField] TextMeshProUGUI descriptionText;

    private void Awake()
    {
        CloseDescription();
    }



    private void OnEnable()
    {
        InteractStation.OnDescriptionOpen += OpenDescription;
        InteractStation.OnInteractClose += CloseDescription;

        HoverButton.OnDescriptionOpen += OpenDescription;
        HoverButton.OnDescriptionClose += CloseDescription;

        CraftableItemButton.OnDescriptionSet += SetDescriptionText;
    }

    private void OnDisable()
    {
        InteractStation.OnDescriptionOpen -= OpenDescription;
        InteractStation.OnInteractClose -= CloseDescription;

        HoverButton.OnDescriptionOpen += OpenDescription;
        HoverButton.OnDescriptionClose += CloseDescription;

        CraftableItemButton.OnDescriptionSet -= SetDescriptionText;
    }


    private void OpenDescription()
    {
        descriptionCanvasGameObject.alpha = 1f;
    }

    private void CloseDescription()
    {
        descriptionCanvasGameObject.alpha = 0f;
    }


    private void SetDescriptionText(string text)
    {
        descriptionText.SetText(text);
    }
}
