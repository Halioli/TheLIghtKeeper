using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationItemSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> translationItems;
    [SerializeField] float randomSpawnOffset = 10.0f;

    int index = 0;

    private void Awake()
    {
        HideAllTranslationItems();
    }



    public void Spawn(KeyValuePair<Item, int> itemAndAmount, Vector2 startPosition, Vector2 endPosition)
    {
        StartCoroutine(ProgressiveSpawn(itemAndAmount, startPosition, endPosition));
    }

    IEnumerator ProgressiveSpawn(KeyValuePair<Item, int> itemAndAmount, Vector2 startPosition, Vector2 endPosition)
    {
        for (int i = 0; i < itemAndAmount.Value; ++i)
        {
            Vector2 offsetStartPosition = startPosition + (Random.insideUnitCircle * randomSpawnOffset);
            Debug.Log(gameObject.transform.name);
            translationItems[index].SetActive(true);
            translationItems[index].GetComponent<TranslationItem>().Init(itemAndAmount.Key.sprite, offsetStartPosition, endPosition);

            index = ++index % translationItems.Count;

            yield return new WaitForSeconds(0.05f);
        }
    }




    private void HideAllTranslationItems()
    {
        for (int i = 0; i < translationItems.Count; ++i)
        {
            translationItems[i].SetActive(false);
        }
    }


}
