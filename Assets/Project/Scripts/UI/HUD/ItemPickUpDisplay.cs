using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class ItemPickUpDisplay : MonoBehaviour
{
    Interpolator fadeInterpolator;
    CanvasGroup canvasGroup;
    RectTransform rectTransform;
    [SerializeField] TextMeshProUGUI displayText;
    [SerializeField] Image displayImage;

    bool isFadeOutStarted = false;
    float totalDuration = 7f;
    float fadeDuration = 1f;

    Item itemToDisplay;
    float lastItemAmount = 0;

    public delegate void ItemPickUpDisplayAction(int itemID);
    public static event ItemPickUpDisplayAction OnItemPickDisplayEnd;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void SetDisplay(int itemID, int itemAmount)
    {
        itemToDisplay = ItemLibrary.instance.GetItem(itemID);

        displayImage.sprite = itemToDisplay.sprite;

        UpdateDisplayText(itemAmount);
    }

    public void UpdateDisplayText(int itemAmount)
    {
        lastItemAmount += itemAmount;
        displayText.text = "+" + lastItemAmount + " " + itemToDisplay.name;

        DoDisplayAnimation();
    }

    private void DoDisplayAnimation()
    {
        displayImage.GetComponent<RectTransform>().DOPunchScale(Vector2.up * 0.5f, 0.5f);
        displayText.GetComponent<RectTransform>().DOPunchScale(Vector2.up * 0.5f, 0.5f);
        //rectTransform.DOPunchScale(Vector2.up, 0.5f);

        if (isFadeOutStarted)
        {
            StopCoroutine("StartFadeOut");
        }

        StartCoroutine("StartFadeOut");
    }


    IEnumerator StartFadeOut()
    {
        isFadeOutStarted = true;
        canvasGroup.alpha = 1f;
        yield return new WaitForSeconds(totalDuration - fadeDuration);


        fadeInterpolator = new Interpolator(fadeDuration);
        fadeInterpolator.ToMax();
        while (!fadeInterpolator.isMaxPrecise)
        {
            fadeInterpolator.Update(Time.deltaTime);
            canvasGroup.alpha = fadeInterpolator.Inverse;
            yield return null;
        }

        if (OnItemPickDisplayEnd != null) OnItemPickDisplayEnd(itemToDisplay.ID);
    }



    public void ResetPosition(float yOffset)
    {
        rectTransform.position = rectTransform.position - (Vector3.up * yOffset);
    }


}
