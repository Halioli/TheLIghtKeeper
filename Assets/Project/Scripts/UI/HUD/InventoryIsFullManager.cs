using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class InventoryIsFullManager : MonoBehaviour
{
    Interpolator fadeInterpolator;
    CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI inventoryIsFullText;
    [SerializeField] Color textColor;

    [SerializeField] AudioSource audioSource;



    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        inventoryIsFullText.color = textColor;
    }

    private void OnEnable()
    {
        ItemPickUp.OnItemPickUpFail += InventoryIsFullTextPopUp;
    }

    private void OnDisable()
    {
        ItemPickUp.OnItemPickUpFail -= InventoryIsFullTextPopUp;
    }


    private void InventoryIsFullTextPopUp(float duration)
    {
        StartCoroutine(InventoryIsFullTextPopUpFade(duration));

        inventoryIsFullText.transform.DOPunchPosition(Vector3.right * 5f, duration);

        PlayTextPopUpSound();
    }

    IEnumerator InventoryIsFullTextPopUpFade(float duration)
    {
        fadeInterpolator = new Interpolator(duration / 2f);

        fadeInterpolator.ToMax();
        while (!fadeInterpolator.isMaxPrecise)
        {
            fadeInterpolator.Update(Time.deltaTime);
            canvasGroup.alpha = fadeInterpolator.Value;
            yield return null;
        }

        fadeInterpolator.ToMin();
        while (!fadeInterpolator.isMinPrecise)
        {
            fadeInterpolator.Update(Time.deltaTime);
            canvasGroup.alpha = fadeInterpolator.Value;
            yield return null;
        }

    }


    private void PlayTextPopUpSound()
    {
        audioSource.Play();
    }



}
