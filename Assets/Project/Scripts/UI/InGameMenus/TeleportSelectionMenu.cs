using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportSelectionMenu : MonoBehaviour
{
    // Private Attributes
    private TeleporterSystem teleportSystem;
    private List<GameObject> teleportButtonsGameObjects;
    private RectTransform teleportListRectTransform;

    // Public Attributes
    public GameObject teleportList;
    public GameObject buttonPrefab;

    void Start()
    {
        teleportSystem = GameObject.FindGameObjectWithTag("Teleporter").GetComponent<TeleporterSystem>();
        teleportButtonsGameObjects = new List<GameObject>();
        teleportListRectTransform = teleportList.GetComponent<RectTransform>();
    }

    void Update()
    {
        
    }

    private void UpdateTeleportSelectionMenu()
    {
        teleportButtonsGameObjects.Clear();

        
    }
}
