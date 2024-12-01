using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorLockButtonView : MonoBehaviour
{
    public static event Action<string> Click;

    private Button _button;
    private TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        _button = GetComponent<Button>();
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Click?.Invoke(_textMeshPro.text);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}