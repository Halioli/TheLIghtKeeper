using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHUDHandler : MonoBehaviour
{
    // Private Attributes
    private const float FADE_TIME = 2f;

    private float currentFadeTime;
    private int playerHealthValue;
    private int lampTimeValue;
    private int coreTimeValue;
    private CanvasGroup healthGroup;
    private CanvasGroup lampGroup;
    private CanvasGroup quickAccessGroup;

    // Public Attributes
    public HUDBar healthBar;
    public HUDBar lampBar;

    public HUDItem itemRight;
    public HUDItem itemCenter;
    public HUDItem itemLeft;

    public HealthSystem playerHealthSystem;
    public Lamp lamp;

    // Start is called before the first frame update
    private void Start()
    {
        currentFadeTime = 0f;

        // Initalize health variables
        healthGroup = GetComponentsInChildren<CanvasGroup>()[0];
        playerHealthValue = playerHealthSystem.GetMaxHealth();
        healthBar.SetMaxValue(playerHealthValue);
        healthBar.UpdateText(CheckTextForZeros(playerHealthValue.ToString()));

        // Initialize lamp variables
        lampGroup = GetComponentsInChildren<CanvasGroup>()[1];
        lampTimeValue = (int)lamp.GetLampTimeRemaining();
        lampBar.SetMaxValue(lampTimeValue);

        // Initialize quick access variables
        quickAccessGroup = GetComponentsInChildren<CanvasGroup>()[3];
    }

    private void Update()
    {
        playerHealthValue = playerHealthSystem.GetHealth();
        ChangeValueInHUD(healthBar, playerHealthValue, playerHealthValue.ToString());

        lampTimeValue = (int)lamp.GetLampTimeRemaining();
        ChangeValueInHUD(lampBar, lampTimeValue, null);

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (healthGroup.alpha >= 1f)
            {
                StartCoroutine(ChangeCanvasGroupAlphaToZero(healthGroup));
            }
            else
            {
                StartCoroutine(ChangeCanvasGroupAlphaToOne(healthGroup));
            }
        }
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
    }

    IEnumerator ChangeCanvasGroupAlphaToZero(CanvasGroup canvasGroup)
    {
        float duration = 2f;
        Vector2 startVector = new Vector2(1f, 1f);
        Vector2 endVector = new Vector2(0f, 0f);

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;

            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            canvasGroup.alpha = Vector2.Lerp(startVector, endVector, normalizedTime).x;
            yield return null;
        }
        canvasGroup.alpha = endVector.x; //without this, the value will end at something like 0.9992367
    }

    IEnumerator ChangeCanvasGroupAlphaToOne(CanvasGroup canvasGroup)
    {
        float duration = 2f;
        Vector2 startVector = new Vector2(0f, 0f);
        Vector2 endVector = new Vector2(1f, 1f);

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;

            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            canvasGroup.alpha = Vector2.Lerp(startVector, endVector, normalizedTime).x;
            yield return null;
        }
        canvasGroup.alpha = endVector.x; //without this, the value will end at something like 0.9992367
    }
}
