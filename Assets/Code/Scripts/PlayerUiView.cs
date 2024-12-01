using UnityEngine;
using Zenject;

public class PlayerUiView : MonoBehaviour
{
    [SerializeField] GameObject playerUi;
    [Inject] private UiService _uiService;

    private void Start()
    {
        _uiService.SetUi(UiType.Player);

        _uiService.Change += OnUiChange;
    }

    private void OnUiChange(UiType type)
    {
        playerUi.SetActive(type == UiType.Player);

        if (type == UiType.Player)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnDestroy()
    {
        _uiService.Change -= OnUiChange;
    }
}