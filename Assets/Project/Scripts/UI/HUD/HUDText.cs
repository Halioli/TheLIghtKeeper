using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class HUDText : MonoBehaviour
{
    enum DisplayMessege { NONE, PICKAXE_TOO_WEAK, PICK_UP_FAIL};


    private float FADE_TIME = 1.5f;
    private float FADE_OUT_TIME = 0.25f;
    private const float SHAKE_STRENGHT = 0.2f;
    private DisplayMessege displayMessage = DisplayMessege.NONE;

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI textMessege;
    [SerializeField] GameObject crossGameObject;


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
        if (displayMessage != DisplayMessege.PICKAXE_TOO_WEAK)
            textMessege.text = "Pickaxe too weak!";

        displayMessage = DisplayMessege.PICKAXE_TOO_WEAK;
        DisplayMessageAndCross(FADE_TIME);
    }


    private void DisplayItemPickUpError(float duration)
    {
        if (displayMessage != DisplayMessege.PICK_UP_FAIL)
            textMessege.text = "No inventory space!";

        displayMessage = DisplayMessege.PICK_UP_FAIL;
        DisplayMessageAndCross(duration);
    }


    private void DisplayMessageAndCross(float duration)
    {
        if (canvasGroup.alpha != 0f) return;

        StartCoroutine(DisplayMessage(duration));
        StartCoroutine(StartCrossAppears(duration));
    }

    IEnumerator DisplayMessage(float duration)
    {
        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(duration);

        Interpolator fadeOutInterpolator = new Interpolator(FADE_OUT_TIME);
        fadeOutInterpolator.ToMax();
        while (!fadeOutInterpolator.IsMax)
        {
            fadeOutInterpolator.Update(Time.deltaTime);
            canvasGroup.alpha = fadeOutInterpolator.Inverse;
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }

    IEnumerator StartCrossAppears(float duration)
    {
        crossGameObject.transform.DOPunchPosition(new Vector2(SHAKE_STRENGHT, 0f), duration, 5);
        yield return new WaitForSeconds(duration);
    }



}
