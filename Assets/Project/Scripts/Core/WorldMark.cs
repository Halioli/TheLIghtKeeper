using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMark : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] float rotationSpeed = 1f;
    float fadeTime = 0.25f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //AppearAtPosition(Vector2.one);
    }


    void Update()
    {
        transform.RotateAroundLocal(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        ChatBox.OnFinishChatMessage += DeactivateSelf;
    }

    private void OnDisable()
    {
        ChatBox.OnFinishChatMessage -= DeactivateSelf;
    }

    private void ActivateSelf()
    {
        gameObject.SetActive(true);
    }

    private void DeactivateSelf()
    {
        gameObject.SetActive(false);
    }

    public void AppearAtPosition(Vector2 position)
    {
        transform.position = position;
        StartCoroutine(FadeIn());
    }

    public void ReppearAtPosition(Vector2 position)
    {
        StartCoroutine(Reappear(position));
    }

    public void Disappear()
    {
        StartCoroutine(FadeOut());
    }


    IEnumerator FadeIn()
    {
        Interpolator fadeInInterpolator = new Interpolator(fadeTime);
        fadeInInterpolator.ToMax();

        Color spriteColor = spriteRenderer.color;
        spriteColor.a = 0f;

        while (!fadeInInterpolator.isMaxPrecise)
        {
            fadeInInterpolator.Update(Time.deltaTime);
            spriteColor.a = fadeInInterpolator.Value;
            spriteRenderer.color = spriteColor;
            yield return null;
        }

        spriteColor.a = 1f;
        spriteRenderer.color = spriteColor;
    }

    IEnumerator FadeOut()
    {
        Interpolator fadeInInterpolator = new Interpolator(fadeTime);
        fadeInInterpolator.ToMax();

        Color spriteColor = spriteRenderer.color;
        spriteColor.a = 1f;

        while (!fadeInInterpolator.isMaxPrecise)
        {
            fadeInInterpolator.Update(Time.deltaTime);
            spriteColor.a = fadeInInterpolator.Inverse;
            spriteRenderer.color = spriteColor;
            yield return null;
        }

        spriteColor.a = 0f;
        spriteRenderer.color = spriteColor;
    }


    IEnumerator Reappear(Vector2 position)
    {
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(fadeTime*2);
        AppearAtPosition(position);
    }


}
