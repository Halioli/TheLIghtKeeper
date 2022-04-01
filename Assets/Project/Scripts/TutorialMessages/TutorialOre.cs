using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOre : MonoBehaviour
{
    public PopUp canvasPopUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvasPopUp.ShowInteraction();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvasPopUp.HideInteraction();
        }
    }
}
