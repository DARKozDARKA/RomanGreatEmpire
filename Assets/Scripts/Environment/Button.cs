using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject _platformObject;
    [SerializeField] private float _normalScale;
    [SerializeField] private float _activeTime = 5f;
    [SerializeField] private float _appearSpeed = 1f;

    private bool _isAppearing = false;
    private bool _isDisappering = false;
    private Vector3 _currentScale;
    private bool _isEnabled = false;

    private void Start()
    {
        _platformObject.SetActive(false);
    }

    private void Update()
    {
        if (_isAppearing)
        {
            _currentScale += Vector3.one * Time.deltaTime * _appearSpeed;
            _platformObject.transform.localScale = _currentScale;
            if (_currentScale.x >= _normalScale)
                _isAppearing = false;
        }
        else if (_isDisappering)
        {
            _currentScale -= Vector3.one * Time.deltaTime * _appearSpeed;
            _platformObject.transform.localScale = _currentScale;
            if (_currentScale.x <= 0.05f)
            {
                _platformObject.SetActive(false);
                _isDisappering = false;
            }

        }
    }

    public void PressButton()
    {
        if (_isEnabled) return;
        _platformObject.SetActive(true);
        _isAppearing = true;
        _isEnabled = true;
        StartCoroutine(Disable());

    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(_activeTime);
        _isAppearing = false;
        _isDisappering = true;
        _isEnabled = false;
    }



}
