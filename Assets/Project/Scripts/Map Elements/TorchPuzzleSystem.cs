using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPuzzleSystem : MonoBehaviour
{
    public Torch[] linkedTorches;

    private int numberOfTorches;

    public int maxTorchesOff;
    public int maxTorchesOn;

    public int torchesOn;
    public int torchesOff;
    // Start is called before the first frame update
    void Start()
    {
        numberOfTorches = linkedTorches.Length;
        Debug.Log(numberOfTorches);
        TorchesChecker();
        Debug.Log("MAX ON: " + maxTorchesOn);
        Debug.Log("MAX OFF: " + maxTorchesOff);
    }
    private void TorchesChecker()
    {
        foreach (Torch torch in linkedTorches)
        {
            if (torch.hasToBurn)
            {
                maxTorchesOn += 1;
            }
            else
            {
                maxTorchesOff += 1;
            }
        }
    }

    
}
