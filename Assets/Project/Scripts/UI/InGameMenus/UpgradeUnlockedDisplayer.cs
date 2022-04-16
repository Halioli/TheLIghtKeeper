using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class UpgradeUnlockedDisplayer : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] Image upgradeIcon;
    [SerializeField] Transform shakeTransform;
    [SerializeField] GameObject upgradedText;
    [SerializeField] GameObject upgradCompletedText;


    private void Awake()
    {
        canvasGroup.alpha = 0.0f;
    }


    public void DisplayUpgradeBanner(bool isMaxCompleted, string upgradeName, Image upgradeIcon)
    {
        this.upgradeName.text = upgradeName;
        this.upgradeIcon.sprite = upgradeIcon.sprite;

        upgradedText.SetActive(!isMaxCompleted);
        upgradCompletedText.SetActive(isMaxCompleted);



        ForceDisplayStop();

        StartCoroutine("Display");
    }


    IEnumerator Display()
    {
        shakeTransform.DOComplete();
        shakeTransform.DOPunchPosition(Vector2.left * 10, 2f, 3);


        Interpolator fadeInterpolator = new Interpolator(0.2f);

        fadeInterpolator.ToMax();
        while (!fadeInterpolator.isMaxPrecise)
        {
            fadeInterpolator.Update(Time.deltaTime);

            canvasGroup.alpha = fadeInterpolator.Value;
            yield return null;
        }


        yield return new WaitForSeconds(3f);


        fadeInterpolator.ToMin();
        while (!fadeInterpolator.isMinPrecise)
        {
            fadeInterpolator.Update(Time.deltaTime);

            canvasGroup.alpha = fadeInterpolator.Value;
            yield return null;
        }
    }


    public void ForceDisplayStop()
    {
        if (canvasGroup.alpha != 0.0f) StopCoroutine("Display");
        canvasGroup.alpha = 0.0f;
    }

}
