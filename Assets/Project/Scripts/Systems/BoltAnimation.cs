using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltAnimation : MonoBehaviour
{
    
    private Animator boltAnimator;

    // Start is called before the first frame update
    void Start()
    {
        boltAnimator = GetComponentInChildren<Animator>();
    }

    public void ActivateBolt()
    {
        boltAnimator.SetBool("isActive", true);
    }

    public void DesactivateBolt()
    {
        boltAnimator.SetBool("isActive", false);
    }

}
