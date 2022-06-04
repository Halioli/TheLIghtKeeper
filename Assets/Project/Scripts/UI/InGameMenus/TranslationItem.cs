using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslationItem : MonoBehaviour
{
    Interpolator interpolator;
    Vector2 startPosition;
    Vector2 endPosition;

    Image spriteImage;

    private void Awake()
    {
        spriteImage = GetComponent<Image>();
    }


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

    private void OnEnable()
    {
        InteractStation.OnInteractClose += End;
    }

    private void OnDisable()
    {
        InteractStation.OnInteractClose -= End;
    }

    // Must be called on instantiate/activation
    public void Init(Sprite sprite, Vector2 startPosition, Vector2 endPosition, float duration = 0.5f)
    {
        spriteImage.sprite = sprite;

        this.startPosition = startPosition;
        this.endPosition = endPosition;

        interpolator = new Interpolator(duration, Interpolator.Type.QUADRATIC);
        interpolator.ToMax();

        //gameObject.SetActive(true);
        StartCoroutine(LateShow());
    }

    IEnumerator LateShow()
    {
        Color color = spriteImage.color;
        color.a = 0.0f;
        spriteImage.color = color;

        yield return null;

        color.a = 1.0f;
        spriteImage.color = color;
    }



    private void End()
    {
        gameObject.SetActive(false);
    }


}
