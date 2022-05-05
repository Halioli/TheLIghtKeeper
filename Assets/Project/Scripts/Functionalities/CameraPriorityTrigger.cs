using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class CameraPriorityTrigger : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcamera;
    [SerializeField] int mainPriority = 20;
    [SerializeField] int noPriority = 9;



    public void SetCameraToMainPriority()
    {
        vcamera.Priority = mainPriority;
    }


    public void SetCameraToNoPriority()
    {
        vcamera.Priority = noPriority;
    }

}
