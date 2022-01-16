using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverButton : MonoBehaviour
{
    public delegate void TriggerButtonDescriptionAction();
    public static event TriggerButtonDescriptionAction OnDescriptionOpen;
    public static event TriggerButtonDescriptionAction OnDescriptionClose;

    public delegate void ButtonDescriptionAction(string description);
    public static event ButtonDescriptionAction OnDescriptionSet;

    private string description;


    private void OnEnable()
    {
        InteractStation.OnInteractClose += DoBlankDescriptionTextAction;
    }

    private void OnDisable()
    {
        InteractStation.OnInteractClose -= DoBlankDescriptionTextAction;
    }


    public void SetDescription(string description)
    {
        this.description = description;
    }

    public virtual void DoDescriptionTextAction()
    {
        if (OnDescriptionSet != null)
            OnDescriptionSet(description);
        if (OnDescriptionOpen != null)
            OnDescriptionOpen();
    }

    public void DoBlankDescriptionTextAction()
    {
        if (OnDescriptionSet != null)
            OnDescriptionSet("");
        if (OnDescriptionClose != null)
            OnDescriptionClose();
    }

}
