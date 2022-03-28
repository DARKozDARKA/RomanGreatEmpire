using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayOnTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _onEnter;

    private bool _isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_isActivated) return;
        _isActivated = true;
        _onEnter?.Invoke();
    }
}
