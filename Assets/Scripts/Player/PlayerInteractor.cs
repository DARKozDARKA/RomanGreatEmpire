using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float _touchDistance = 3f;
    [SerializeField] private LayerMask _planksLayer;
    [SerializeField] private LayerMask _doorsLayer;
    [SerializeField] private LayerMask _buttonLayer;

    [SerializeField] private UnityEvent _onButtonPressed;


    private Camera _camera;

    public void Init(Camera camera)
    {
        _camera = camera;
    }
    public void DoCrowbarRaycast()
    {

        GameObject rayObject = RaycastFromCamera(_touchDistance, _planksLayer);
        if (rayObject == null) return;
        Destroy(rayObject);


    }
    public void DoDoorRaycast(bool hasKey)
    {
        GameObject rayObject = RaycastFromCamera(_touchDistance, _doorsLayer);
        if (rayObject == null) return;
        var door = rayObject.GetComponent<Door>();
        if (door is DoorLocked)
        {
            if (hasKey)
                door.OpenDoor();
        }
        else
            door.OpenDoor();
    }

    private GameObject RaycastFromCamera(float maxDistance, LayerMask mask)
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistance, mask.value))
        {
            Transform objectHit = hit.transform;
            return objectHit.gameObject;
        }
        return null;
    }

    public void DoButtonRaycast()
    {
        GameObject rayObject = RaycastFromCamera(_touchDistance, _buttonLayer);
        if (rayObject == null) return;
        var button = rayObject.GetComponent<Button>();
        button.PressButton();
        _onButtonPressed?.Invoke();
    }
}