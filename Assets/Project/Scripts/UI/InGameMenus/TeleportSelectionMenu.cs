using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSelectionMenu : MonoBehaviour
{
    // Private Attributes
    private List<GameObject> teleportButtonsGameObjects;
    private RectTransform teleportListRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        teleportButtonsGameObjects = new List<GameObject>();
        //teleportListRectTransform = teleportButtonsGameObjects.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
