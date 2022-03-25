using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 100f;
    [SerializeField] private float _reversingAngularSpeed = 3f;
    private float _xRotation;
    private Camera _camera;
    private Quaternion _startRotation;
    private Quaternion _endRotation;
    private float _rotationProgress = -1;

    private bool _isReversingGravity = false;

    public Action OnGravityStopReversing;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Init(Camera camera)
    {
        _camera = camera;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        if (_rotationProgress < 1 && _rotationProgress >= 0)
        {
            _rotationProgress += Time.deltaTime * _reversingAngularSpeed;

            transform.rotation = Quaternion.Lerp(_startRotation, _endRotation, _rotationProgress);
        }
        if (_isReversingGravity && _rotationProgress >= 1)
            OnGravityStopReversing?.Invoke();


    }

    public void ReverseGravity()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y, rot.z + 180);
        StartRotating(Quaternion.Euler(rot));
        _isReversingGravity = true;
    }


    private void StartRotating(Quaternion endRotation)
    {

        // Here we cache the starting and target rotations
        _startRotation = transform.rotation;
        _endRotation = endRotation;

        // This starts the rotation, but you can use a boolean flag if it's clearer for you
        _rotationProgress = 0;
    }


}
