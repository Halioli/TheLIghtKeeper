using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDHandler : MonoBehaviour
{
    // Private Attributes
    private const float FADE_TIME = 0.5f;
    private int coreTimeValue;
    private bool showingCountdown;
    private CanvasGroup coreGroup;

    // Public Attributes
    public HUDBar coreBar;
    public Furnace furnace;

    private void Start()
    {
        showingCountdown = false;

        // Initialize core variables
        coreGroup = GetComponentInChildren<CanvasGroup>();
        coreTimeValue = furnace.GetMaxFuel();
        coreBar.SetMaxValue(coreTimeValue);
        coreBar.UpdateText(CheckTextForZeros(coreTimeValue.ToString()));
    }

    private void Update()
    {
        if (furnace.countdownActive && !showingCountdown)
        {
            StopCoroutine(CanvasFadeOut(coreGroup));
            StartCoroutine(CanvasFadeIn(coreGroup));
        }
        else if (!furnace.countdownActive && showingCountdown)
        {
            StopCoroutine(CanvasFadeIn(coreGroup));
            StartCoroutine(CanvasFadeOut(coreGroup));
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

    IEnumerator CanvasFadeOut(CanvasGroup canvasGroup)
    {
        Vector2 startVector = new Vector2(1f, 1f);
        Vector2 endVector = new Vector2(0f, 0f);

        for (float t = 0f; t < FADE_TIME; t += Time.deltaTime)
        {
            float normalizedTime = t / FADE_TIME;

            canvasGroup.alpha = Vector2.Lerp(startVector, endVector, normalizedTime).x;
            yield return null;
        }
        canvasGroup.alpha = endVector.x;

        showingCountdown = false;
    }

    IEnumerator CanvasFadeIn(CanvasGroup canvasGroup)
    {
        Vector2 startVector = new Vector2(0f, 0f);
        Vector2 endVector = new Vector2(1f, 1f);

        for (float t = 0f; t < FADE_TIME; t += Time.deltaTime)
        {
            float normalizedTime = t / FADE_TIME;

            canvasGroup.alpha = Vector2.Lerp(startVector, endVector, normalizedTime).x;
            yield return null;
        }
        canvasGroup.alpha = endVector.x;

        showingCountdown = true;
    }
}
