using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairCursor : PlayerInputs
{
    // Private Attributes
    private SpriteRenderer crosshairSpriteRenderer;

    // Public Attributes
    //public GameObject pickAxe;

    public Sprite crosshairNormal;
    public Sprite crosshairPickaxe;
    public Sprite crosshairPickaxeX;
    public Sprite crosshairSword;
    public Sprite crosshairSwordX;


    private void Awake()
    {
        crosshairSpriteRenderer = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
    }

    
    private void Update()
    {
        SetNewMousePosition();
        transform.position = mouseWorldPosition;
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision != null && collision.gameObject.CompareTag("Ore"))
    //    {
    //        GetComponent<SpriteRenderer>().sprite = crosshairPickaxe;
    //    }
    //    else if (collision != null && collision.gameObject.CompareTag("Enemy"))
    //    {
    //        GetComponent<SpriteRenderer>().sprite = crosshairSword;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ore")|| (collision != null && collision.gameObject.CompareTag("Enemy")))
    //    {
    //        GetComponent<SpriteRenderer>().sprite = crosshairNormal;
    //    }
    //}
}
