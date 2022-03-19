using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObjective : MonoBehaviour
{
    /*
    Part 1
    1. Reach the Spaceship
    2. Bring 6 Coal to the Core

    Part 2
    3. Upgrade the Pickaxe
    4. Bring Luxinite to the Upgrade Station
    */


    [SerializeField] string messege;
    [SerializeField] bool isLast = false;


    public delegate void TutorialObjectiveStartAction(string messege);
    public static event TutorialObjectiveStartAction OnObjectiveStart;

    public delegate void TutorialObjectiveEndAction();
    public static event TutorialObjectiveEndAction OnObjectiveEnd;


    public void InvokeOnObjectiveStart()
    {
        if (OnObjectiveStart != null) OnObjectiveStart(messege);
    }

    public void InvokeOnObjectiveEnd()
    {
        if (OnObjectiveEnd != null) OnObjectiveEnd();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InvokeOnObjectiveStart();
        }
    }


}
