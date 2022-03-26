using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    private List<ItemData> _items = new List<ItemData>();
    public Action<List<ItemData>> OnItemsChange;
    public bool HasCrowbar { get; private set; } = false;
    public bool HasGravityChanger { get; private set; } = false;
    public bool HasKey { get; private set; } = false;

    public void AddNewItem(ItemData item)
    {
        _items.Add(item);
        OnItemsChange?.Invoke(_items);
        CheckOnSpecialObjects(item, true);
    }

    public void SetInventory(List<ItemData> list)
    {
        _items = list;
        foreach (var item in _items)
            CheckOnSpecialObjects(item, true);

    }

    public void CleanInventory()
    {
        _items = new List<ItemData>();
        OnItemsChange?.Invoke(_items);
        SetSpecialObjectToFalse();
    }

    public void DeleteItem(ItemData data)
    {
        if (_items.Contains(data))
            _items.Remove(data);
        OnItemsChange?.Invoke(_items);
        CheckOnSpecialObjects(data, false);
    }

    public bool Contains(ItemData data)
    {
        foreach (var item in _items)
        {
            if (item.name == data.name)
                return true;
        }
        return false;
    }

    private void CheckOnSpecialObjects(ItemData item, bool setBool)
    {
        if (item.name == "Crowbar")
            HasCrowbar = setBool;
        if (item.name == "GravityChanger")
            HasGravityChanger = setBool;
        if (item.name == "Key")
            HasKey = setBool;
    }
    private void SetSpecialObjectToFalse()
    {
        HasCrowbar = false;
        HasGravityChanger = false;
    }
}
