using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarItemCell : ItemCell
{
    [SerializeField] GameObject CellSelectedImageGameObject;


    public override void DoOnSelect() 
    {
        CellSelectedImageGameObject.SetActive(true);
    }

    public override void DoOnDiselect() 
    {
        CellSelectedImageGameObject.SetActive(false);
    }

}
