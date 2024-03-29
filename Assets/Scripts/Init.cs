﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Player _player;
    [SerializeField] private JSONManager _jsonManager;
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private bool _firstLevel = false;

    private void OnEnable()
    {
        _player.OnInventoryChange += _jsonManager.SetNewItems;
        _player.OnWaitListChanged += _UIManager.SetActivePickUpMenu;
    }

    private void OnDisable()
    {
        _player.OnInventoryChange -= _jsonManager.SetNewItems;
        _player.OnWaitListChanged -= _UIManager.SetActivePickUpMenu;
    }

    private void Start()
    {
        _player.Init(_camera);
        SavePointManager.Instance.Init(_player);

        if (_firstLevel)
        {
            _player.CleanInventory();
            return;
        }
        var list = _jsonManager.ReadFromFile();
        if (list != null)
            _player.SetInvetory(list);

    }
}
