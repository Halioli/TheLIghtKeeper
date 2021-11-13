using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    Vector2 playerInput;
    Rigidbody2D rb;

    public float moveSpeed;
    public bool facingRight;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = playerInput.normalized * moveSpeed;
        flip();
    }

    void flip()
    {
        if((Input.GetAxisRaw("Horizontal") > 0 && facingRight) || Input.GetAxisRaw("Horizontal") < 0 && !facingRight){
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
