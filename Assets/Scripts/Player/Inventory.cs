using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemData> _items = new List<ItemData>();

    public void AddNewItem(ItemData item)
    {
        _items.Add(item);
    }
}
