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
        //_player.OnItemAdd += 
    }

    private void OnDisable()
    {

    }

    private void Start()
    {
        _player.Init(_camera);
    }
}
