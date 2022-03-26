using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringToItemDataConverter : MonoBehaviour
{
    public static StringToItemDataConverter Instance { get; private set; }

    [SerializeField] private List<ItemData> _items;

    private Dictionary<string, ItemData> _stringToData;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        Instance = this;


        _stringToData = new Dictionary<string, ItemData>();
        foreach (var item in _items)
        {
            _stringToData.Add(item.itemName, item);
        }
    }

    public ItemData GetDataFromString(string name)
    {
        if (_stringToData.ContainsKey(name))
            return _stringToData[name];
        return null;
    }
}
