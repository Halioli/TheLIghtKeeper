using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnterShip : TutorialMessages
{
    public delegate void EnterShipTutorialDone();
    public static event EnterShipTutorialDone OnEnterShipTutorial;

    [SerializeField] ChatBox chatBox;
    [SerializeField] WorldMark worldMark;
    [SerializeField] Transform furnaceStationTransform;

    private bool mssgSent = false;
    private bool mssgSent1stSent = true;

    private void Update()
    {
        if (mssgSent)
        {

            worldMark.AppearAtPosition(furnaceStationTransform.position);

            if (chatBox.currentTextNum == 2)
            {
                worldMark.gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        OnEnterShipTutorial += DisableSelf;
    }

    private void OnDisable()
    {
        OnEnterShipTutorial -= DisableSelf;
    }

    protected override void SendMessage()
    {
        StartCoroutine(DelayedMessage());
    }

    IEnumerator DelayedMessage()
    {
        yield return new WaitForSeconds(1f);

        mssgSent = true;
        base.SendMessage();

        // Send Action
        if (OnEnterShipTutorial != null)
            OnEnterShipTutorial();
    }
}
