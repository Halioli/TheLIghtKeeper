using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : MonoBehaviour
{
    [SerializeField] GameObject leftBridgeGameObject;
    [SerializeField] GameObject rightBridgeGameObject;
    [SerializeField] Sprite bridgeSprite;

    [SerializeField] public AudioSource audioSource;

    private SpriteRenderer bridgeSpriteRenderer;

    public bool constructed;

    private void Awake()
    {
        SaveSystem.bridges.Add(this);
    }
    private void Start()
    {
        bridgeSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(constructed)
        {
            BridgeConstructed();
        }
    }
    public void BridgeConstructed()
    {
        bridgeSpriteRenderer.sprite = bridgeSprite;

        leftBridgeGameObject.SetActive(false);
        rightBridgeGameObject.SetActive(false);

    }
}
