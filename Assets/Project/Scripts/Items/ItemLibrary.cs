using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLibrary : MonoBehaviour
{
    public static ItemLibrary instance;

    [SerializeField] Item[] items;
    Dictionary<int, Item> itemCollection;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        InitItemCollection();
    }


    private void InitItemCollection()
    {
        itemCollection = new Dictionary<int, Item>();
        for (int i = 0; i < items.Length; ++i)
        {
            itemCollection.Add(items[i].ID, items[i]);
        }
    }

    public Item GetItem(int ID)
    {
        return itemCollection[ID];
    }


}
