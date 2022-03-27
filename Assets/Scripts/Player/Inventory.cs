using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    private List<ItemData> _items = new List<ItemData>();
    public Action<List<ItemData>> OnItemsChange;
    public bool HasCrowbar => _specialItems["Crowbar"];
    public bool HasGravityChanger => _specialItems["GravityChanger"];
    public bool HasKey => _specialItems["Key"];
    private Dictionary<string, bool> _specialItems = new Dictionary<string, bool>
    {
        {"Crowbar", false},
        {"GravityChanger", false},
        {"Key", false}
    };
    public Dictionary<string, bool> specialItems => _specialItems;

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
        foreach (var specialItem in _specialItems.Keys)
        {
            if (specialItem == item.name)
            {
                _specialItems[specialItem] = setBool;
                return;
            }

        }
    }
    private void SetSpecialObjectToFalse()
    {
        foreach (var item in _specialItems.Keys)
        {
            _specialItems[item] = false;
        }
    }
}

public class SpecialItem
{
    public readonly string name;
    public bool exists;
    public SpecialItem(string name, bool exists)
    {
        this.name = name;
        this.exists = exists;
    }
}