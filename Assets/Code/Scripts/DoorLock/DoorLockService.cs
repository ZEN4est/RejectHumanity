using System;
using UnityEngine;

public class DoorLockService
{
    public event Action Show;
    public event Action Hide;
    public event Action<Guid> DoorUnlocked;

    public string Secret { get; private set; }
    public Guid? DoorId { get; private set; }

    public void ShowDoorLock(Guid doorId, string secret)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        Secret = secret;
        DoorId = doorId;
        Show?.Invoke();
    }

    public void HideDoorLock()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Secret = null;
        DoorId = null;
        Hide?.Invoke();
    }

    public void UnlockDoor()
    {
        DoorUnlocked?.Invoke(DoorId.Value);
    }

    public void UnlockDoor(Guid doorId)
    {
        DoorUnlocked?.Invoke(doorId);
    }
}