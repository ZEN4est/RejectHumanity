using UnityEngine;
using Zenject;

public class DoorLockController : MonoBehaviour
{
    private Outline _outline;
    private bool _isFacingDoor = false;
    [SerializeField] private string _secret = "2021";

    [Inject] private DoorLockService _doorLockService;

    private void Start()
    {
        _outline = GetComponent<Outline>();
    }

    public void Open()
    {
        _outline.OutlineWidth = 0;
    }

    private void Update()
    {
        if (_isFacingDoor && Input.GetKeyDown(KeyCode.E))
            _doorLockService.ShowDoorLock(_secret);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interact"))
        {
            _outline.OutlineWidth = 10;
            _isFacingDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interact"))
        {
            _outline.OutlineWidth = 0;
            _isFacingDoor = false;
        }
    }

}