using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairCursor : MonoBehaviour
{
    public GameObject pickAxe;
    // Start is called before the first frame update
    private void Awake()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseCursorPos;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Ore"))
        {
            pickAxe.SetActive(true);
        }
        else
        {
            pickAxe.SetActive(false);
        }
    }*/
}
