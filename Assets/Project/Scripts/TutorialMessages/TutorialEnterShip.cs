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

    private void Start()
    {
        worldMark.Disappear();
    }

    private void Update()
    {
        if (mssgSent)
        {
            worldMark.AppearAtPosition(furnaceStationTransform.position);
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
        mssgSent = true;
        base.SendMessage();

        // Send Action
        if (OnEnterShipTutorial != null)
            OnEnterShipTutorial();
    }
}
