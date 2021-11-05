using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public new Rigidbody2D rigidbody2D;
    public GameObject interactObject;
    public GameObject attackObject;
    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        // Inputs
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    // FixedUpdate is used to calculate physics without the framerate's interference
    void FixedUpdate()
    {
        // Move
        if (movement.x != 0 && movement.y != 0)
        {
            // Diagonal movement
            rigidbody2D.MovePosition(rigidbody2D.position + movement * moveSpeed/2 * Time.fixedDeltaTime);
        }
        else
        {
            // Standard movement
            rigidbody2D.MovePosition(rigidbody2D.position + movement * moveSpeed * Time.fixedDeltaTime);
            
            if (movement.x != 0) // Horizontal
            {
                interactObject.transform.localPosition = new Vector2(1 * movement.x, 0);
                
                attackObject.transform.localPosition = new Vector2(1 * movement.x, 0);
                attackObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (movement.y != 0) // Vertical
            {
                interactObject.transform.localPosition = new Vector2(0, 1 * movement.y);
                
                attackObject.transform.localPosition = new Vector2(0, 1 * movement.y);
                attackObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
        }
    }
}
