using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    // Public Attributes
    public PlayerCombat playerCombat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            playerCombat.DealDamageToEnemy(collision.gameObject.GetComponent<Enemy>());
            playerCombat.TargetWasHit();
        }
    }
}
