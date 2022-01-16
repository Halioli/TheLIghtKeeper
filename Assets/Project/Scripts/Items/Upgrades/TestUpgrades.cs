using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUpgrades : MonoBehaviour
{

    private void OnEnable()
    {
        LanternSourceUpgrade.OnLanternSourceUpgrade += TestLanternUpgrade;
    }

    private void OnDisable()
    {
        LanternSourceUpgrade.OnLanternSourceUpgrade -= TestLanternUpgrade;
    }


    public void TestLanternUpgrade()
    {
        Debug.Log("Lanter Upgrade");
    }

}
