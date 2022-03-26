using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData _data;
    [SerializeField] private Transform _itemPosition;
    private GameObject _object;
    private Player _player;
    private bool _isTaken;

    private void Start()
    {
        _object = Instantiate(_data.model, _itemPosition.position, Quaternion.identity);
        _isTaken = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (_isTaken) return;
        _player = other.GetComponent<Player>();
        _player.AddNewItemToWaitList(_data);
        _player.OnItemAddSuccesfully += DeleteItem;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isTaken) return;
        _player.RemoveItemFromWaitList();
    }

    private void DeleteItem()
    {
        _player.OnItemAddSuccesfully -= DeleteItem;
        _isTaken = true;
        Destroy(_object);
    }



}
