using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item", order = 1)]
public class ItemData : ScriptableObject
{
    public Sprite image;
    public GameObject model;
    public string itemName;
}
