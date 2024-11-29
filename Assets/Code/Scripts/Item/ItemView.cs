using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public static event Action<ItemType> Click;

    [SerializeField] private ItemSettings _itemSettings;
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    private void Start()
    {
        _image.sprite = _itemSettings.sprite;
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Click?.Invoke(_itemSettings.type);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}