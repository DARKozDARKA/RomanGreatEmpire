using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _doorClosed;
    [SerializeField] private GameObject _doorOpened;
    public UnityEvent onDoorOpen;

    public void OpenDoor()
    {
        onDoorOpen?.Invoke();
        _doorClosed.SetActive(false);
        _doorOpened.SetActive(true);
    }
}
