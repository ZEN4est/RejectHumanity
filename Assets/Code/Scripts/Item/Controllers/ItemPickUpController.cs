using System.Linq;
using UnityEngine;
using Zenject;

public class ItemPickUpController : MonoBehaviour
{
    [Inject] private ItemService _itemService;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            PickUp();

        if (Input.GetMouseButtonDown(0))
            _itemService.UseItem();
    }

    private void PickUp()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, new Vector3(2, 2, 2), transform.forward);
        GameObject obj = hits.Select(x => x.collider.gameObject).FirstOrDefault(x => x.CompareTag("Item"));
        if (obj is not null)
        {
            var settings = obj.GetComponent<ItemSettingReference>()?.settings;
            _itemService.AddItem(settings);
            Destroy(obj);
        }
    }
}