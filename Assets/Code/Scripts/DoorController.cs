using System;
using UnityEngine;
using Zenject;

public class DoorController : MonoBehaviour
{
    private Animator _animator;

    public Guid Id { get; private set; }

    [Inject] private DoorLockService _doorLockService;

    private void Awake()
    {
        Id = Guid.NewGuid();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _doorLockService.DoorUnlocked += OnDoorUnlocked;
    }

    private void OnDoorUnlocked(Guid doorId)
    {
        if (Id == doorId)
            _animator.Play("OpenDoor");
    }

    public void StayOpen()
    {
        transform.position = new Vector3(13.5100002f, 0.575546503f, -4.40999985f);
        transform.eulerAngles = new Vector3(0f, 272.42099f, 0f);
    }

    private void OnDestroy()
    {
        _doorLockService.DoorUnlocked -= OnDoorUnlocked;
    }
}