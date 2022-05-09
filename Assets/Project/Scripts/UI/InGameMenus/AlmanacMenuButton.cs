using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlmanacMenuButton : HoverMovableButton
{
    [SerializeField] Button button;
    [SerializeField] GameObject attention;
    [SerializeField] ResetableFloatingItem attentionResetableFloatingItem;

    public delegate void AlmanacMenuButtonAction();
    public static event AlmanacMenuButtonAction OnAlmanacMenuEnter;



    private void Start()
    {
        DisableAttention();
    }

    private void Update()
    {
        if (PlayerInputs.instance.PlayerPressedAlmanacButton())
        {
            OpenAlmanacMenu();
        }
    }


    private void OnEnable()
    {
        AlmanacEnvironmentChecker.OnNewItemFound += SetButtonWithAttention;
        AlmanacMaterialsChecker.OnNewItemFound += SetButtonWithAttention;

        Almanac.OnAlmanacMenuExit += EnableButton;
        Almanac.OnAlmanacMenuExit += ResetInputs;
    }

    private void OnDisable()
    {
        AlmanacEnvironmentChecker.OnNewItemFound -= SetButtonWithAttention;
        AlmanacMaterialsChecker.OnNewItemFound -= SetButtonWithAttention;

        Almanac.OnAlmanacMenuExit -= EnableButton;
        Almanac.OnAlmanacMenuExit -= ResetInputs;
    }


    private void SetButtonWithAttention()
    {
        //MoveToEndPosition();
        EnableAttention();
    }


    private void EnableAttention()
    {
        attentionResetableFloatingItem.StartFloating();
        attention.SetActive(true);
    }

    public void DisableAttention()
    {
        attentionResetableFloatingItem.StopFloating();
        attention.SetActive(false);
    }



    public void OpenAlmanacMenu() // also called on click
    {
        if (OnAlmanacMenuEnter != null) OnAlmanacMenuEnter();

        DisableButton();
    }


    public void EnableButton()
    {
        button.interactable = true;
    }

    public void DisableButton()
    {
        button.interactable = false;
    }

    private void ResetInputs()
    {
        PlayerInputs.instance.SetInGameMenuCloseInputs();
    }
}
