using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBox : MonoBehaviour
{
    private static float FADE_IN_TIME = 0.3f;
    private static float FADE_OUT_TIME = 1.0f;
    private static float WAIT_TIME = 2.0f;

    private CanvasGroup chatCanvasGroup;
    private bool chatOpen;
    private string[] chatMessages = {
        "Quack",
        "Kcauq",
        "What the fuck is that?!"
    };

    public TextMeshProUGUI mssgText;

    // Start is called before the first frame update
    void Start()
    {
        chatCanvasGroup = GetComponent<CanvasGroup>();
        chatOpen = false;
    }

    private void OnEnable()
    {
        TutorialMessages.OnNewMessage += ShowChatBox;
    }

    private void OnDisable()
    {
        TutorialMessages.OnNewMessage -= ShowChatBox;
    }

    private void ShowChatBox(int mssgID)
    {
        if (!chatOpen)
        {
            // Set canvas group to 1
            StartCoroutine(CanvasFadeIn(chatCanvasGroup, FADE_IN_TIME));
        }

        // Display text
        mssgText.text = chatMessages[mssgID];

        // Set canvas group to 0
        StartCoroutine(CanvasFadeOut(chatCanvasGroup, FADE_OUT_TIME));
    }

    IEnumerator CanvasFadeOut(CanvasGroup canvasGroup, float fadeTime)
    {
        Vector2 startVector = new Vector2(1f, 1f);
        Vector2 endVector = new Vector2(0f, 0f);

        yield return new WaitForSeconds(WAIT_TIME);

        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeTime;

            canvasGroup.alpha = Vector2.Lerp(startVector, endVector, normalizedTime).x;
            yield return null;
        }
        canvasGroup.alpha = endVector.x;

        chatOpen = false;
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

        chatOpen = true;
    }
}
