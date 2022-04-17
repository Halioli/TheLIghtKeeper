using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionUpgradeButton : UpgradeButton
{
    [SerializeField] GameObject[] attentions;
    [SerializeField] bool isAttentionActive = true;


    public override void UpgradeSelected() // called on click
    {
        base.UpgradeSelected();

        CleanAttentions();
    }

    public override void AlwaysProgressUpgradeSelected()
    {
        base.AlwaysProgressUpgradeSelected();

        CleanAttentions();
    }


    private void CleanAttentions()
    {
        if (isCompleted && isAttentionActive)
        {
            isAttentionActive = false;

            foreach (GameObject attention in attentions)
            {
                Destroy(attention);
            }

        }
    }


}
