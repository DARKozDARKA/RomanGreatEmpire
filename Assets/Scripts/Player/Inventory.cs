using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    private List<ItemData> _items = new List<ItemData>();

    public Action<List<ItemData>> OnItemsChange;

    public void AddNewItem(ItemData item)
    {
        _items.Add(item);
        OnItemsChange?.Invoke(_items);
    }

    public void SetInventory(List<ItemData> list)
    {
        _items = list;
    }

    public void CleanInventory()
    {
        _items = new List<ItemData>();
        OnItemsChange?.Invoke(_items);
    }

    public void DeleteItem(ItemData data)
    {
        if (_items.Contains(data))
            _items.Remove(data);
        OnItemsChange?.Invoke(_items);
    }
}
