using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctionality : MonoBehaviour
{
    public Button buttonTemplate;

    private List<Button> buttons = new List<Button>();
    private int index = 0;

    private void Start()
    {
        //buttonTemplate.onClick.AddListener(() => PrintNumber(10));
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            CreateButton();
        }
    }


    public void PrintNumber(int number)
    {
        Debug.Log(number);
    }


    private void CreateButton()
    {
        buttons.Add(Instantiate(buttonTemplate, transform));
        index++;

        for (int i = 0; i < index; i++)
        {
            if (i == index - 1)
                buttons[i].onClick.AddListener(() => PrintNumber(i - 1));
        }
    }
}
