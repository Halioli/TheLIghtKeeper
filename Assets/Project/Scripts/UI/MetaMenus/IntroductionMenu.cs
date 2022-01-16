using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroductionMenu : MonoBehaviour
{
    public Image loadingBarImage;
    public CanvasGroup loadingGroup;
    public CanvasGroup introductionGroup;
    public CanvasGroup informationGroup;
    public CanvasGroup controllsGroup;
    public GameObject previousMenuGameObject;

    private const int MAX_PANELS = 2;
    private const int MIN_PANELS = 0;

    private MainMenu mainMenu;
    private bool inMainMenu;
    private int currentPanelShowing = MIN_PANELS;

    private void Start()
    {
        if (previousMenuGameObject.GetComponent<MainMenu>() != null)
        {
            mainMenu = previousMenuGameObject.GetComponent<MainMenu>();
            inMainMenu = true;
        }
        else
        {
            inMainMenu = false;
        }
    }

    private void ManageShownPanel()
    {
        switch (currentPanelShowing)
        {
            case 0:
                introductionGroup.alpha = 1f;
                informationGroup.alpha = 0f;
                controllsGroup.alpha = 0f;
                break;

            case 1:
                introductionGroup.alpha = 0f;
                informationGroup.alpha = 1f;
                controllsGroup.alpha = 0f;
                break;

            case 2:
                introductionGroup.alpha = 0f;
                informationGroup.alpha = 0f;
                controllsGroup.alpha = 1f;
                break;

            default:
                break;
        }
    }

    public void PlayButtonClick(int sceneIndex)
    {
        if (inMainMenu)
        {
            loadingGroup.alpha = 1f;
            StartCoroutine(AsyncLoading(sceneIndex));
        }
        else
        {
            ClickedBackButton();
        }
    }

    public void ClickedBackButton()
    {
        previousMenuGameObject.SetActive(true);

        if (inMainMenu)
            mainMenu.ResetRespawns();

        gameObject.SetActive(false);
    }

    public void NextButtonClicked()
    {
        currentPanelShowing++;
        if (currentPanelShowing > MAX_PANELS)
        {
            currentPanelShowing = MIN_PANELS;
        }

        ManageShownPanel();
    }

    public void PreviousButtonClicked()
    {
        currentPanelShowing--;
        if (currentPanelShowing < MIN_PANELS)
        {
            currentPanelShowing = MAX_PANELS;
        }

        ManageShownPanel();
    }

    IEnumerator AsyncLoading(int sceneIndex)
    {
        // LoadSceneMode.Single unloads current scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);

        float progress;
        while (!operation.isDone)
        {
            progress = Mathf.Clamp01(operation.progress / .9f);

            loadingBarImage.fillAmount = progress;
            yield return null;
        }
    }
}
