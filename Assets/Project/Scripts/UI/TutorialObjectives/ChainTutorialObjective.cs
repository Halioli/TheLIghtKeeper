using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainTutorialObjective : MonoBehaviour
{
    [SerializeField] GameObject[] tutorialObjectives;
    int objectiveIndex = 0;


    private void OnEnable()
    {
        ChatBox.OnChatTutorialEvent += ProgressObjective;
        PickaxeUpgrade.OnPickaxeUpgrade += SkipPickaxeUpgradeObjective;
    }

    private void OnDisable()
    {
        ChatBox.OnChatTutorialEvent -= ProgressObjective;
        PickaxeUpgrade.OnPickaxeUpgrade -= SkipPickaxeUpgradeObjective;
    }



    private void ProgressObjective(int eventID)
    {
        // use eventID

        if (objectiveIndex == 0)
        {
            tutorialObjectives[objectiveIndex++].GetComponent<TutorialObjective>().InvokeOnObjectiveStart();
            return;
        }

        //tutorialObjectives[objectiveIndex].GetComponent<TutorialObjective>().InvokeOnObjectiveEnd();
        //Destroy(tutorialObjectives[objectiveIndex++]);

        if (objectiveIndex < tutorialObjectives.Length) 
            tutorialObjectives[objectiveIndex++].GetComponent<TutorialObjective>().InvokeOnObjectiveStart();
        else
        {
            tutorialObjectives[objectiveIndex - 1].GetComponent<TutorialObjective>().InvokeOnObjectiveEnd();
            Destroy(gameObject);
        }
    }


    private void SkipPickaxeUpgradeObjective()
    {
        if (tutorialObjectives[objectiveIndex].name == "TutorialObjective_UpgradeThePickaxe")
        {
            ++objectiveIndex;
        }
    }


}
