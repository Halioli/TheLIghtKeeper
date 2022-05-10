using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetableFloatingItem : FloatingItem
{
    private RectTransform rectTransform;
    private Transform transform;
    private bool hasRectTransform;

    public bool isFloating;
    Vector3 startPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            hasRectTransform = false;
            transform = GetComponent<Transform>();
            startPosition = transform.position;
        }
        else
        {
            hasRectTransform = true;
            startPosition = rectTransform.position;
        }

        Init();
    }

    void Update()
    {
        if (isFloating) ItemFloating();
    }

    public void StopFloating()
    {
        if (hasRectTransform)
        {
            rectTransform.position = startPosition;
        }
        else
        {
            transform.position = startPosition;
        }
        
        isFloating = false;
    }

    public void StartFloating()
    {
        isFloating = true;
    }

}
