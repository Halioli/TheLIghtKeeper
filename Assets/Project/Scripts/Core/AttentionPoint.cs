using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionPoint : MonoBehaviour
{
    [SerializeField] protected bool isActive = true;
    [SerializeField] protected GameObject exclamationObject;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = exclamationObject.GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            StartCoroutine(FadeOut());
        }
    }

    public void ActivateExclamation()
    {
        isActive = true;
        exclamationObject.SetActive(true);
        Color spriteColor = spriteRenderer.color;
        spriteColor.a = 1f;
        spriteRenderer.color = spriteColor;
    }


    IEnumerator FadeOut()
    {
        isActive = false;
        Interpolator fadeInterpolator = new Interpolator(0.5f);
        fadeInterpolator.ToMax();

        Color spriteColor = spriteRenderer.color;


        while (!fadeInterpolator.isMaxPrecise)
        {
            fadeInterpolator.Update(Time.deltaTime);
            spriteColor.a = fadeInterpolator.Inverse;
            spriteRenderer.color = spriteColor;
            yield return null;
        }

        OnFadeEnd();
    }


    protected virtual void OnFadeEnd()
    {
        Destroy(gameObject);
    }


}
