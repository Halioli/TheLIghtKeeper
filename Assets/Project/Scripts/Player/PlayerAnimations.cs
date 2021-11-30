using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : PlayerBase
{
    //Public Atributes
    public Animator animator;

    //Protected Atributes
    protected bool startMiningAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStates.PlayerActionIsMining())
        {
            animator.SetBool("isMining", true);
        }
        else
        {
            animator.SetBool("isMining", false);
        }
    }

    public void ActivateShake()
    {
        CinemachineShake.Instance.ShakeCamera(5f, 1f);
    }

    public void DesactivateShake()
    {
        CinemachineShake.Instance.ShakeCamera(0f, 1f);
    }

    //+Intense Shake
    //Waiting for the "explotion" animation
    public void DestroyOre()
    {
        //Explote (Shake effects)
        CinemachineShake.Instance.ShakeCamera(10f, 1f);
    }

}
