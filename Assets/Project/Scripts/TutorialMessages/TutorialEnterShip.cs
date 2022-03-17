using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnterShip : TutorialMessages
{
    public delegate void EnterShipTutorialDone();
    public static event EnterShipTutorialDone OnEnterShipTutorial;

    [SerializeField] ChatBox chatBox;
    [SerializeField] WorldMark worldMark;
    [SerializeField] Transform healthStationTransform;
    [SerializeField] Transform craftingStationTransform;
    [SerializeField] Transform upgradesStationTransform;
    [SerializeField] Transform storageStationTransform;
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch (chatBox.currentTextNumb)
                {
                    case 0:
                    case 1:
                    case 2:
                        break;

                    case 3:
                        worldMark.AppearAtPosition(healthStationTransform.position);
                        break;

                    case 4:
                        worldMark.AppearAtPosition(craftingStationTransform.position);
                        break;

                    case 5:
                        worldMark.AppearAtPosition(upgradesStationTransform.position);
                        break;

                    case 6:
                        worldMark.AppearAtPosition(storageStationTransform.position);
                        break;

                    case 7:
                        worldMark.AppearAtPosition(furnaceStationTransform.position);
                        break;

                    default:
                        worldMark.Disappear();
                        DisableSelf();
                        break;
                }
            }
            if (chatBox.allTextShown)
            {
                worldMark.Disappear();
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
        mssgSent = true;
        base.SendMessage();

        // Send Action
        if (OnEnterShipTutorial != null)
            OnEnterShipTutorial();
    }
}
