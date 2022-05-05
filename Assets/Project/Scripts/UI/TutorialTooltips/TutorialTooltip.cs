using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTooltip : MonoBehaviour
{
    public enum TutorialTooltipType { NONE, MOUSE_LEFT, MOUSE_RIGHT };


    bool isActionDone = false;
    [SerializeField] protected KeyCode action = KeyCode.Mouse0;
    [SerializeField] protected TutorialTooltipType type = TutorialTooltipType.NONE;
    [SerializeField] [TextArea (5,4)] protected string messege;
    [SerializeField] float timeBeforeFade;


    // Action
    public delegate void TutorialTooltipAppearAction(TutorialTooltipType type, string messege);
    public static event TutorialTooltipAppearAction OnPause;

    public delegate void TutorialTooltipHideAction(float timeBeforeFade);
    public static event TutorialTooltipHideAction OnResume;



    private void Update()
    {
        isActionDone = Input.GetKeyDown(action);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(PauseTimeUntilAction());
    }



    IEnumerator PauseTimeUntilAction()
    {
        Pause();

        yield return new WaitUntil(() => isActionDone);

        Resume();
        Destroy(gameObject);
    }



    protected virtual void Pause()
    {
        PlayerInputs.instance.canMove = false;
        PlayerInputs.instance.isLanternPaused = true;

        if (OnPause != null) OnPause(type, messege);
    }

    protected virtual void Resume()
    {
        PlayerInputs.instance.canMove = true;
        PlayerInputs.instance.isLanternPaused = false;

        if (OnResume != null) OnResume(timeBeforeFade);
    }



}
