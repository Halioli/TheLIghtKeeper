using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShipStations : TutorialMessages
{
    public delegate void ShipStationsTutorialDone();
    public static event ShipStationsTutorialDone OnShipStationsTutorial;

    [SerializeField] ChatBox chatBox;
    [SerializeField] WorldMark worldMark;
    [SerializeField] Transform healingStationTransform;
    [SerializeField] Transform craftingStationTransform;
    [SerializeField] Transform upgradesStationTransform;
    [SerializeField] Transform storageStationTransform;

    private bool mssgSent = false;

    private void Start()
    {
        worldMark.Disappear();
    }

    private void Update()
    {
        if (mssgSent)
        {
            switch (chatBox.currentTextNumb)
            {
                case 1:
                    worldMark.AppearAtPosition(healingStationTransform.position);
                    break;

                case 2:
                    worldMark.AppearAtPosition(craftingStationTransform.position);
                    break;

                case 3:
                    worldMark.AppearAtPosition(upgradesStationTransform.position);
                    break;

                case 4:
                    worldMark.AppearAtPosition(storageStationTransform.position);
                    break;

                default:
                    worldMark.Disappear();
                    DisableSelf();
                    break;
            }
        }
    }

    private void OnEnable()
    {
        OnShipStationsTutorial += DisableSelf;
    }

    private void OnDisable()
    {
        OnShipStationsTutorial -= DisableSelf;
    }

    protected override void SendMessage()
    {
        mssgSent = true;
        base.SendMessage();

        // Send Action
        if (OnShipStationsTutorial != null)
            OnShipStationsTutorial();
    }
}
