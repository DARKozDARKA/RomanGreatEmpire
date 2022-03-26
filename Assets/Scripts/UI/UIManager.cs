using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pickUpMenu;

    public void SetActivePickUpMenu(bool isActive)
    {
        _pickUpMenu.SetActive(isActive);
    }
}
