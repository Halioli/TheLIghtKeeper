using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeleportSelectionMenu : MonoBehaviour
{
    // Private Attributes
    public TeleportSystem teleportSystem;
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

        for (int i = 0; i < teleportSystem.teleports.Count; ++i)
        {
            GameObject gameObjectButton = Instantiate(buttonPrefab, teleportList.transform);
            teleportButtonsGameObjects.Add(gameObjectButton);

            gameObjectButton.GetComponentInChildren<TextMeshProUGUI>().text = teleportSystem.teleports[i].teleportName;
            gameObjectButton.GetComponentInChildren<TeleportButton>().buttonNumber = i;

            //RectTransform gameObjectButtonRectTransform = gameObjectButton.GetComponent<RectTransform>();
            //teleportListRectTransform.sizeDelta = new Vector2(teleportListRectTransform.sizeDelta.x,
            //    

            if (!teleportSystem.teleports[i].activated)
            {
                gameObjectButton.SetActive(false);
            }
        }
    }


    public void UpdateTeleportSelectionMenu()
    {
        for (int i = 0; i < teleportSystem.teleports.Count; ++i)
        {
            if (teleportSystem.teleports[i].activated)
            {
                teleportButtonsGameObjects[i].SetActive(true);
            }
        }
    }
}
