using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public string name;

    private bool activated = false;
    private Vector2 spawnPosition;
    private Animator animatior;

    private void Start()
    {
        spawnPosition = transform.position;
        animatior = GetComponent<Animator>();
    }

    private void Update()
    {
    
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Do the activate teleport animation and stay teleport
            if (!activated)
            {
                Debug.Log("AAAAAAA");
                animatior.SetBool("isActivated", true);
            }
        }
    }
}