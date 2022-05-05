using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



public class CraftableItemButton : HoverButton
{
    float clickCooldownDuration = 0.25f;
    bool isClicked = false;


    public int buttonNumber = 0;
    [SerializeField] Image itemImage;

    public delegate void ClickedRecepieButtonAction(int number);
    public static event ClickedRecepieButtonAction OnClickedRecepieButton;

    public static event ClickedRecepieButtonAction OnHoverRecepieButton;

    public delegate void ExitRecepieButtonAction();
    public static event ExitRecepieButtonAction OnRecepieButtonHoverExit;


    private void Awake()
    {
        itemImage = GetComponentsInChildren<Image>()[1];
    }


    public void OnClick()
    {
        //if (isClicked) return;

        //StartCoroutine(OnClickCooldown());

        transform.DOComplete();
        transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), clickCooldownDuration);

        if (OnClickedRecepieButton != null) OnClickedRecepieButton(buttonNumber);
    }

    public void DoOnHoverRecepieButton()
    {
        if (OnHoverRecepieButton != null) OnHoverRecepieButton(buttonNumber);
    }

    public void DoOnHoverExitRecepieButton()
    {
        if (OnRecepieButtonHoverExit != null) OnRecepieButtonHoverExit();
    }

    public void Init(int buttonNumber, int itemID)
    {
        this.buttonNumber = buttonNumber;
        itemImage.sprite = ItemLibrary.instance.GetItem(itemID).sprite; 
    }


    IEnumerator OnClickCooldown()
    {
        isClicked = true;
        transform.DOComplete();
        transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), clickCooldownDuration);

        yield return new WaitForSeconds(clickCooldownDuration);

        isClicked = false;
    }

}
