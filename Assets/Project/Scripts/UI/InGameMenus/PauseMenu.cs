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

    public delegate void PauseMenuAction();
    public static event PauseMenuAction OnPaused;

    public delegate void ToggleCheatsAction(bool toggle);
    public static event ToggleCheatsAction OnToggleCheats;

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
                if(OnPaused != null) OnPaused();
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
        gameIsPaused = false;
        loadingGroup.alpha = 1f;
        StartCoroutine(AsyncLoading(sceneIndex));
    }

    public void ClickedExitButton()
    {
        PlayerInputs.instance.QuitGame();
    }

    public void ToggleCheats(bool value)
    {
        if (OnToggleCheats != null)
            OnToggleCheats(value);
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
