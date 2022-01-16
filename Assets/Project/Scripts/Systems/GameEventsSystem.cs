using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsSystem : MonoBehaviour
{
    // Public Attributes
    public Furnace furnace;

    // Private Attributes
    private const int MAX_FURNACE_EVENTS = 3;

    private bool eventInAction = false;
    private bool randomEventEnumeratorActive = false;

    void Update()
    {
        if (furnace.GetLightLevel() >= 1)
        {
            if (!randomEventEnumeratorActive && !eventInAction)
            {
                StartCoroutine(CreateRandomEvent());
            }
            else if (eventInAction)
            {
                if (furnace.GetCurrentEventID() == 0)
                {
                    eventInAction = false;
                }
            }
        }
    }

    IEnumerator CreateRandomEvent()
    {
        randomEventEnumeratorActive = true;
        int randomEventID;

        while (!eventInAction)
        {
            yield return new WaitForSeconds(10);

            randomEventID = Random.Range(0, MAX_FURNACE_EVENTS);

            if (randomEventID == 1)// || randomEventID == 2)
            {
                furnace.StartEvent(randomEventID);
                eventInAction = true;
            }
        }

        randomEventEnumeratorActive = false;
    }
}
