using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickProtector : MonoBehaviour
{
    
    public void DoOnPointerEnter()
    {
        PlayerInputs.instance.canAttack = false;
        PlayerInputs.instance.canMine = false;
    }

    public void DoOnPointerExit()
    {
        PlayerInputs.instance.canAttack = true;
        PlayerInputs.instance.canMine = true;
    }


}
