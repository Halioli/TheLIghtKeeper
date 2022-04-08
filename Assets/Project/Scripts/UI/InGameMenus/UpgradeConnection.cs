using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeConnection : MonoBehaviour
{
    [SerializeField] Sprite notActiveSprite;
    [SerializeField] Sprite activeSprite;
    [SerializeField] Sprite completedSprite;

    [SerializeField] Color32 notActiveColor;
    [SerializeField] Color32 activeColor;

    [SerializeField] Image[] sourceImages;
    int sourceImageIndex = 0;



    public void Init(int lastConnectionIndex)
    {
        Progress(lastConnectionIndex);

        for (int i = lastConnectionIndex + 2; i < sourceImages.Length; ++i)
        {
            SetNotActive(i);
        }
    }



    private void SetSourceImageSprite(int index, Sprite sprite)
    {
        sourceImages[index].sprite = sprite;
    }

    private void SetSourceImageColor(int index, Color32 color)
    {
        sourceImages[index].color = color;
    }


    public void SetNotActive(int index)
    {
        SetSourceImageSprite(index, notActiveSprite);
        SetSourceImageColor(index, notActiveColor);
    }

    public void SetActive(int index)
    {
        SetSourceImageSprite(index, activeSprite);
        SetSourceImageColor(index, activeColor);
    }

    public void SetCompleted(int index)
    {
        SetSourceImageSprite(index, completedSprite);
        SetSourceImageColor(index, activeColor);
    }



    public void Progress(int endIndex)
    {
        if (endIndex >= sourceImages.Length) return;

        for (int i = sourceImageIndex; i < endIndex; ++i)
        {
            SetCompleted(i);
        }

        if (endIndex+1 < sourceImages.Length) SetActive(endIndex + 1);


        sourceImageIndex = endIndex+1;
    }


    public void ProgressOneStage()
    {
        SetCompleted(sourceImageIndex++);
        if (sourceImageIndex < sourceImages.Length) SetActive(sourceImageIndex);
    }


}
