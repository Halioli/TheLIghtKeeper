using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPlant : InteractStation
{
    public GameObject interactText;

    // Start is called before the first frame update
    void Start()
    {
        interactText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInsideTriggerArea)
        {
            interactText.SetActive(true);
            GetInput();
        }
        else
        {
            interactText.SetActive(false);
        }
    }

  

}
