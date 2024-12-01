using UnityEngine;
using Zenject;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [Inject] private DoorLockService _doorLockService;

    private void Start()
    {
        _doorLockService.Show += OnDoorLockShow;
        _doorLockService.Hide += OnDoorLockHide;
    }

    private void OnDoorLockShow()
    {
        _camera.enabled = false;
    }

    private void OnDoorLockHide()
    {
        _camera.enabled = true;
    }

    private void OnDestroy()
    {
        _doorLockService.Show -= OnDoorLockShow;
        _doorLockService.Hide -= OnDoorLockHide;
    }
}