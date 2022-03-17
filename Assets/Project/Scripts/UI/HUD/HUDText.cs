using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class HUDText : MonoBehaviour
{
    enum DisplayMessege { NONE, PICKAXE_TOO_WEAK, PICK_UP_FAIL, LANTERN_RECHARGED};

    private float FADE_TIME = 1.5f;
    private float FADE_OUT_TIME = 0.25f;
    private const float SHAKE_STRENGHT = 0.2f;
    private DisplayMessege displayMessage = DisplayMessege.NONE;

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI textMessege;
    [SerializeField] GameObject crossGameObject;
    [SerializeField] GameObject exclamationGameObject;
    [SerializeField] GameObject lightningGameObject;


    private void OnEnable()
    {
        PlayerMiner.pickaxeNotStrongEnoughEvent += DisplayMineErrorText;
        ItemPickUp.OnItemPickUpFail += DisplayItemPickUpError;

        PlayerLightChecker.OnPlayerEntersCoreLight += DisplayLanternRecharged;
    }

    private void OnDisable()
    {
        PlayerMiner.pickaxeNotStrongEnoughEvent -= DisplayMineErrorText;
        ItemPickUp.OnItemPickUpFail -= DisplayItemPickUpError;

        PlayerLightChecker.OnPlayerEntersCoreLight -= DisplayLanternRecharged;
    }


    private void DisplayMineErrorText()
    {
        if (displayMessage == DisplayMessege.PICKAXE_TOO_WEAK && canvasGroup.alpha != 0f)
            return;

        textMessege.text = "Pickaxe too weak!";
        displayMessage = DisplayMessege.PICKAXE_TOO_WEAK;
        DisplayMessageAndCross(FADE_TIME);
    }


    private void DisplayItemPickUpError(float duration)
    {
        if (displayMessage == DisplayMessege.PICK_UP_FAIL && canvasGroup.alpha != 0f)
            return;

        textMessege.text = "No inventory space!";
        displayMessage = DisplayMessege.PICK_UP_FAIL;
        DisplayMessageAndCross(duration);
    }

    private void DisplayLanternRecharged()
    {
        if (displayMessage == DisplayMessege.LANTERN_RECHARGED && canvasGroup.alpha != 0f)
            return;

        textMessege.text = "Lantern charged up!";
        displayMessage = DisplayMessege.LANTERN_RECHARGED;
        DisplayMessageAndLightning(FADE_TIME);
    }




    private void DisplayMessageAndCross(float duration)
    {
        StartCoroutine(DisplayMessage(duration));
        StartCoroutine(StartCrossAppears(duration));
    }

    private void DisplayMessageAndExclamation(float duration)
    {
        StartCoroutine(DisplayMessage(duration));
        StartCoroutine(StartExclamationAppears(duration));
    }

    private void DisplayMessageAndLightning(float duration)
    {
        StartCoroutine(DisplayMessage(duration));
        StartCoroutine(StartLightningAppears(duration));
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
        exclamationGameObject.SetActive(false);
        crossGameObject.SetActive(true);
        lightningGameObject.SetActive(false);

        crossGameObject.transform.DOPunchPosition(new Vector2(SHAKE_STRENGHT, 0f), duration, 5);
        yield return new WaitForSeconds(duration);
    }

    IEnumerator StartExclamationAppears(float duration)
    {
        exclamationGameObject.SetActive(true);
        crossGameObject.SetActive(false);
        lightningGameObject.SetActive(false);

        exclamationGameObject.transform.DOPunchPosition(new Vector2(0f, SHAKE_STRENGHT), duration);
        yield return new WaitForSeconds(duration);
    }

    IEnumerator StartLightningAppears(float duration)
    {
        exclamationGameObject.SetActive(false);
        crossGameObject.SetActive(false);
        lightningGameObject.SetActive(true);

        lightningGameObject.transform.DOPunchPosition(new Vector2(SHAKE_STRENGHT, 0f), duration);
        yield return new WaitForSeconds(duration);
    }

}
