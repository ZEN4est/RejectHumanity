using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static event Action<ItemSettings> PickUpItem;

    private IItem _itemInHand;

    private Dictionary<ItemType, IItem> _items;

    //private ItemService itemService;

    private void Start()
    {
        _items = GetComponentsInChildren<IItem>().ToDictionary(i => i.ItemType);

        ItemView.Click += OnChooseItem;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            PickUp();
    }

    private void PickUp()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Item"))
            {
                var settings = hit.collider.GetComponent<ItemSettingReference>()?.settings;
                PickUpItem?.Invoke(settings);
                Destroy(hit.transform.gameObject);
            }
        }
    }

    private void OnChooseItem(ItemType type)
    {
        if (type == ItemType.None)
        {
            _items.ToList().ForEach(kvp => kvp.Value.Hide());
            return;
        }

        _itemInHand = _items[type];

        _items.ToList().ForEach(kvp => kvp.Value.Hide());
        _itemInHand.Show();
    }

    private void OnClick()
    {
        if (_itemInHand != null)
            _itemInHand.Use();
    }

    private void OnDestroy()
    {
        ItemView.Click -= OnChooseItem;
    }
}
