using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LorePilarsInfo : MonoBehaviour
{
    private bool isBeingShown = false;
    private CanvasGroup textCanvasGroup;

    [SerializeField] TextMeshProUGUI tittleTextMesh;
    [SerializeField] TextMeshProUGUI textTextMesh;

    void Start()
    {
        textCanvasGroup = GetComponentInChildren<CanvasGroup>();
        textCanvasGroup.alpha = 0f;
    }

    private void OnEnable()
    {
        LoreFunction.OnPilarInteract += SetTexts;
    }

    private void OnDisable()
    {
        LoreFunction.OnPilarInteract -= SetTexts;
    }

    private void SetTexts(string tittle, string text)
    {
        isBeingShown = !isBeingShown;
        if (isBeingShown)
        {
            PlayerInputs.instance.SetInGameMenuOpenInputs();

            tittleTextMesh.text = tittle;
            textTextMesh.text = text;

            textCanvasGroup.alpha = 1f;
        }
        else
        {
            HideUI();
        }
    }

    public void HideUI()
    {
        PlayerInputs.instance.SetInGameMenuCloseInputs();

        textCanvasGroup.alpha = 0f;
        isBeingShown = false;
    }
}
