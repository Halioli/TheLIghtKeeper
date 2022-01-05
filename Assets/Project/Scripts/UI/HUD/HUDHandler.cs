using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHandler : MonoBehaviour
{
    // Private Attributes
    private const float COUNTDOWN_FADE_TIME = 0.5f;
    private const float DEATH_FADE_TIME = 1f;
    private const float DAMAGE_FADE_TIME = 0.2f;
    private const float FADE_TIME = 0.5f;

    private int coreTimeValue;
    private bool showingCountdown;
    private CanvasGroup coreGroup;
    private CanvasGroup deathImageGroup;
    private CanvasGroup fadeOutGroup;
    private CanvasGroup recieveDamageGroup;

    // Public Attributes
    public HUDBar coreBar;
    public Furnace furnace;

    private void Start()
    {
        showingCountdown = false;

        // Initialize core variables
        coreGroup = GetComponentsInChildren<CanvasGroup>()[0];
        coreTimeValue = furnace.GetMaxFuel();
        coreBar.SetMaxValue(coreTimeValue);
        coreBar.UpdateText(CheckTextForZeros(coreTimeValue.ToString()));

        deathImageGroup = GetComponentsInChildren<CanvasGroup>()[1];
        fadeOutGroup = GetComponentsInChildren<CanvasGroup>()[2];
        recieveDamageGroup = GetComponentsInChildren<CanvasGroup>()[3];
    }

    private void Update()
    {
        if (furnace.countdownActive && !showingCountdown)
        {
            StopCoroutine(CanvasFadeOut(coreGroup, COUNTDOWN_FADE_TIME));
            StartCoroutine(CanvasFadeIn(coreGroup, COUNTDOWN_FADE_TIME));
        }
        else if (!furnace.countdownActive && showingCountdown)
        {
            StopCoroutine(CanvasFadeIn(coreGroup, COUNTDOWN_FADE_TIME));
            StartCoroutine(CanvasFadeOut(coreGroup, COUNTDOWN_FADE_TIME));
        }

        coreTimeValue = furnace.GetCurrentFuel();
        ChangeValueInHUD(coreBar, coreTimeValue, coreTimeValue.ToString());
    }

    private string CheckTextForZeros(string text)
    {
        string zero = "0";

        if (text.Length < 2)
        {
            text = zero + text;
        }

        return text;
    }

    private void ChangeValueInHUD(HUDBar bar, int value, string text)
    {
        bar.SetValue(value);
        bar.UpdateText(CheckTextForZeros(text));
    }

    public void DoDeathImageFade()
    {
        StartCoroutine(CanvasFadeIn(deathImageGroup, DEATH_FADE_TIME));
    }

    public void DoFadeToBlack()
    {
        StartCoroutine(CanvasFadeIn(fadeOutGroup, FADE_TIME));
    }

    public void RestoreFades()
    {
        StopCoroutine(CanvasFadeIn(deathImageGroup, DEATH_FADE_TIME));
        StopCoroutine(CanvasFadeIn(fadeOutGroup, FADE_TIME));

        deathImageGroup.alpha = 0f;
        fadeOutGroup.alpha = 0f;
    }

    public void ShowRecieveDamageFades()
    {
        StartCoroutine(RecieveDamageFadeInAndOut());
    }

    IEnumerator CanvasFadeOut(CanvasGroup canvasGroup, float fadeTime)
    {
        Vector2 startVector = new Vector2(1f, 1f);
        Vector2 endVector = new Vector2(0f, 0f);

        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeTime;

            canvasGroup.alpha = Vector2.Lerp(startVector, endVector, normalizedTime).x;
            yield return null;
        }
        canvasGroup.alpha = endVector.x;

        showingCountdown = false;
    }

    IEnumerator CanvasFadeIn(CanvasGroup canvasGroup, float fadeTime)
    {
        Vector2 startVector = new Vector2(0f, 0f);
        Vector2 endVector = new Vector2(1f, 1f);

        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeTime;

            canvasGroup.alpha = Vector2.Lerp(startVector, endVector, normalizedTime).x;
            yield return null;
        }
        canvasGroup.alpha = endVector.x;

        showingCountdown = true;
    }

    IEnumerator RecieveDamageFadeInAndOut()
    {
        Vector2 fadeInStartVector = new Vector2(0f, 0f);
        Vector2 fadeInEndVector = new Vector2(1f, 1f);

        // Fade in
        for (float t = 0f; t < DAMAGE_FADE_TIME; t += Time.deltaTime)
        {
            float normalizedTime = t / DAMAGE_FADE_TIME;

            recieveDamageGroup.alpha = Vector2.Lerp(fadeInStartVector, fadeInEndVector, normalizedTime).x;
            yield return null;
        }
        recieveDamageGroup.alpha = fadeInEndVector.x;

        // Fade out
        for (float t = 0f; t < DAMAGE_FADE_TIME; t += Time.deltaTime)
        {
            float normalizedTime = t / DAMAGE_FADE_TIME;

            recieveDamageGroup.alpha = Vector2.Lerp(fadeInEndVector, fadeInStartVector, normalizedTime).x;
            yield return null;
        }
        recieveDamageGroup.alpha = fadeInStartVector.x;
    }
}
