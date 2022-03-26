using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerMover), typeof(PlayerRotator), typeof(Inventory))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _touchDistance = 3f;
    [SerializeField] private LayerMask _planksLayer;
    [SerializeField] private LayerMask _doorsLayer;
    private PlayerMover _playerMover;
    private PlayerRotator _playerRotator;
    private Inventory _inventory;
    private bool _canChangeGravity = true;
    private ItemData _currentItem;
    private Camera _camera;

    public Action<List<ItemData>> OnInventoryChange;
    public Action OnItemAddSuccesfully;
    public Action OnItemAddedInWaitList;
    public Action<bool> OnWaitListChanged;



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
        if (Input.GetKeyDown(KeyCode.F) && _inventory.HasGravityChanger)
            ReverseGravity();
        if (Input.GetKeyDown(KeyCode.E))
            TryAddItem();
        DoCrowbarRaycast();
        DoDoorRaycast();


    }

    private void DoCrowbarRaycast()
    {
        if (_inventory.HasCrowbar)
        {
            GameObject rayObject = RaycastFromCamera(_touchDistance, _planksLayer);
            if (rayObject == null) return;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(rayObject);
            }
        }
    }
    private void DoDoorRaycast()
    {
        GameObject rayObject = RaycastFromCamera(_touchDistance, _doorsLayer);
        if (rayObject == null) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            var door = rayObject.GetComponent<Door>();
            if (door is DoorLocked)
            {
                if (_inventory.HasKey)
                    door.OpenDoor();
            }
            else
                door.OpenDoor();


        }
    }

    public void Init(Camera camera)
    {
        _playerRotator.Init(camera);
        _playerMover.Init(camera);
        _camera = camera;
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
        OnItemAddSuccesfully?.Invoke();
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

    public void CleanInventory()
    {
        _inventory.CleanInventory();
    }

    public void DeleteItemFromInventory(ItemData data)
    {
        _inventory.DeleteItem(data);
    }

    public void TryAddItem()
    {
        if (_currentItem == null) return;
        if (!_inventory.Contains(_currentItem))
        {
            AddNewItem(_currentItem);
            RemoveItemFromWaitList();
        }
    }

    public void AddNewItemToWaitList(ItemData item)
    {
        _currentItem = item;
        OnWaitListChanged?.Invoke(true);
    }

    public void RemoveItemFromWaitList()
    {
        _currentItem = null;
        OnWaitListChanged?.Invoke(false);
    }

    private GameObject RaycastFromCamera(float maxDistance, LayerMask mask)
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistance, mask.value))
        {
            Transform objectHit = hit.transform;
            return objectHit.gameObject;
        }
        return null;
    }
}
