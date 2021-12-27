using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    // Private Attributes
    private List<Collider2D> colliders = new List<Collider2D>();

    // Public Attributes
    public List<Collider2D> GetColliders() { return colliders; }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!colliders.Contains(collision))
        {
            colliders.Add(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!colliders.Contains(collision)) { colliders.Add(collision); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colliders.Remove(collision);
    }

    public void DamageAllInCollider(int damage)
    {
        for (int i = 0; i < GetColliders().Count; i++)
        {
            if (GetColliders()[i].GetComponent<Enemy>() != null)
            {
                GetColliders()[i].GetComponent<Enemy>().ReceiveDamage(damage);
                Debug.Log("Enemy attacked");
            }
            Debug.Log(GetColliders()[i]);
        }
    }
}
