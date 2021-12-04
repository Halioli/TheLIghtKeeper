using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeleportSelectionMenu : MonoBehaviour
{
    // Private Attributes
    private TeleportSystem teleportSystem;
    private List<GameObject> teleportButtonsGameObjects;
    private RectTransform teleportListRectTransform;

    // Public Attributes
    public GameObject teleportList;
    public GameObject buttonPrefab;

    void Start()
    {
        teleportSystem = GameObject.FindGameObjectWithTag("TeleportSystem").GetComponent<TeleportSystem>();
        teleportButtonsGameObjects = new List<GameObject>();
        teleportListRectTransform = teleportList.GetComponent<RectTransform>();
    }

    void Update()
    {
        
    }

    private void UpdateTeleportSelectionMenu()
    {
        teleportButtonsGameObjects.Clear();
        int buttonNumb = 0;

        foreach(GameObject teleport in teleportSystem.teleports)
        {
            GameObject gameObjectButton = Instantiate(buttonPrefab, teleportList.transform);
            teleportButtonsGameObjects.Add(gameObjectButton);

            gameObjectButton.GetComponentInChildren<TextMeshProUGUI>().text = teleport.name;
            
            ++buttonNumb;
        }
    }
}
