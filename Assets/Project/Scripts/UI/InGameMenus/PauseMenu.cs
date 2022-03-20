using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public Image loadingBarImage;
    public CanvasGroup loadingGroup;
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    void Update()
    {
        if (PlayerInputs.instance.PlayerPressedPauseButton())
        {
            if (gameIsPaused)
            {
                Resume();
            } 
            else
            {
                Pause();
            }
        }

        if (gameIsPaused)
        {
            PauseGame();
        }
        else
        {
            Resume();
        }
    }

    private void Resume()
    {
        pauseMenu.SetActive(false);

        if (optionsMenu.activeInHierarchy)
            optionsMenu.SetActive(false);

        ResumeGame();
        gameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);

        PauseGame();
        gameIsPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    static public void PauseMineAndAttack()
    {
        PlayerInputs.instance.canMine = false;
        PlayerInputs.instance.canAttack = false;
    }

    static public void ResumeMineAndAttack()
    {
        PlayerInputs.instance.canMine = true;
        PlayerInputs.instance.canAttack = true;
    }

    public void ClickedResumeButton()
    {
        Resume();
    }

    public void ClickedOptionsButton()
    {
        optionsMenu.SetActive(true);

        pauseMenu.SetActive(false);
    }

    public void ClickedMainMenuButton(int sceneIndex)
    {
        loadingGroup.alpha = 1f;
        StartCoroutine(AsyncLoading(sceneIndex));
    }

    public void ClickedExitButton()
    {
        PlayerInputs.instance.QuitGame();
    }

    IEnumerator AsyncLoading(int sceneIndex)
    {
        // LoadSceneMode.Single unloads current scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingBarImage.fillAmount = progress;
            yield return null;
        }
    }
}
