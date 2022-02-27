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

    float moveDuration = 5f;
    float fadeDuration = 2f;


    public delegate void ItemPickUpDisplayAction();
    public static event ItemPickUpDisplayAction OnItemPickDisplayEnd;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void SetDisplay(int itemID, int itemAmount)
    {
        Item itemToDisplay = ItemLibrary.instance.GetItem(itemID);

        displayImage.sprite = itemToDisplay.sprite;

        displayText.text = "+" + itemAmount + " " + itemToDisplay.name;
    }

    public void DoDisplayAnimation(float endPosition)
    {        
        rectTransform.DOMoveY(rectTransform.position.y + endPosition, moveDuration);
        StartCoroutine(StartFadeOut());
    }

    IEnumerator StartFadeOut()
    {
        yield return new WaitForSeconds(moveDuration - fadeDuration);


        fadeInterpolator = new Interpolator(fadeDuration);
        fadeInterpolator.ToMax();
        while (!fadeInterpolator.isMaxPrecise)
        {
            fadeInterpolator.Update(Time.deltaTime);
            canvasGroup.alpha = fadeInterpolator.Inverse;
            yield return null;
        }

        if (OnItemPickDisplayEnd != null) OnItemPickDisplayEnd();

        Destroy(gameObject);
    }



}
