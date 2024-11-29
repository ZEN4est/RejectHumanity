using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] float _mouseSensitivity = 100f;
    private Transform _playerCamera;
    private float _xRotation = 0f;

    private void Start()
    {
        _playerCamera = Camera.main.transform;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _playerCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}