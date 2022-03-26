using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerMover), typeof(PlayerRotator), typeof(Inventory))]
public class Player : MonoBehaviour
{
    private PlayerMover _playerMover;
    private PlayerRotator _playerRotator;
    private Inventory _inventory;
    private bool _canChangeGravity = true;
    public Action<List<ItemData>> OnInventoryChange;



    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _playerRotator = GetComponent<PlayerRotator>();
        _inventory = GetComponent<Inventory>();
        _playerRotator.OnGravityStopReversing += SetReverseGravityAvailible;
        _inventory.OnItemsChange += ChangeInventory;
    }

    private void OnDestroy()
    {
        _playerRotator.OnGravityStopReversing -= SetReverseGravityAvailible;
        _inventory.OnItemsChange -= ChangeInventory;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            ReverseGravity();

    }

    public void Init(Camera camera)
    {
        _playerRotator.Init(camera);
    }

    public void ReverseGravity()
    {
        if (!_canChangeGravity || !_playerMover.IsGrounded || _playerMover.moverState != PlayerMoverStates.moving) return;
        _playerMover.ReverseGravity();
        _playerRotator.ReverseGravity();
        _canChangeGravity = false;
    }
    public void StartClimbingLadder()
    {
        _playerMover.StartClimbingLadder();
    }

    public void StopClimbingLadder()
    {
        _playerMover.StopClimbingLadder();
    }

    private void SetReverseGravityAvailible()
    {
        _canChangeGravity = true;
    }

    public void ReverseVelocity()
    {
        _playerMover.ReverseVelocity();
    }

    public void AddNewItem(ItemData item)
    {
        _inventory.AddNewItem(item);

    }

    private void ChangeInventory(List<ItemData> list)
    {
        OnInventoryChange?.Invoke(list);
    }

    public void SetInvetory(List<string> list)
    {
        var newList = new List<ItemData>();
        foreach (var item in list)
        {
            newList.Add(StringToItemDataConverter.Instance.GetDataFromString(item));
        }
        _inventory.SetInventory(newList);
    }
}
