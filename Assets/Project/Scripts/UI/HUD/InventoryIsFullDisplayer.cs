using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class InventoryIsFullDisplayer : MonoBehaviour
{
    Interpolator fadeInterpolator;
    CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI inventoryIsFullText;
    //[SerializeField] Color textColor;

    [SerializeField] AudioSource audioSource;



    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        //inventoryIsFullText.color = textColor;
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
        canvasGroup.alpha = 1f;

        fadeInterpolator = new Interpolator(duration);
        fadeInterpolator.ToMax();
        while (!fadeInterpolator.isMaxPrecise)
        {
            fadeInterpolator.Update(Time.deltaTime);
            canvasGroup.alpha = fadeInterpolator.Inverse;
            yield return null;
        }
    }


    private void PlayTextPopUpSound()
    {
        audioSource.Play();
    }



}
