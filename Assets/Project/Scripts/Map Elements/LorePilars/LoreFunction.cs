using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreFunction : InteractStation
{
    private Animator lorePilarAnimator;
    private ResetableFloatingItem floatingItem;

    public bool activated;
    public PopUp popUp;

    [SerializeField] AudioSource idlAudioSource;
    [SerializeField] AudioSource activeAudioSource;

    public string tittle;
    [TextArea(5, 20)] public string text;

    public delegate void LorePilarActive();
    public static event LorePilarActive OnLorePilarActive;

    public delegate void LorePilarInteract(string tittle, string text);
    public static event LorePilarInteract OnPilarInteract;

    void Start()
    {
        lorePilarAnimator = GetComponent<Animator>();
        floatingItem = GetComponent<ResetableFloatingItem>();
        floatingItem.isFloating = false;
    }

    void Update()
    {
        if(playerInsideTriggerArea)
        {
            if (!floatingItem.isFloating)
            {
                lorePilarAnimator.SetBool("_isActivate", true);
            }
            else
            {
                GetInput();
                PopUpAppears();
            }
        }
        else
        {
            PopUpDisappears();
        }

    }

    public void LorePilarActivated()
    {
        if (OnLorePilarActive != null)
            OnLorePilarActive();

        floatingItem.isFloating = true;
        idlAudioSource.Play();
    }

    public override void StationFunction()
    {
        if (OnPilarInteract != null) 
            OnPilarInteract(tittle, text);
    }

    public void StartActivateAudio()
    {
        activeAudioSource.Play();
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
