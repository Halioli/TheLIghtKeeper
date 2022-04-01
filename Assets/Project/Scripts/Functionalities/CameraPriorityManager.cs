using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPriorityManager : MonoBehaviour
{
    [SerializeField] CinemachineBrain cinemachineBrain;
    [SerializeField] int[] vcameraIDs;
    [SerializeField] CameraPriorityTrigger[] cameraPriorityTrigger;

    private int currentVcameraID = 0;


    private void OnEnable()
    {
        ChatBox.OnChatCameraEvent += TriggerCameraPriority;
        ChatBox.OnFinishChatMessage += GiveCamera0Priority;
    }

    private void OnDisable()
    {
        ChatBox.OnChatCameraEvent -= TriggerCameraPriority;
        ChatBox.OnFinishChatMessage -= GiveCamera0Priority;
    }




    private void GiveCamera0Priority()
    {
        cameraPriorityTrigger[currentVcameraID].SetCameraToNoPriority();

        currentVcameraID = 0;
        cameraPriorityTrigger[currentVcameraID].SetCameraToMainPriority();

        StartCoroutine(MakeCameraTransitionTimeFast());
    }

    private void TriggerCameraPriority(int eventID)
    {
        cinemachineBrain.m_DefaultBlend.m_Time = 1;

        cameraPriorityTrigger[currentVcameraID].SetCameraToNoPriority();

        currentVcameraID = eventID;
        cameraPriorityTrigger[currentVcameraID].SetCameraToMainPriority();
    }


    IEnumerator MakeCameraTransitionTimeFast()
    {
        yield return new WaitForSeconds(5f); // wait 5 seconds
        cinemachineBrain.m_DefaultBlend.m_Time = 0.1f;
    }


}
