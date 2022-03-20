using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckShadowSpider : ShadowSpider
{


    public override void ReceiveDamage(int damageValue)
    {
        base.ReceiveDamage(damageValue);
    }

    protected override void DropItem()
    {
        return;
    }


}
