using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBaseScenes : MonoBehaviour
{
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
        SceneManager.LoadSceneAsync(spaceshipScene, LoadSceneMode.Additive);
        spaceshipLoaded = true;
    }

    private void LoadMapElementsScene()
    {
        SceneManager.LoadSceneAsync(mapElementsScene, LoadSceneMode.Additive);
        mapElementsLoaded = true;
    }
}
