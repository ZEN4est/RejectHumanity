using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemService
{
    public event Action<KeyCode, ItemSettings> Add;
    public event Action<KeyCode, ItemSettings> Active;
    public event Action<KeyCode, ItemSettings> Use;

    private Dictionary<KeyCode, ItemSettings> _items = new()
    {
        {KeyCode.Alpha1, null},
        {KeyCode.Alpha2, null},
        {KeyCode.Alpha3, null},
        {KeyCode.Alpha4, null},
        {KeyCode.Alpha5, null},
        {KeyCode.Alpha6, null},
    };

    private KeyCode _activeItem = KeyCode.Alpha1;

    public void AddItem(ItemSettings settings)
    {
        KeyCode? freeKey = null;
        foreach (var (key, value) in _items)
        {
            if (value == null)
            {
                freeKey = key;
                break;
            }
        }
        if (freeKey != null)
        {
            _items[freeKey.Value] = settings;
            Add?.Invoke(freeKey.Value, settings);
        }
    }

    public void SetActiveItem(KeyCode code)
    {
        if (_items.ContainsKey(code))
        {
            _activeItem = code;
            Active?.Invoke(_activeItem, _items[_activeItem]);
        }
    }

    public void UseItem()
    {
        Use?.Invoke(_activeItem, _items[_activeItem]);
    }
}