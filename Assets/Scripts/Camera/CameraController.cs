using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Mouse Sensivity")]
    [SerializeField] float _sensX;
    [SerializeField] float _sensY;
    [SerializeField] float _multiplier;

    float _mouseX;
    float _mouseY;

    float _rotationX;
    float _rotationY;

    Camera _camera;


    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        ControlCameraInput();

        _camera.transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, _rotationY, 0f);
    }
    void ControlCameraInput()
    {
        _mouseX = Input.GetAxisRaw("Mouse X");
        _mouseY = Input.GetAxisRaw("Mouse Y");

        _rotationY += _mouseX * _sensX * _multiplier;
        _rotationX -= _mouseY * _sensY * _multiplier;

        _rotationX = Mathf.Clamp(_rotationX, -90, 90);
    }
}
