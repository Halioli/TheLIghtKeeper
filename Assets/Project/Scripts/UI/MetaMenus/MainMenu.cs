using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] mainMenuEnemies;
    public Image loadingBarImage;
    public CanvasGroup loadingGroup;
    public GameObject introductionMenuGameObject;
    public GameObject optionsMenuGameObject;
    public GameObject creditsMenuGameObject;

    public void PlayButtonClick(int sceneIndex)
    {
        StopRespawns();
        introductionMenuGameObject.SetActive(true);
        //loadingGroup.alpha = 1f;
        //StartCoroutine(AsyncLoading(sceneIndex));
    }

    public void OptionsButtonClick()
    {
        StopRespawns();
        optionsMenuGameObject.SetActive(true);
    }

    public void CreditsButtonClick()
    {
        StopRespawns();
        creditsMenuGameObject.SetActive(true);
    }

    public void ExitButtonClick()
    {
        PlayerInputs.instance.QuitGame();
    }

    public void StopRespawns()
    {
        for (int i = 0; i < mainMenuEnemies.Length; i++)
        {
            mainMenuEnemies[i].SetActive(false);
        }
    }

    public void ResetRespawns()
    {
        for (int i = 0; i < mainMenuEnemies.Length; i++)
        {
            mainMenuEnemies[i].SetActive(true);
        }
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
