using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemViewManager : MonoBehaviour
{
    private List<ItemView> _items;

    private void Start()
    {
        _items = GetComponentsInChildren<ItemView>().ToList();

        _items[0].Enable();

        ItemManager.PickUpItem += OnPickUpItem;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EnableItem(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) EnableItem(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) EnableItem(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) EnableItem(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5)) EnableItem(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6)) EnableItem(5);
    }

    private void OnPickUpItem(ItemSettings settings)
    {
        int i = 0;
        foreach (var item in _items)
        {
            if (item.itemSettings == null)
            {
                item.SetSettings(settings);
                EnableItem(i);
                break;
            }
            i++;
        }
    }

    private void EnableItem(int index)
    {
        DisableAllItems();

        if (_items.Count > index)
            _items[index].Enable();
    }

    private void DisableAllItems()
    {
        _items.ForEach(item => item.Disable());
    }
}