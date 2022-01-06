using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public GameObject mainMenuGameObject;

    private MainMenu mainMenu;

    private void Start()
    {
        mainMenu = mainMenuGameObject.GetComponent<MainMenu>();
    }

    public void ClickedBackButton()
    {
        mainMenuGameObject.SetActive(true);
        mainMenu.ResetRespawns();

        gameObject.SetActive(false);
    }
}
