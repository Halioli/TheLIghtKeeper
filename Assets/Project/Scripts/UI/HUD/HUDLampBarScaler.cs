using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class HUDLampBarScaler : MonoBehaviour
{
    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    private void OnEnable()
    {
        LanternAttack.OnLanternAttackStart += ScaleUpRectTransform;
        LanternAttack.OnLanternAttackEnd += ScaleDownRectTransform;
    }

    private void OnDisable()
    {
        LanternAttack.OnLanternAttackStart -= ScaleUpRectTransform;
        LanternAttack.OnLanternAttackEnd -= ScaleDownRectTransform;
    }


    private void ScaleUpRectTransform()
    {
        rectTransform.localScale = new Vector3(1.5f, 1.5f, 1);
    }

    private void ScaleDownRectTransform()
    {
        rectTransform.localScale = new Vector3(1, 1, 1);
    }




}
