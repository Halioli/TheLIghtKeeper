using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class ChatBox : MonoBehaviour
{
    private static float FADE_IN_TIME = 0.3f;
    private static float FADE_OUT_TIME = 1.0f;
    private static float WAIT_TIME = 2.0f;
    private static float LETTER_DELAY = 0.05f;
    private static int MAX_TEXT_LENGHT = 71;

    private CanvasGroup chatCanvasGroup;
    private bool chatOpen;
    private bool allTextShown;
    private string fullMssgText;
    private string currentMssgText = "";
    private List<string> textToShow = new List<string>();
    private int currentTextNumb;

    public TextMeshProUGUI mssgText;
    public GameObject duckFace;

    void Start()
    {
        chatCanvasGroup = GetComponent<CanvasGroup>();
        chatOpen = false;
        allTextShown = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
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

    private void ShowChatBox(string mssg)
    {
        ParseText(mssg);

        // Set canvas group to 1
        StartCoroutine("CanvasFadeIn", chatCanvasGroup);

        fullMssgText = textToShow[0];
        DisplayText();
    }

    private void ParseText(string msg)
    {
        string modText;
        int numMssgs;
        int currentPos = 0;
        int nextPos = MAX_TEXT_LENGHT;

        //Split full message into smaller fragments
        if (msg.Length < MAX_TEXT_LENGHT)
        {
            textToShow.Add(msg);
            return;            
        }

        while(currentPos < msg.Length)
        {
            textToShow.Add(msg.Substring(currentPos * 72, Mathf.Min(currentPos * 72 + 72, msg.Length)));
        }
    }

    private void DisplayText()
    {
        // Display text
        StartCoroutine("ShowText");
    }

    private void NextText()
    {
        if (textToShow.Count < currentTextNumb)
        {
            currentTextNumb++;
            fullMssgText = textToShow[currentTextNumb];

            DisplayText();
        }
        else
        {
            HideChat();
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
