using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Player _player;

    private void Start()
    {
        _player.Init(_camera);
    }
}
