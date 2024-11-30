using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class ItemView : MonoBehaviour
{
    public KeyCode key;
    [SerializeField] private Image _image;
    [SerializeField] private Image _background;

    private ItemSettings itemSettings;

    [Inject] private ItemService itemService;

    private void Start()
    {
        if (itemSettings != null)
            SetImage(itemSettings.sprite);

        itemService.Active += OnActiveItem;
        itemService.Add += OnAddItem;
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            itemService.SetActiveItem(key);
        }
    }

    private void OnActiveItem(KeyCode code, ItemSettings settings)
    {
        _background.color = code == key ? Color.blue : Color.white;
    }

    private void OnAddItem(KeyCode code, ItemSettings settings)
    {
        if (key == code) SetSettings(settings);
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

    private void OnDestroy()
    {
        itemService.Active -= OnActiveItem;
        itemService.Add -= OnAddItem;
    }
}