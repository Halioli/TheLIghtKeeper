using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMiner : MonoBehaviour
{
    private Collider2D oreToMineCollider = null;
    public float miningReachRadius = 3f;


    // Update is called once per frame
    void Update()
    {
        Mine();
    }


    // METHODS
    void Mine()
    {
        // Check mouse right click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // 0. Calculate mouse position in scene
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Check if mouse click is in range within miningReachRadius
            float distancePlayerMouseClick = Vector2.Distance(mousePosition, transform.position);
            if (distancePlayerMouseClick <= miningReachRadius)
            {
                // 1. Get Collider2D where mouse clicked
                Collider2D currentCollider = Physics2D.OverlapCircle(mousePosition, 0.05f);

                // 2. Check if said Collider2D is an Ore
                if (currentCollider != null && currentCollider.gameObject.CompareTag("Ore"))
                {
                    // 3. Check if it is a "new" or different Collider2D
                    if (oreToMineCollider == null || oreToMineCollider != currentCollider)
                    {
                        // 4. Set oreToMineCollider to this Collider2D
                        oreToMineCollider = currentCollider;
                    }

                    // 5. Mine the Ore
                    Debug.Log("Clicked an Ore\n");
                    Ore oreToMine = oreToMineCollider.gameObject.GetComponent<Ore>();
                    if (oreToMine.CanBeMined())
                        oreToMine.GetsMined(1);

                }
            }
            else
            {
                oreToMineCollider = null;
            }
        }
    }
}