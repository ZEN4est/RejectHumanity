using System;
using UnityEngine;
using Zenject;

public class LaserTrigger : MonoBehaviour
{
    [SerializeField] private DoorController _doorController;

    [Inject] private DoorLockService _doorLockService;

    public event Action Triggered;
    public void Trigger()
    {
        _doorLockService.UnlockDoor(_doorController.Id);
        Triggered?.Invoke();
    }
}
