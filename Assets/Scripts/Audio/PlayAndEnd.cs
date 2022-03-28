using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayAndEnd : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clip;

    private bool _isActivated = false;
    private bool _isPlaying = false;

    [SerializeField] private UnityEvent _onEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (_isActivated) return;
        _source.clip = _clip;
        _source.Play();
        _isActivated = true;
        _isPlaying = true;
    }

    public void Play(AudioClip _clip)
    {
        if (_isActivated) return;
        _source.clip = _clip;
        _source.Play();
        _isActivated = true;
        _isPlaying = true;
    }

    private void Update()
    {
        if (_isPlaying && _isActivated)
        {
            if (!_source.isPlaying)
            {
                _isPlaying = false;
                _onEnd?.Invoke();
            }
        }
    }
}
