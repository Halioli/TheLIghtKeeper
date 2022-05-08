using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmanacEnvironmentTrigger : MonoBehaviour
{
    [SerializeField] int environmentID;
    [SerializeField] Collider2D colliderTrigger;
    
    
    public delegate void AlmanacEnvironmentTriggerAction(int environmentID);
    public static event AlmanacEnvironmentTriggerAction OnEnvironmentTrigger;



    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.CompareTag("Player") && colliderTrigger.IsTouchingLayers(LayerMask.NameToLayer("Player")))
        if (other.CompareTag("Player") && other.IsTouching(colliderTrigger))
        {
            if (OnEnvironmentTrigger != null) OnEnvironmentTrigger(environmentID);
        }
    }


}
