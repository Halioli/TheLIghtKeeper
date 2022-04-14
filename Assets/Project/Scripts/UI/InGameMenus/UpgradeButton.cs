using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeButton : HoverButton
{
    int upgradeBranchIndex;
    int upgradeIndex;
    protected bool isCompleted = false;
    [SerializeField] Upgrade upgrade;
    [SerializeField] GameObject activeNodeImage;
    [SerializeField] GameObject doneText;
    [SerializeField] Image iconImage;
    [SerializeField] ResetableFloatingItem floatingItem;

    UpgradeMenuCanvas upgradeMenuCanvas;




    public void Init(bool isEnabled, int upgradeBranchIndex, int upgradeIndex, UpgradeMenuCanvas upgradeMenuCanvas)
    {
        if (isEnabled) EnableButton();
        else DisableButton();

        this.upgradeBranchIndex = upgradeBranchIndex;
        this.upgradeIndex = upgradeIndex;
        this.upgradeMenuCanvas = upgradeMenuCanvas;

        doneText.SetActive(false);
    }



    private void ClickedAnimation()
    {
        transform.DOComplete();
        transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), 0.25f, 3);
    }


    public void DisplayUpgrade() // called on hover enter
    {
        upgradeMenuCanvas.DisplayUpgrade(upgrade, isCompleted);
    }

    public void HideDisplay() // called on hover exit
    {
        upgradeMenuCanvas.HideDisplay();
    }

    public virtual void UpgradeSelected() // called on click
    {
        isCompleted = upgradeMenuCanvas.UpgradeSelected(upgradeBranchIndex, upgradeIndex);

        if (isCompleted) DisplayUpgrade();

        ClickedAnimation();
    }



    public void DisableButton()
    {
        GetComponent<Button>().enabled = false;
        GetComponent<Button>().interactable = false;

        activeNodeImage.SetActive(false);

        floatingItem.StopFloating();
    }

    public void EnableButton()
    {
        GetComponent<Button>().enabled = true;
        GetComponent<Button>().interactable = true;

        activeNodeImage.SetActive(true);

        iconImage.color = new Color(255, 255, 255, 255);

        floatingItem.StartFloating();
    }


    public void SetDone()
    {
        GetComponent<Button>().enabled = false;

        doneText.SetActive(true);
        floatingItem.StopFloating();

        isCompleted = true;
    }


}
