using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Player _player;
    [SerializeField] private JSONManager _jsonManager;

    private void OnEnable()
    {
        _player.OnInventoryChange += _jsonManager.SetNewItems;
    }

    private void OnDisable()
    {
        _player.OnInventoryChange -= _jsonManager.SetNewItems;
    }

    private void Start()
    {
        _player.Init(_camera);

        var list = _jsonManager.ReadFromFile();
        if (list != null)
            _player.SetInvetory(list);

    }
}
