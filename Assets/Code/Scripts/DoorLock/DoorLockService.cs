using System;
using UnityEngine;

public class DoorLockService
{
    public event Action Show;
    public event Action Hide;
    public event Action DoorUnlocked;

    private string _secret = null;
    public string Secret => _secret;

    public void ShowDoorLock(string secret)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        _secret = secret;
        Show?.Invoke();
    }

    public void HideDoorLock()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _secret = null;
        Hide?.Invoke();
    }

    public void UnlockDoor()
    {
        DoorUnlocked?.Invoke();
    }
}