using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarItemCell : ItemCell
{
    [SerializeField] GameObject CellSelectedImageGameObject;
    [SerializeField] GameObject UseCellSelectedImageGameObject;


    public override void DoOnSelect() 
    {
        CellSelectedImageGameObject.SetActive(true);
    }

    public override void DoOnSelect(bool isConsumible)
    {
        CellSelectedImageGameObject.SetActive(true);
        UseCellSelectedImageGameObject.SetActive(isConsumible);
    }

    public override void DoOnDiselect() 
    {
        CellSelectedImageGameObject.SetActive(false);
    }

}
