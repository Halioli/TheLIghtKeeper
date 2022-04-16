using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;



public class TutorialObjectiveReceiver : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI objectiveMessege;


    private void OnEnable()
    {
        TutorialObjective.OnObjectiveStart += SetNewObjective;
        TutorialObjective.OnObjectiveEnd += FinishObjective;

        CoreUpgrade.OnCoreUpgrade += () => Destroy(gameObject);
    }

    private void OnDisable()
    {
        TutorialObjective.OnObjectiveStart -= SetNewObjective;
        TutorialObjective.OnObjectiveEnd -= FinishObjective;

        CoreUpgrade.OnCoreUpgrade -= () => Destroy(gameObject);
    }

    private void Awake()
    {
        canvasGroup.alpha = 0f;
    }


    private void SetNewObjective(string messege)
    {
        canvasGroup.alpha = 1f;
        objectiveMessege.text = messege;

        objectiveMessege.transform.DOPunchScale(new Vector2(0.1f, 0.2f), 5f, 1);
    }

    private void FinishObjective()
    {
        canvasGroup.alpha = 0f;
    }



}
