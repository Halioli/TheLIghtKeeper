using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeFader : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;


    public void SetTransparent()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
    }

    public void SetOpaque()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
    }


}
