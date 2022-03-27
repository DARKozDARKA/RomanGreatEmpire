using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointTrigger : MonoBehaviour
{
    [SerializeField] private int _priority;
    [SerializeField] private Transform _positionTransform;
    [SerializeField] private bool _gravity;

    private void OnTriggerEnter(Collider other)
    {
        SavePointManager.Instance.TrySetNewSavePoint(_priority, _positionTransform, _gravity);
    }
}
