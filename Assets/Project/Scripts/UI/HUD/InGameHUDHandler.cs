using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHUDHandler : MonoBehaviour
{
    // Private Attributes
    private const float FADE_TIME = 2f;

    private int playerHealthValue;
    private int lampTimeValue;
    private bool playerIsDamaged;
    private bool lampIsOn;
    private CanvasGroup healthGroup;
    private CanvasGroup lampGroup;

    // Public Attributes
    public HUDBar healthBar;
    public HUDBar lampBar;

    public HealthSystem playerHealthSystem;
    public Lamp lamp;

    // Start is called before the first frame update
    private void Start()
    {
        // Initalize health variables
        healthGroup = GetComponentsInChildren<CanvasGroup>()[0];
        playerHealthValue = playerHealthSystem.GetMaxHealth();
        healthBar.SetMaxValue(playerHealthValue);
        playerIsDamaged = false;

        // Initialize lamp variables
        lampGroup = GetComponentsInChildren<CanvasGroup>()[1];
        lampTimeValue = (int)lamp.GetLampTimeRemaining();
        lampBar.SetMaxValue(lampTimeValue);
        lampIsOn = false;
    }

    private void Update()
    {
        playerHealthValue = playerHealthSystem.GetHealth();
        ChangeValueInHUD(healthBar, playerHealthValue, playerHealthValue.ToString());

        lampTimeValue = (int)lamp.GetLampTimeRemaining();
        ChangeValueInHUD(lampBar, lampTimeValue, null);

        // Check if any element needs to appear/disappear
        ManageShowingHealth();
        ManageShowingLampFuel();
    }

    private void ManageShowingHealth()
    {
        if ((playerHealthSystem.GetHealth() < playerHealthSystem.GetMaxHealth()) && !playerIsDamaged)
        {
            StartCoroutine(ChangeCanvasGroupAlphaToOne(healthGroup));
            playerIsDamaged = true;
        }
        else if (!(playerHealthSystem.GetHealth() < playerHealthSystem.GetMaxHealth()) && playerIsDamaged)
        {
            StartCoroutine(ChangeCanvasGroupAlphaToZero(healthGroup));
            playerIsDamaged = false;
        }
    }

    private void ManageShowingLampFuel()
    {
        if (lamp.turnedOn && !lampIsOn)
        {
            StartCoroutine(ChangeCanvasGroupAlphaToOne(lampGroup));
            lampIsOn = true;
        }
        else if (!lamp.turnedOn && lampIsOn)
        {
            StartCoroutine(ChangeCanvasGroupAlphaToZero(lampGroup));
            lampIsOn = false;
        }
    }

    private void ChangeValueInHUD(HUDBar bar, int value, string text)
    {
        bar.SetValue(value);
    }

    IEnumerator ChangeCanvasGroupAlphaToZero(CanvasGroup canvasGroup)
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
    }

    IEnumerator ChangeCanvasGroupAlphaToOne(CanvasGroup canvasGroup)
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
    }
}
