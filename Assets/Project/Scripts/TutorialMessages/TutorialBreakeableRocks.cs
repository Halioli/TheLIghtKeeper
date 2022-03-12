using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBreakeableRocks : TutorialMessages
{
    public delegate void TutorialBreakeableRocksDone();
    public static event TutorialBreakeableRocksDone DoTutorialBreakeableRocks;

    private void OnEnable()
    {
        DoTutorialBreakeableRocks += DisableSelf;
    }

    private void OnDisable()
    {
        DoTutorialBreakeableRocks -= DisableSelf;
    }

    protected override void SendMessage()
    {
        base.SendMessage();

        // Send Action
        if (DoTutorialBreakeableRocks != null)
            DoTutorialBreakeableRocks();
    }
}