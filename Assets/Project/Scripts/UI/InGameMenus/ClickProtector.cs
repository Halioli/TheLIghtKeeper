using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickProtector : MonoBehaviour
{
    
    public void DoOnPointerEnter()
    {
        //if (!PlayerInputs.instance.canPause) return;

        PlayerInputs.instance.canAttack = false;
        PlayerInputs.instance.canMine = false;
    }

    public void DoOnPointerExit()
    {
        if (!PlayerInputs.instance.canPause) return;

        PlayerInputs.instance.canAttack = true;
        PlayerInputs.instance.canMine = true;
    }


}
