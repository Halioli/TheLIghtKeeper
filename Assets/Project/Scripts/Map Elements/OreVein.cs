using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OreVein : InteractStation
{
    // Public Attributes
    public Item materialVein;
    public Item autoMiner;
    public GameObject interactText;
    public TextMeshProUGUI mssgText;

    // Private Attributes
    private string[] messagesToShow = { "", "No auto-miner found", "Auto-miner placed" };

    void Update()
    {
        if (playerInsideTriggerArea)
        {
            GetInput();
            PopUpAppears();
        }
        else
        {
            PopUpDisappears();
        }
    }

    // Interactive pop up disappears
    private void PopUpAppears()
    {
        interactText.SetActive(true);
    }

    // Interactive pop up disappears
    private void PopUpDisappears()
    {
        interactText.SetActive(false);
        mssgText.text = messagesToShow[0];
    }
}
