using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslationItem : MonoBehaviour
{
    Interpolator interpolator;
    Vector2 startPosition;
    Vector2 endPosition;

    void Update()
    {
        interpolator.Update(Time.deltaTime);
        
        if (interpolator.isMaxPrecise)
        {
            End();
            return;
        }

        transform.position = Vector2.Lerp(startPosition, endPosition, interpolator.Value);
    }


    // Must be called on instantiate/activation
    public void Init(Sprite sprite, Vector2 startPosition, Vector2 endPosition, float duration = 0.5f)
    {
        GetComponent<Image>().sprite = sprite;

        this.startPosition = startPosition;
        this.endPosition = endPosition;

        interpolator = new Interpolator(duration, Interpolator.Type.QUADRATIC);
        interpolator.ToMax();

        //gameObject.SetActive(true);
    }

    private void End()
    {
        gameObject.SetActive(false);
    }


}
