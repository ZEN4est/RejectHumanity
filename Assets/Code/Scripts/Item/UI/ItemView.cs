using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public static event Action<ItemType> Click;
    public KeyCode key;


    private int index = 0;

    public ItemSettings itemSettings;
    [SerializeField] private Image _image;
    [SerializeField] private Image _background;
    [SerializeField] private Button _button;

    //private ItemService itemService;

    private void Start()
    {
        //index = transform.parent.GetComponentsInChildren<ItemView>().ToList().IndexOf(this);

        if (itemSettings != null)
            SetImage(itemSettings.sprite);

        _button.onClick.AddListener(OnClick);

        //itemService.Add += OnAddItem;
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            OnClick();
        }
    }

    // private void OnAddItem(ItemSettings settings)
    // {
    //     if (itemService.items.IndexOf(settings) == index)
    //     {
    //         itemSettings = settings;
    //         Enable();
    //     }
    //     else
    //     {
    //         Disable();
    //     }
    // }

    public void Disable()
    {
        _background.color = Color.white;
    }

    public void Enable()
    {
        _background.color = Color.blue;
        OnClick();
    }

    public void SetSettings(ItemSettings settings)
    {
        if (settings == null) throw new ArgumentNullException("settings in SetSettings of ItemView is null");

        itemSettings = settings;
        SetImage(itemSettings.sprite);
    }

    private void SetImage(Sprite sprite)
    {
        _image.enabled = true;
        _image.sprite = sprite;
    }

    private void OnClick()
    {
        Click?.Invoke(itemSettings == null ? ItemType.None : itemSettings.type);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}