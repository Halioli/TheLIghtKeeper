using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationItem : MonoBehaviour
{
    Interpolator interpolator;
    Vector2 startPosition;
    Vector2 endPosition;

    // Must be called on instantiate
    public void Init(Sprite sprite, Vector2 startPosition, Vector2 endPosition, float duration = 0.5f)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;

        this.startPosition = startPosition;
        this.endPosition = endPosition;

        interpolator = new Interpolator(duration, Interpolator.Type.SIN);
        interpolator.ToMax();
    }

    void Update()
    {
        interpolator.Update(Time.deltaTime);
        
        if (interpolator.isMaxPrecise)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.Lerp(startPosition, endPosition, interpolator.Value);       
    }

}
