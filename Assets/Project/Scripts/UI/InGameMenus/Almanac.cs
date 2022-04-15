using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Almanac : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI almanacNameText;
    [SerializeField] TextMeshProUGUI almanacTagText;
    [SerializeField] TextMeshProUGUI almanacDescriptionText;
    [SerializeField] GameObject[] almanacScalator;
    [SerializeField] Animator[] almanacAnimator;

    private int emptyAnimId = 16;

    public GameObject[] submenus;
    public GameObject environmentMenu;
    public Image almanacImage;

    private void Start()
    {
        SetItemToEmptyID();
        ChangeToEmptyAnimator();
        environmentMenu.SetActive(false);
    }

    private void Update()
    {
        CloseAlmanac();
    }
    public void ShowInfo(AlmanacScriptableObject item)
    {
        if (!item.hasFound)
        {
            SetUndiscoveredInfo();
            ChangeToEmptyAnimator();
            SetItemToEmptyID();
        }
        else
        {
            if (submenus[0].activeInHierarchy)
            {
                SetDiscoveredInfo(item);

                switch (item.ID)
                {
                    case 1:
                    case 2:
                        ChangeToNormalAnimator();
                        break;
                    case 3:
                        ChangeToSkullAnimator();
                        break;
                    default:
                        ChangeToSpiderAndPlantsAnimator();
                        break;
                }
                ChangeIdAnimator(item);
            }
            else
            {
                SetItemSpriteImage(item);
                SetDiscoveredInfo(item);
            }
           
        }
    }

    private void ChangeToSkullAnimator()
    {
        almanacScalator[0].SetActive(false);
        almanacScalator[1].SetActive(true);
        almanacScalator[2].SetActive(false);
        almanacScalator[3].SetActive(false);
    } 
    
    private void ChangeToEmptyAnimator()
    {
        almanacScalator[0].SetActive(false);
        almanacScalator[1].SetActive(false);
        almanacScalator[2].SetActive(false);
        almanacScalator[3].SetActive(false);
    }

    private void ChangeToNormalAnimator()
    {
        almanacScalator[0].SetActive(false);
        almanacScalator[1].SetActive(false);
        almanacScalator[2].SetActive(true);
        almanacScalator[3].SetActive(false);

    }
    private void ChangeToSpiderAndPlantsAnimator()
    {
        almanacScalator[0].SetActive(true);
        almanacScalator[1].SetActive(false);
        almanacScalator[2].SetActive(false);
        almanacScalator[3].SetActive(false);
    }
    private void ChangeToMaterialsImages()
    {
        almanacScalator[0].SetActive(false);
        almanacScalator[1].SetActive(false);
        almanacScalator[2].SetActive(false);
        almanacScalator[3].SetActive(true);
    }

    private void ChangeIdAnimator(AlmanacScriptableObject item)
    {
        almanacAnimator[0].SetInteger("Id", item.ID);
        almanacAnimator[1].SetInteger("Id", item.ID);
        almanacAnimator[2].SetInteger("Id", item.ID);
    }

    private void SetItemToEmptyID()
    {
        almanacAnimator[0].SetInteger("Id", emptyAnimId);
        almanacAnimator[1].SetInteger("Id", emptyAnimId);
        almanacAnimator[2].SetInteger("Id", emptyAnimId);
    }

    private void SetUndiscoveredInfo()
    {
        almanacNameText.text = "?";
        almanacTagText.text = "?";
        almanacDescriptionText.text = "?";
    }

    private void SetDiscoveredInfo(AlmanacScriptableObject item)
    {
        almanacNameText.text = item.name;
        almanacTagText.text = item.tag;
        almanacDescriptionText.text = item.description;
        almanacImage.sprite = item.sprite;
    }

    private void SetItemSpriteImage(AlmanacScriptableObject item)
    {
        ChangeToMaterialsImages();
    }

    public void OpenAlmanac()
    {
        this.gameObject.SetActive(true);
    }

    private void CloseAlmanac()
    {
        if(this.gameObject.activeInHierarchy && PlayerInputs.instance.PlayerPressedPauseButton())
        {
            this.gameObject.SetActive(false);
        }
    }
    //public void SubmenuMaterialsActive()
    //{
    //    submenus[0].SetActive(false);
    //    submenus[1].SetActive(true);
    //}

    //public void SubmenuEnvironmentActive()
    //{
    //    submenus[0].SetActive(true);
    //    submenus[1].SetActive(false);
    //}
}
