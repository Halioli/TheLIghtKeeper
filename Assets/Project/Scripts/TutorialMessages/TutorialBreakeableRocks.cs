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
        PickaxeUpgrade.OnPickaxeUpgrade += ModifyMessege;
    }

    private void OnDisable()
    {
        DoTutorialBreakeableRocks -= DisableSelf;
        PickaxeUpgrade.OnPickaxeUpgrade -= ModifyMessege;
    }

    protected override void SendMessage()
    {
        base.SendMessage();

        // Send Action
        if (DoTutorialBreakeableRocks != null)
            DoTutorialBreakeableRocks();
    }


    private void ModifyMessege()
    {
        mssgs = new string[1];
        mssgs[0] = "Great, you already upgraded the Pickaxe. Use it to break this heavy rocks.";
    }


}