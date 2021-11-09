using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharger : MonoBehaviour
{
    // Public
    public GameObject player;
    public AttackSystem attackSystem;
    public float speed = 2f;
    public float minDistance = 5f;

    // Private
    private float distance;

    private void Start()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (distance > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {

        }

        // Update distance
        distance = Vector2.Distance(transform.position, player.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HealthSystem>() != null)
        {
            attackSystem.DamageHealthSystemWithAttackValue(collision.GetComponent<HealthSystem>());
        }
    }
}
