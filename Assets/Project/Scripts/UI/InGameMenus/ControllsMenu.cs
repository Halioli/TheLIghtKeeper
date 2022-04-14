using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllsMenu : MonoBehaviour
{
    public GameObject previousMenuGameObject;

    public void PressedBackButton()
    {
        previousMenuGameObject.SetActive(true);

        gameObject.SetActive(false);
    }
}
