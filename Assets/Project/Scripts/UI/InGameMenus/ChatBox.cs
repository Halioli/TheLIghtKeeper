using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class ChatBox : MonoBehaviour
{
    public bool allTextShown;
    public delegate void ChatNextInput();
    public static event ChatNextInput OnChatNextInput;


    private static float FADE_IN_TIME = 0.1f;
    private static float FADE_OUT_TIME = 0.1f;
    private static float LETTER_DELAY = 0.05f;

    private CanvasGroup chatCanvasGroup;
    private bool chatOpen;
    private string fullMssgText;
    private string currentMssgText = "";
    private List<string> textToShow;
    private int currentTextNumb = 0;

    public TextMeshProUGUI mssgText;
    public GameObject duckFace;
    public Transform buttonTransoform;

    void Start()
    {
        chatCanvasGroup = GetComponent<CanvasGroup>();
        chatOpen = false;
        allTextShown = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space))
        {
            if (OnChatNextInput != null)
                OnChatNextInput();

            buttonTransoform.DOComplete();
            buttonTransoform.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), 0.25f, 3);

            NextText();
        }
    }

    private void OnEnable()
    {
        TutorialMessages.OnNewMessage += ShowChatBox;
        MessageItemToStorage.OnNewMessage += ShowChatBox;
    }

    private void OnDisable()
    {
        TutorialMessages.OnNewMessage -= ShowChatBox;
        MessageItemToStorage.OnNewMessage -= ShowChatBox;
    }

    private void ShowChatBox(string[] mssg)
    {
        //ParseText(mssg);
        textToShow = new List<string>(mssg);

        // Set canvas group to 1
        StartCoroutine("CanvasFadeIn", chatCanvasGroup);

        NextText();
    }

    private void DisplayText()
    {
        // Display text
        StartCoroutine("ShowText");
    }

    private void NextText()
    {
        if (!allTextShown)
        {
            StopCoroutine("ShowText");
            mssgText.text = fullMssgText;
            allTextShown = true;
        }
        else
        {
            if (textToShow.Count > currentTextNumb)
            {
                fullMssgText = textToShow[currentTextNumb];

                DisplayText();

                currentTextNumb++;
            }
            else
            {
                HideChat();
            }
        }
    }

    private void HideChat()
    {
        // Set canvas group to 0
        StartCoroutine("CanvasFadeOut", chatCanvasGroup);
    }

    private void ResetValues()
    {
        textToShow.Clear();
        currentTextNumb = 0;
        TutorialMessages.tutorialOpened = false;
        chatOpen = false;
        allTextShown = false;
    }

    IEnumerator CanvasFadeIn(CanvasGroup canvasGroup)
    {
        Vector2 startVector = new Vector2(0f, 0f);
        Vector2 endVector = new Vector2(1f, 1f);

        for (float t = 0f; t < FADE_IN_TIME; t += Time.deltaTime)
        {
            float normalizedTime = t / FADE_IN_TIME;

            canvasGroup.alpha = Vector2.Lerp(startVector, endVector, normalizedTime).x;
            yield return null;
        }
        canvasGroup.alpha = endVector.x;

        chatOpen = true;
    }

    IEnumerator CanvasFadeOut(CanvasGroup canvasGroup)
    {
        Vector2 startVector = new Vector2(1f, 1f);
        Vector2 endVector = new Vector2(0f, 0f);

        while (!allTextShown)
        {
            yield return null;
        }

        for (float t = 0f; t < FADE_OUT_TIME; t += Time.deltaTime)
        {
            float normalizedTime = t / FADE_OUT_TIME;

            canvasGroup.alpha = Vector2.Lerp(startVector, endVector, normalizedTime).x;
            yield return null;
        }
        canvasGroup.alpha = endVector.x;

        ResetValues();
    }

    IEnumerator ShowText()
    {
        allTextShown = false;
        for (int i = 0; i < fullMssgText.Length + 1; i++)
        {
            currentMssgText = fullMssgText.Substring(0, i);
            mssgText.text = currentMssgText;

            duckFace.transform.DOShakeRotation(LETTER_DELAY, 10, 10, 50);
            
            yield return new WaitForSeconds(LETTER_DELAY);
        }
        allTextShown = true;
    }
}
