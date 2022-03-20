using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainTutorialObjective : MonoBehaviour
{
    [SerializeField] GameObject[] tutorialObjectives;
    int objectiveIndex = 0;


    private void OnEnable()
    {
        ChatBox.OnChatEvent += ProgressObjective;
        PickaxeUpgrade.OnPickaxeUpgrade += SkipPickaxeUpgradeObjective;
    }

    private void OnDisable()
    {
        ChatBox.OnChatEvent -= ProgressObjective;
        PickaxeUpgrade.OnPickaxeUpgrade -= SkipPickaxeUpgradeObjective;
    }



    private void ProgressObjective()
    {
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
            tutorialObjectives[objectiveIndex++].GetComponent<TutorialObjective>().InvokeOnObjectiveEnd();
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
