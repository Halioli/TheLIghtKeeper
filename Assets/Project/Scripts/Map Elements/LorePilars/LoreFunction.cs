using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreFunction : InteractStation
{
    private Animator lorePilarAnimator;

    public bool activated;

    public PopUp popUp;
    private ResetableFloatingItem floatingItem;
    // Start is called before the first frame update
    void Start()
    {
        lorePilarAnimator = GetComponent<Animator>();
        floatingItem = GetComponent<ResetableFloatingItem>();
        floatingItem.isFloating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInsideTriggerArea)
        {
            lorePilarAnimator.SetBool("_isActivate", true);
            PopUpAppears();
            floatingItem.isFloating = true;
            Debug.Log(floatingItem.isFloating);
        }
        else
        {
            PopUpDisappears();
        }

    }

    public override void StationFunction()
    {
        
    }

    private void PopUpAppears()
    {
        if (!activated)
        {
            popUp.ShowInteraction();
        }

        popUp.ShowMessage();
    }

    // Interactive pop up disappears
    private void PopUpDisappears()
    {
        popUp.HideAll();
    }
}
