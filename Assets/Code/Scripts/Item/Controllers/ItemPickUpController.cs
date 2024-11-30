using UnityEngine;
using Zenject;

public class ItemPickUpController : MonoBehaviour
{
    [Inject] private ItemService _itemService;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            PickUp();
    }

    private void OnClick()
    {
        _itemService.UseItem();
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
                _itemService.AddItem(settings);
                Destroy(hit.transform.gameObject);
            }
        }
    }
}