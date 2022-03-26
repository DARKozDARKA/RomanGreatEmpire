using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JSONReader), typeof(JSONSaver))]
public class JSONManager : MonoBehaviour
{
    [SerializeField] private string _fileName = "items";
    private JSONReader _reader;
    private JSONSaver _saver;



    private void Awake()
    {
        _reader = GetComponent<JSONReader>();
        _saver = GetComponent<JSONSaver>();

        //_saver.SaveToFile(new ItemsParameters("items", new List<string>() { "Apple" }));
        //_reader.ReadFromFile("items");
    }

    public void AddNewItem(ItemData data)
    {
        _saver.SaveToFile(new ItemsParameters("_fileName", new List<string>() { data.itemName }));
    }
}
