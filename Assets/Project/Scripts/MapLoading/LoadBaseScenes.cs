using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBaseScenes : MonoBehaviour
{
    public delegate void KeepBlackFadeAction();
    public static event KeepBlackFadeAction OnKeepBlackFade;

    public delegate void FadeToNormalAction();
    public static event FadeToNormalAction OnFadeToNormal;

    private bool spaceshipLoaded = false;
    private bool mapElementsLoaded = false;

    public string spaceshipScene = "Spaceship";
    public string mapElementsScene = "MapElements";

    void Start()
    {
        // Check if scene is already open
        if (SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == spaceshipScene)
                {
                    spaceshipLoaded = true;
                }
                else if (scene.name == mapElementsScene)
                {
                    mapElementsLoaded = true;
                }
            }
        }

        // Additive load the scenes if necessary
        if (!spaceshipLoaded)
        {
            LoadSpaceshipScene();
        }

        if (!mapElementsLoaded)
        {
            LoadMapElementsScene();
        }
    }

    private void LoadSpaceshipScene()
    {
        if (OnKeepBlackFade != null)
            OnKeepBlackFade();

        SceneManager.LoadSceneAsync(spaceshipScene, LoadSceneMode.Additive);
        spaceshipLoaded = true;

        if (OnFadeToNormal != null)
            OnFadeToNormal();
    }

    private void LoadMapElementsScene()
    {
        if (OnKeepBlackFade != null)
            OnKeepBlackFade();

        SceneManager.LoadSceneAsync(mapElementsScene, LoadSceneMode.Additive);
        mapElementsLoaded = true;

        if (OnFadeToNormal != null)
            OnFadeToNormal();
    }
}
