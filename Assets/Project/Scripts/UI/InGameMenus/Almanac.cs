using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Almanac : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI almanacNameText;
    [SerializeField] TextMeshProUGUI almanacTagText;
    [SerializeField] TextMeshProUGUI almanacDescriptionText;
    [SerializeField] GameObject[] almanacScalator;
    [SerializeField] Animator[] almanacAnimator;

    private int emptyAnimId = 16;

    private void Start()
    {
        almanacAnimator[0].SetInteger("Id", emptyAnimId);
        almanacAnimator[1].SetInteger("Id", emptyAnimId);
        almanacScalator[0].SetActive(true);
        almanacScalator[1].SetActive(false);
    }

    public void ShowInfo(AlmanacScriptableObject item)
    {
        if (!item.hasFound)
        {
            almanacNameText.text = "?";
            almanacTagText.text = "?";
            almanacDescriptionText.text = "?";
            almanacScalator[0].SetActive(true);
            almanacScalator[1].SetActive(false);
            almanacAnimator[0].SetInteger("Id", emptyAnimId);
            almanacAnimator[1].SetInteger("Id", emptyAnimId);
        }
        else
        {
            almanacNameText.text = item.name;
            almanacTagText.text = item.tag;
            almanacDescriptionText.text = item.description;
            almanacAnimator[0].SetInteger("Id", item.ID);
            almanacAnimator[1].SetInteger("Id", item.ID);
            if (item.ID == 3)
            {
                almanacScalator[0].SetActive(false);
                almanacScalator[1].SetActive(true);
            }
        }
    }
}
