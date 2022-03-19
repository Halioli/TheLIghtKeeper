using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class TutorialObjectiveReceiver : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI objectiveMessege;


    private void OnEnable()
    {
        TutorialObjective.OnObjectiveStart += SetNewObjective;
        TutorialObjective.OnObjectiveEnd += FinishObjective;
    }

    private void OnDisable()
    {
        TutorialObjective.OnObjectiveStart -= SetNewObjective;
        TutorialObjective.OnObjectiveEnd -= FinishObjective;
    }




    private void SetNewObjective(string messege)
    {
        canvasGroup.alpha = 1f;
        objectiveMessege.text = messege;
    }

    private void FinishObjective()
    {
        canvasGroup.alpha = 0f;
    }



}
