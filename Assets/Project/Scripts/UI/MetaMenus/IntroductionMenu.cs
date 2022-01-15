using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroductionMenu : MonoBehaviour
{
    public Image loadingBarImage;
    public CanvasGroup loadingGroup;
    public GameObject previousMenuGameObject;
    
    private MainMenu mainMenu;
    private bool inMainMenu;

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
