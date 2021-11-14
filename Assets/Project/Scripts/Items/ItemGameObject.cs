using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGameObject : MonoBehaviour
{
    // Private Attributes
    private Rigidbody2D rigidbody2D;
    
    private float dropDownForceY = 1.5f;
    private float dropDownTime = 0.37f;

    private float dropForwarndForceX = 2.0f;
    private float dropForwarndForceY = 2.5f;
    private float dropForwardTime = 0.55f;


    // Public Attributes
    public Item item;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    public void DropsDown()
    {
        rigidbody2D.AddForce(transform.up * dropDownForceY, ForceMode2D.Impulse);

        StartCoroutine("StopDroping", dropDownTime);
    }

    public void DropsForward(int directionX)
    {
        rigidbody2D.AddForce(new Vector2(directionX * dropForwarndForceX, dropForwarndForceY), ForceMode2D.Impulse);

        StartCoroutine("StopDroping", dropForwardTime);
    }

    IEnumerator StopDroping(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        rigidbody2D.bodyType = RigidbodyType2D.Static;
    }


}
