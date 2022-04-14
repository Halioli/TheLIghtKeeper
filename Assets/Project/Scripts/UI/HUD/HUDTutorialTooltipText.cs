using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class HUDTutorialTooltipText : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI textMessege;
    [SerializeField] GameObject leftClickGameObject;
    [SerializeField] GameObject rightClickGameObject;

    bool isTooltipDisplayed = false;
    float timeBeforeFade;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
    }


    private void OnEnable()
    {
        TutorialTooltip.OnPause += SetTooltip;
        TutorialTooltip.OnResume += StopDisplaying;
    }

    private void OnDisable()
    {
        TutorialTooltip.OnPause -= SetTooltip;
        TutorialTooltip.OnResume -= StopDisplaying;
    }


    private void SetTooltip(TutorialTooltip.TutorialTooltipType type, string messege)
    {
        if (type == TutorialTooltip.TutorialTooltipType.MOUSE_LEFT) SetLeftClick();
        else if (type == TutorialTooltip.TutorialTooltipType.MOUSE_RIGHT) SetRightClick();

        textMessege.text = messege;
        
        isTooltipDisplayed = true;

        StartCoroutine(DisplayUntilResume());
    }

    private void SetLeftClick()
    {
        leftClickGameObject.SetActive(true);
        rightClickGameObject.SetActive(false);
    }

    private void SetRightClick()
    {
        leftClickGameObject.SetActive(false);
        rightClickGameObject.SetActive(true);
    }

    private void StopDisplaying(float timeBeforeFade)
    {
        isTooltipDisplayed = false;
        this.timeBeforeFade = timeBeforeFade;
    }



    IEnumerator DisplayUntilResume()
    {
        canvasGroup.alpha = 1f;

        yield return new WaitUntil(() => !isTooltipDisplayed);


        yield return new WaitForSeconds(timeBeforeFade);


        Interpolator fadeOutInterpolator = new Interpolator(0.25f);
        fadeOutInterpolator.ToMax();
        while (!fadeOutInterpolator.IsMax)
        {
            fadeOutInterpolator.Update(Time.deltaTime);
            canvasGroup.alpha = fadeOutInterpolator.Inverse;
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }


}
