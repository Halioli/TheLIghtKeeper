using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeInteractor : MonoBehaviour
{
    public delegate void GrassInteractionAction();
    public static event GrassInteractionAction OnGrassEnter;
    public static event GrassInteractionAction OnGrassExit;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Grass"))
        {
            if (OnGrassEnter != null) OnGrassEnter();
        }
        else if (other.CompareTag("Tree"))
        {
            other.gameObject.GetComponent<LandscapeFader>().SetTransparent();
        }
        
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Grass"))
        {
            if (OnGrassExit != null) OnGrassExit();
        }
        else if (other.CompareTag("Tree"))
        {
            other.gameObject.GetComponent<LandscapeFader>().SetOpaque();
        }
    }

}
