using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



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
        ChatBox.OnFinishChatMessage += Disappear;
    }

    private void OnDisable()
    {
        ChatBox.OnFinishChatMessage -= Disappear;
    }

    public void AppearAtPosition(Vector2 position)
    {
        transform.position = position;

        //transform.DOComplete();
        //transform.DOPunchScale(Vector2.one, fadeTime, 1);
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
