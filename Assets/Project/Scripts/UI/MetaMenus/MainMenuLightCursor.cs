using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLightCursor : MonoBehaviour
{
    void Update()
    {
        transform.position = PlayerInputs.instance.GetMousePositionInWorld();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hello");
        if (collision.CompareTag("Enemy"))
        {
            //collision.GetComponent<MainMenuEnemy>().ReceiveDamage(1);
        }
    }
}
