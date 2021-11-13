using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private static readonly Dictionary<ItemType, ItemData> items = new Dictionary<ItemType, ItemData>();

    private void Awake() => InitializeStorage();
    private void InitializeStorage() 
    {
        ItemData[] loadedItems = Resources.LoadAll<ItemData>("Items");

        int length = loadedItems.Length;

        for (int i = 0; i < length; i++) 
        items.Add(loadedItems[i].type, loadedItems[i]);
    }

    public static Sprite GetItemIcon(ItemType type) => items[type].icon;
}
