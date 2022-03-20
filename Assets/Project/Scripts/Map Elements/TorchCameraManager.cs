using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TorchCameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera normalCamera;
    [SerializeField] CinemachineVirtualCamera pilarCamera;


    private void Awake()
    {
        SwapToNormalCamera();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.M))
    //    {
    //        SwapToNormalCamera();
    //    }
    //    else if (Input.GetKeyDown(KeyCode.N))
    //    {
    //        SwapToPilarCamera();
    //    }
    //}

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
        normalCamera.Priority = 11;
    }

    private void SwapToPilarCamera()
    {
        normalCamera.Priority = 9;
    }


}
