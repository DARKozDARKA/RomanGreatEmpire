using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SavePointManager : MonoBehaviour
{
    public static SavePointManager Instance;

    private int _currentPriority;
    private Player _player;
    private Transform _currentTransform;
    private bool _currentGravity;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        Instance = this;
    }

    public void Init(Player player)
    {
        _player = player;
    }

    public void TrySetNewSavePoint(int priority, Transform newTransform, bool gravity)
    {
        if (priority > _currentPriority)
        {
            _currentTransform = newTransform;
            _currentPriority = priority;
            _currentGravity = gravity;
        }
    }

    public void SetPlayerToSave()
    {
        _player.SetToPosition(_currentTransform.position, _currentGravity);
    }
}
