using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITesting : MonoBehaviour
{
    // Public
    public GameObject healthBar;
    public GameObject lampBar;
    public GameObject coreBar;

    public int healthVal;
    public int lampVal;
    public int coreVal;

    // Private
    private string healthText;
    private string lampText;
    private string coreText;

    private int maxHealthVal;
    private int maxLampVal;
    private int maxCoreVal;

    private float lampTimeRemaining;
    private float coreTimeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        // Set & check all values
        maxHealthVal = healthVal;
        healthText = healthVal.ToString();
        healthText = CheckText(healthText);
        maxLampVal = lampVal;
        lampText = lampVal.ToString();
        lampText = CheckText(lampText);
        maxCoreVal = coreVal;
        coreText = coreVal.ToString();
        coreText = CheckText(coreText);

        lampTimeRemaining = (float)lampVal;
        coreTimeRemaining = (float)coreVal;

        // Set HUD values
        healthBar.GetComponent<HUDBar>().SetMaxValue(healthVal);
        healthBar.GetComponent<HUDBar>().UpdateText(healthText);

        lampBar.GetComponent<HUDBar>().SetMaxValue(lampVal);
        lampBar.GetComponent<HUDBar>().UpdateText(lampText);

        coreBar.GetComponent<HUDBar>().SetMaxValue(coreVal);
        coreBar.GetComponent<HUDBar>().UpdateText(coreText);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            healthVal -= 1;
            healthText = healthVal.ToString();

            ChangeValue(healthBar, healthVal, healthText);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            healthVal += 1;
            healthText = healthVal.ToString();

            ChangeValue(healthBar, healthVal, healthText);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            healthVal = maxHealthVal;
            healthText = healthVal.ToString();
            ChangeValue(healthBar, healthVal, healthText);

            lampVal = maxLampVal;
            lampText = lampVal.ToString();
            ChangeValue(lampBar, lampVal, lampText);

            coreVal = maxCoreVal;
            coreText = coreVal.ToString();
            ChangeValue(coreBar, coreVal, coreText);
        }

        if (lampVal > 0)
        {
            // Reduce time
            lampTimeRemaining -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(lampTimeRemaining % 60);

            // Update values
            lampText = ((int)lampTimeRemaining).ToString();
            lampVal = (int)lampTimeRemaining;

            ChangeValue(lampBar, lampVal, lampText);
        }
        else
        {
            lampVal = 0;
            ChangeValue(lampBar, lampVal, lampText);
        }

        if (coreVal > 0)
        {
            // Reduce time
            coreTimeRemaining -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(coreTimeRemaining % 60);

            // Update values
            coreText = ((int)coreTimeRemaining).ToString();
            coreVal = (int)coreTimeRemaining;

            ChangeValue(coreBar, coreVal, coreText);
        }
        else
        {
            coreVal = 0;
            ChangeValue(coreBar, coreVal, coreText);
        }
    }

    private string CheckText(string text)
    {
        string zero = "0";

        if (text.Length < 2)
        {
            text = zero + text;
        }

        return text;
    }

    private void ChangeValue(GameObject bar, int value, string text)
    {
        bar.GetComponent<HUDBar>().SetValue(value);
        bar.GetComponent<HUDBar>().UpdateText(CheckText(text));
    }
}
