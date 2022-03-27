using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    public void PlayOneShot(AudioClip clip)
    {
        _source.pitch = Random.Range(0.95f, 1.05f);
        _source.PlayOneShot(clip);
    }
}
