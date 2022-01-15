using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InGameHUDHandler : MonoBehaviour
{
    // Private Attributes
    private const float FADE_TIME = 0.5f;
    private const float CLAW_FADE_TIME = 0.2f;
    private const float SHAKE_AMOUNT = 0.1f;
    private const float CLAW_SHAKE_STRENGHT = 40f;
    private const float SHAKE_STRENGHT = 0.5f;

    private int playerHealthValue;
    private int lampTimeValue;
    private bool playerIsDamaged;
    private bool healthIsTrembeling;
    private bool lampIsOn;
    private bool lampIsTrembeling;
    private CanvasGroup healthGroup;
    private CanvasGroup lampGroup;
    private CanvasGroup clawStrikeGroup;
    private CanvasGroup exclamationGroup;
    private CanvasGroup crossGroup;

    // Public Attributes
    public HUDBar healthBar;
    public HUDBar lampBar;

    public GameObject healthBarGameObject;
    public GameObject lampBarGameObject;
    public GameObject clawStrikeGameObject;
    public GameObject exclamationGameObject;
    public GameObject crossGameObject;

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
        healthIsTrembeling = false;
        
        // Initialize lamp variables
        lampGroup = GetComponentsInChildren<CanvasGroup>()[1];
        UpdateMaxLampValue();
        lampTimeValue = (int)lamp.GetMaxLampTime();
        lampBar.SetMaxValue(lampTimeValue);
        lampIsOn = false;
        lampIsTrembeling = false;

        // Initialize claw variables
        clawStrikeGroup = clawStrikeGameObject.GetComponent<CanvasGroup>();

        // Initialize exclamation variables
        exclamationGroup = exclamationGameObject.GetComponent<CanvasGroup>();

        // Initialize cross variables
        crossGroup = crossGameObject.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        playerHealthValue = playerHealthSystem.GetHealth();
        ChangeValueInHUD(healthBar, playerHealthValue, playerHealthValue.ToString());

        UpdateMaxLampValue();
        lampTimeValue = (int)lamp.GetLampTimeRemaining();
        ChangeValueInHUD(lampBar, lampTimeValue, null);

        // Check if any element needs to appear/disappear
        ManageShowingHealth();
        ManageShowingLampFuel();

        if (playerIsDamaged)
        {
            if (playerHealthSystem.GetHealth() <= 5 && !healthIsTrembeling)
            {
                // Tremble
                healthIsTrembeling = true;
                StartCoroutine(ShakeHealthGameObject());
            }
        }

        if (lampIsOn)
        {
            if (lamp.GetLampTimeRemaining() <= 5f && !lampIsTrembeling)
            {
                // Tremble
                lampIsTrembeling = true;
                StartCoroutine(ShakeLampGameObject());
            }
        }
    }

    private void UpdateMaxLampValue()
    {
        lampBar.SetMaxValue((int)lamp.GetMaxLampTime());
    }

    private void ManageShowingHealth()
    {
        if ((playerHealthSystem.GetHealth() < playerHealthSystem.GetMaxHealth()) && !playerIsDamaged)
        {
            StopCoroutine(CanvasFadeOut(healthGroup));
            StartCoroutine(CanvasFadeIn(healthGroup));
            playerIsDamaged = true;
        }
        else if (!(playerHealthSystem.GetHealth() < playerHealthSystem.GetMaxHealth()) && playerIsDamaged)
        {
            StopCoroutine(CanvasFadeIn(healthGroup));
            StartCoroutine(CanvasFadeOut(healthGroup));
            playerIsDamaged = false;
            healthIsTrembeling = false;
        }
    }

    private void ManageShowingLampFuel()
    {
        if (lamp.turnedOn && !lampIsOn)
        {
            StopCoroutine(CanvasFadeOut(lampGroup));
            StartCoroutine(CanvasFadeIn(lampGroup));
            lampIsOn = true;
        }
        else if (!lamp.turnedOn && lampIsOn)
        {
            StopCoroutine(CanvasFadeIn(lampGroup));
            StartCoroutine(CanvasFadeOut(lampGroup));
            lampIsOn = false;
            lampIsTrembeling = false;
        }
    }

    private void ChangeValueInHUD(HUDBar bar, int value, string text)
    {
        bar.SetValue(value);
    }

    public void DoRecieveDamageFadeAndShake()
    {
        StartCoroutine(RecieveDamageFadeAndShake());
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
    }

    IEnumerator ShakeLampGameObject()
    {
        Vector2 startingPos = lampBarGameObject.transform.localPosition;
        Vector2 currentPos = startingPos;

        while (lampIsTrembeling)
        {
            currentPos.x += Random.Range(-SHAKE_AMOUNT, SHAKE_AMOUNT);
            currentPos.y += Random.Range(-SHAKE_AMOUNT, SHAKE_AMOUNT);
            lampBarGameObject.transform.localPosition = currentPos;

            yield return null;
            lampBarGameObject.transform.localPosition = startingPos;
            currentPos = startingPos;
        }

        yield return new WaitWhile(() => lampIsTrembeling);
        lampBarGameObject.transform.localPosition = startingPos;
    }

    IEnumerator ShakeHealthGameObject()
    {
        Vector2 startingPos = healthBarGameObject.transform.localPosition;
        Vector2 currentPos = startingPos;

        while (healthIsTrembeling)
        {
            currentPos.x += Random.Range(-SHAKE_AMOUNT, SHAKE_AMOUNT);
            currentPos.y += Random.Range(-SHAKE_AMOUNT, SHAKE_AMOUNT);
            healthBarGameObject.transform.localPosition = currentPos;

            yield return null;
            healthBarGameObject.transform.localPosition = startingPos;
            currentPos = startingPos;
        }

        yield return new WaitWhile(() => healthIsTrembeling);
        healthBarGameObject.transform.localPosition = startingPos;
    }

    IEnumerator RecieveDamageFadeAndShake()
    {
        Vector2 fadeInStartVector = new Vector2(0f, 0f);
        Vector2 fadeInEndVector = new Vector2(1f, 1f);

        // Fade in
        for (float t = 0f; t < CLAW_FADE_TIME; t += Time.deltaTime)
        {
            float normalizedTime = t / CLAW_FADE_TIME;

            clawStrikeGroup.alpha = Vector2.Lerp(fadeInStartVector, fadeInEndVector, normalizedTime).x;
            yield return null;
        }
        clawStrikeGroup.alpha = fadeInEndVector.x;

        // Shake
        clawStrikeGameObject.transform.DOShakeRotation(1f, CLAW_SHAKE_STRENGHT);
        yield return new WaitForSeconds(0.5f);

        // Fade out
        for (float t = 0f; t < CLAW_FADE_TIME; t += Time.deltaTime)
        {
            float normalizedTime = t / CLAW_FADE_TIME;

            clawStrikeGroup.alpha = Vector2.Lerp(fadeInEndVector, fadeInStartVector, normalizedTime).x;
            yield return null;
        }
        clawStrikeGroup.alpha = fadeInStartVector.x;
    }

    private void OnEnable()
    {
        PlayerMiner.playerSucceessfulMineEvent += ExclamationAppears;
        PlayerMiner.playerFailMineEvent += CrossAppears;
    }

    private void OnDisable()
    {
        PlayerMiner.playerSucceessfulMineEvent -= ExclamationAppears;
        PlayerMiner.playerFailMineEvent -= CrossAppears;
    }

    private void ExclamationAppears()
    {
        StartCoroutine(StartExclamationAppears());
    }

    IEnumerator StartExclamationAppears()
    {
        exclamationGroup.alpha = 1f;

        exclamationGameObject.transform.DOPunchPosition(new Vector2(0f, SHAKE_STRENGHT), FADE_TIME);
        yield return new WaitForSeconds(FADE_TIME);

        exclamationGroup.alpha = 0f;
    }

    private void CrossAppears()
    {
        StartCoroutine(StartCrossAppears());
    }

    IEnumerator StartCrossAppears()
    {
        crossGroup.alpha = 1f;

        crossGameObject.transform.DOPunchPosition(new Vector2(SHAKE_STRENGHT, 0f), FADE_TIME);
        yield return new WaitForSeconds(FADE_TIME);

        crossGroup.alpha = 0f;
    }
}
