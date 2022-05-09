using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetableFloatingItem : FloatingItem
{
    RectTransform rectTransform;

    public bool isFloating;
    Vector3 startPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.position;

        Init();
    }

    void Update()
    {
        if (isFloating) ItemFloating();
    }

    public void StopFloating()
    {
        rectTransform.position = startPosition;
        isFloating = false;
    }

    public void StartFloating()
    {
        isFloating = true;
    }

}
