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

            if (!teleportSystem.teleports[i].activated)
            {
                teleportButtonsGameObjects[i].GetComponent<Button>().interactable = false;
                gameObjectButton.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        Teleporter.OnActivation += UpdateTeleportSelectionMenu;

        TeleportButton.OnSelection += DeactivateSelf;
    }

    private void OnDisable()
    {
        Teleporter.OnActivation -= UpdateTeleportSelectionMenu;
        
        TeleportButton.OnSelection -= DeactivateSelf;
    }

    public void UpdateTeleportSelectionMenu(string currentTeleportInUse)
    {
        for (int i = 0; i < teleportSystem.teleports.Count; ++i)
        {
            if (teleportSystem.teleports[i].activated)
            {
                teleportButtonsGameObjects[i].SetActive(true);

                if (i == teleportSystem.currentTeleportInUse)
                {
                    teleportButtonsGameObjects[i].GetComponent<Button>().interactable = false;
                }
                else if (!teleportButtonsGameObjects[i].GetComponent<Button>().interactable)
                {
                    teleportButtonsGameObjects[i].GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    private void DeactivateSelf(int teleportIndex)
    {
        gameObject.SetActive(false);
    }

}
