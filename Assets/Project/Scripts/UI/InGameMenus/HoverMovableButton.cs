using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMovableButton : HoverButton
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] RectTransform start;
    [SerializeField] RectTransform end;
    Vector2 startPosition;
    Vector2 endPosition;
    [SerializeField] float moveDuration = 0.2f;
    Interpolator interpolator;


    private void Awake()
    {
        startPosition = start.GetComponent<RectTransform>().position;
        endPosition = end.GetComponent<RectTransform>().position;

        transform.position = startPosition;
    }

    private bool IsAtStartPosition()
    {
        return (Vector2)transform.position == startPosition;
    }

    private bool IsAtEndPosition()
    {
        return (Vector2)transform.position == endPosition;
    }


    public void MoveToStartPosition()
    {
        interpolator = new Interpolator(moveDuration, Interpolator.Type.QUADRATIC);
        interpolator.ToMax();


        if (!IsAtEndPosition())
        {
            StopCoroutine(MoveTowards(transform.position, startPosition));
        }
        StartCoroutine(MoveTowards(transform.position, startPosition));
    }


    public void MoveToEndPosition()
    {
        interpolator = new Interpolator(moveDuration, Interpolator.Type.QUADRATIC);
        interpolator.ToMax();

        if (!IsAtStartPosition())
        {
            StopCoroutine(MoveTowards(transform.position, endPosition));
        }
        StartCoroutine(MoveTowards(transform.position, endPosition));
    }

    IEnumerator MoveTowards(Vector2 startPos, Vector2 endPos)
    {
        while (!interpolator.isMaxPrecise)
        {
            interpolator.Update(Time.deltaTime);

            transform.position = Vector2.Lerp(startPos, endPos, interpolator.Value);


            yield return null;
        }

    }

}
