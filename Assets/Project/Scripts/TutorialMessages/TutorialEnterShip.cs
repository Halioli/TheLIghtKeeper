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
    private int mssgNumb = -1;

    private void Start()
    {
        worldMark.Disappear();
    }

    private void Update()
    {
        if (mssgSent)
        {
            if (Input.GetKeyDown(KeyCode.Space) && chatBox.allTextShown)
            {
                mssgNumb++;
                switch (mssgNumb)
                {
                    case 0:
                        break;

                    case 1:
                        worldMark.AppearAtPosition(healthStationTransform.position);
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

                    case 5:
                        worldMark.AppearAtPosition(furnaceStationTransform.position);
                        break;

                    default:
                        worldMark.Disappear();
                        DisableSelf();
                        break;
                }
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
