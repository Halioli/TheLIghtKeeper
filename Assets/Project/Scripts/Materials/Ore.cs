using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public int id;
    public int maxHardness;
    private int hardness;

    private float alphaDecrement;

    public GameObject ore;
    private SpriteRenderer sprite;

    private void Start()
    {
        hardness = maxHardness;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void GetMined()
    {
        Debug.Log(id);
        hardness -= 1;

        Test_ChangeOreColor();

    }


    private void Test_ChangeOreColor()
    {
        if (hardness <= 0)
        {
            SpawnMaterial();
        }
        else if (hardness <= maxHardness / 3)
        {
            sprite.color = new Color(1, 0, 0, 1);
        }
        else if (hardness <= (maxHardness / 3) * 2)
        {
            sprite.color = new Color(0.5f, 0, 0, 1);
        }
    }

    private void SpawnMaterial()
    {
        Debug.Log("Spawned material for: " + id);
        ore.SetActive(false);
    }
}
