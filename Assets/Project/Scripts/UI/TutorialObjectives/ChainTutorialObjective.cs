using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainTutorialObjective : MonoBehaviour
{
    [SerializeField] GameObject[] tutorialObjectives;
    int objectiveIndex = 0;

    private void Awake()
    {
        InitObjectives();
    }


    private void OnEnable()
    {
        ChatBox.OnChatEvent += ProgressObjective;
    }

    private void OnDisable()
    {
        ChatBox.OnChatEvent -= ProgressObjective;
    }


    private void InitObjectives()
    {
        foreach (GameObject tutorialObjective in tutorialObjectives)
        {
            tutorialObjective.SetActive(false);
        }

        tutorialObjectives[objectiveIndex].SetActive(true);
        tutorialObjectives[objectiveIndex].GetComponent<TutorialObjective>().InvokeOnObjectiveStart();
    }

    private void ProgressObjective()
    {
        tutorialObjectives[objectiveIndex].GetComponent<TutorialObjective>().InvokeOnObjectiveEnd();
        Destroy(tutorialObjectives[objectiveIndex++]);


        if (objectiveIndex < tutorialObjectives.Length) 
            tutorialObjectives[objectiveIndex].GetComponent<TutorialObjective>().InvokeOnObjectiveStart();
    }


}
