using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;

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
    }

    private void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ClickedResumeButton()
    {
        Resume();
    }

    public void ClickedOptionsButton()
    {

    }

    public void ClickedMainMenuButton()
    {

    }

    public void ClickedExitButton()
    {
        PlayerInputs.instance.QuitGame();
    }
}
