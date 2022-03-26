using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _doorClosed;
    [SerializeField] private GameObject _doorOpened;

    public void OpenDoor()
    {
        _doorClosed.SetActive(false);
        _doorOpened.SetActive(true);
    }
}
