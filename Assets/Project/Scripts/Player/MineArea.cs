using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineArea : MonoBehaviour
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

    public Ore GetClosestOreToCollider()
    {
        float minDistance = 10f;
        int oreWithMinDistance = 0;
        Ore closestOre;

        for (int i = 0; i < GetColliders().Count; i++)
        {
            if (GetColliders()[i].GetComponent<Ore>() != null)
            {
                if (minDistance > Vector2.Distance(GetColliders()[i].transform.position, transform.position))
                {
                    oreWithMinDistance = i;
                    minDistance = Vector2.Distance(GetColliders()[i].transform.position, transform.position);
                }
            }
        }

        if (GetColliders()[oreWithMinDistance].GetComponent<Ore>() == null)
        {
            colliders.Clear();
            return null;
        }
        closestOre = GetColliders()[oreWithMinDistance].GetComponent<Ore>();
        
        colliders.Clear();
        return closestOre;
    }
}
