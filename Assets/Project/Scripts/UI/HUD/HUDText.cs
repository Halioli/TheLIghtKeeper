using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HUDText : MonoBehaviour
{
    private float FADE_TIME = 1.5f;

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI textMessege;


    private void OnEnable()
    {
        PlayerMiner.pickaxeNotStrongEnoughEvent += DisplayMineErrorText;
        ItemPickUp.OnItemPickUpFail += DisplayItemPickUpError;
    }

    private void OnDisable()
    {
        PlayerMiner.pickaxeNotStrongEnoughEvent -= DisplayMineErrorText;
        ItemPickUp.OnItemPickUpFail -= DisplayItemPickUpError;
    }


    private void DisplayMineErrorText()
    {
        textMessege.text = "Pickaxe too weak!";
        DoDisplayMessage(FADE_TIME);
    }


    private void DisplayItemPickUpError(float duration)
    {
        textMessege.text = "No inventory space!";
        DoDisplayMessage(duration);
    }


    private void DoDisplayMessage(float duration)
    {
        if (canvasGroup.alpha == 1f) return;

        StartCoroutine(DisplayMessage(duration));
    }

    IEnumerator DisplayMessage(float duration)
    {
        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(duration);

        canvasGroup.alpha = 0f;
    }


}
