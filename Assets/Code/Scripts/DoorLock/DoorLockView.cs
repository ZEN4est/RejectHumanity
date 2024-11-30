using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DoorLockView : MonoBehaviour
{
    [SerializeField] private GameObject _canvas, _camera;
    [SerializeField] private Image _red, _green;

    [Inject] private DoorLockService _doorLockService;

    private int _clickCounter = 0;
    private string _typedNumber = "";

    private void Start()
    {
        _doorLockService.Show += OnDoorLockShow;
        DoorLockButtonView.Click += OnDoorLockButtonClick;
    }

    private void OnDoorLockButtonClick(string text)
    {
        _clickCounter++;
        _typedNumber += text;

        if (_clickCounter == 4)
        {
            StartCoroutine(CloseDoorLock());
        }
    }

    IEnumerator CloseDoorLock()
    {
        if (_typedNumber == _doorLockService.Secret)
        {
            _green.enabled = true;
            _doorLockService.UnlockDoor();
        }
        else
        {
            _red.enabled = true;
        }

        yield return new WaitForSeconds(1);

        _red.enabled = false;
        _green.enabled = false;

        _doorLockService.HideDoorLock();

        _canvas.SetActive(false);
        _camera.SetActive(false);
    }

    private void OnDoorLockShow()
    {
        _canvas.SetActive(true);
        _camera.SetActive(true);
        _clickCounter = 0;
        _typedNumber = "";
    }

    private void OnDestroy()
    {
        _doorLockService.Show -= OnDoorLockShow;
        DoorLockButtonView.Click -= OnDoorLockButtonClick;
    }
}