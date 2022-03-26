using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData _data;
    [SerializeField] private Transform _itemPosition;
    private GameObject _object;
    private bool _isTaken;

    private void Start()
    {
        _object = Instantiate(_data.model, _itemPosition.position, Quaternion.identity);
        _isTaken = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (_isTaken) return;
        other.GetComponent<Player>().AddNewItem(_data);
        Destroy(_object);
        _isTaken = true;
    }

}
