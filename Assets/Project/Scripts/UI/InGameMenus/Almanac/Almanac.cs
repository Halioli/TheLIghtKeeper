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
    public Image[] itemImages;
    public Image[] scalatorImages;

    private int emptyAnimId = 16;

    public GameObject[] submenus;
    public GameObject environmentMenu;
    public GameObject previousMenuGameObject;
    public Image almanacImage;



    public delegate void AlmanacMenuAction();
    public static event AlmanacMenuAction OnAlmanacMenuExit;



    private void Start()
    {
        SetItemToEmptyID();
        ChangeToEmptyAnimator();
        environmentMenu.SetActive(false);
        //for(int i = 0; i <= almanacScalator.Length; i++)
        //{
        //    scalatorImages[i] = almanacScalator[i].GetComponent<Image>();
        //}
    }

    private void Update()
    {
        CloseAlmanac();

        PlayerInputs.instance.SetInGameMenuOpenInputs();
    }

    public void ShowInfo(AlmanacScriptableObject item)
    {
        if (item.hasBeenFound)
        {
            ShowFoundInfo(item);
        }
        else
        {
            ShowNotFoundInfo(item);
        }
    }


    private void ShowFoundInfo(AlmanacScriptableObject item)
    {
        if (submenus[0].activeInHierarchy)
        {
            SetDiscoveredInfo(item);

            switch (item.ID)
            {
                case 1:
                    ChangeToNormalAnimator();
                    break;
                case 2:
                    ChangeToGeckoAnimator();
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
            if(item.ID == 12)
            {
                ChangeToSpiderAndPlantsAnimator();
                ChangeIdAnimator(item);
            }
            else
            {
                SetItemSpriteImage(item);
                SetDiscoveredInfo(item);
            }
        }
    }

    private void ShowNotFoundInfo(AlmanacScriptableObject item)
    {
        SetUndiscoveredInfo();
        ChangeToEmptyAnimator();
        SetItemToEmptyID();
    }


    private void ChangeToSkullAnimator()
    {
        almanacScalator[0].SetActive(false);
        almanacScalator[1].SetActive(true);
        almanacScalator[2].SetActive(false);
        almanacScalator[3].SetActive(false);
        almanacScalator[4].SetActive(false);
    } 
    
    private void ChangeToEmptyAnimator()
    {
        almanacScalator[0].SetActive(false);
        almanacScalator[1].SetActive(false);
        almanacScalator[2].SetActive(false);
        almanacScalator[3].SetActive(false);
        almanacScalator[4].SetActive(false);
    }

    private void ChangeToNormalAnimator()
    {
        almanacScalator[0].SetActive(false);
        almanacScalator[1].SetActive(false);
        almanacScalator[2].SetActive(true);
        almanacScalator[3].SetActive(false);
        almanacScalator[4].SetActive(false);

    }
    private void ChangeToSpiderAndPlantsAnimator()
    {
        almanacScalator[0].SetActive(true);
        almanacScalator[1].SetActive(false);
        almanacScalator[2].SetActive(false);
        almanacScalator[3].SetActive(false);
        almanacScalator[4].SetActive(false);
    }
    private void ChangeToMaterialsImages()
    {
        almanacScalator[0].SetActive(false);
        almanacScalator[1].SetActive(false);
        almanacScalator[2].SetActive(false);
        almanacScalator[3].SetActive(false);
        almanacScalator[4].SetActive(true);
    }

    private void ChangeToGeckoAnimator()
    {
        almanacScalator[0].SetActive(false);
        almanacScalator[1].SetActive(false);
        almanacScalator[2].SetActive(false);
        almanacScalator[3].SetActive(true);
        almanacScalator[4].SetActive(false);
    }
    private void ChangeIdAnimator(AlmanacScriptableObject item)
    {
        almanacAnimator[0].SetInteger("Id", item.ID);
        almanacAnimator[1].SetInteger("Id", item.ID);
        almanacAnimator[2].SetInteger("Id", item.ID);
        almanacAnimator[3].SetInteger("Id", item.ID);
        //foreach(Image image in scalatorImages)
        //{
        //    //image.sprite = item.sprite;
        //    image.SetNativeSize();
        //}
    }

    private void SetItemToEmptyID()
    {
        almanacAnimator[0].SetInteger("Id", emptyAnimId);
        almanacAnimator[1].SetInteger("Id", emptyAnimId);
        almanacAnimator[2].SetInteger("Id", emptyAnimId);
        almanacAnimator[3].SetInteger("Id", emptyAnimId);
    }

    private void SetUndiscoveredInfo()
    {
        almanacNameText.text = "????";
        almanacTagText.text = "???????";
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
        if(this.gameObject.activeInHierarchy && (PlayerInputs.instance.PlayerPressedInteractExitButton() || PlayerInputs.instance.PlayerPressedAlmanacButton()))
        {
            PressedBackButton();
        }
    }

    public void PressedBackButton()
    {
        if (OnAlmanacMenuExit != null) OnAlmanacMenuExit();

        //previousMenuGameObject.SetActive(true);
        //PlayerInputs.instance.SetInGameMenuCloseInputs();

        gameObject.SetActive(false);

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
