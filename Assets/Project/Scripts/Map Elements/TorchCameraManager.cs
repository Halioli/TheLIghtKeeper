using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchCameraManager : MonoBehaviour
{
    [SerializeField] GameObject normalCamera;
    [SerializeField] GameObject pilarCamera;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SwapToNormalCamera();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            SwapToPilarCamera();
        }
    }

    private void OnEnable()
    {
        Torch.OnTorchStartActivation += SwapToPilarCamera;
        Torch.OnTorchEndActivation += SwapToNormalCamera;
    }

    private void OnDisable()
    {
        Torch.OnTorchStartActivation -= SwapToPilarCamera;
        Torch.OnTorchEndActivation -= SwapToNormalCamera;
    }


    private void SwapToNormalCamera()
    {
        normalCamera.SetActive(true);
        pilarCamera.SetActive(false);
    }

    private void SwapToPilarCamera()
    {
        pilarCamera.SetActive(true);
        normalCamera.SetActive(false);
    }


}
