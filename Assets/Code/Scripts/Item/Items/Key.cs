using System.Linq;
using Unity.Collections;
using UnityEngine;
using Zenject;

public class Key : MonoBehaviour
{
    public ItemType ItemType => ItemType.key2;

    [Inject] private ItemService _itemService;


    private void Start()
    {
        _itemService.Active += OnActiveItem;
        _itemService.Use += OnUseItem;
    }

    private void OnActiveItem(KeyCode code, ItemSettings settings)
    {
    }

    private void OnUseItem(KeyCode code, ItemSettings settings)
    {
        // RaycastHit[] hits = Physics.SphereCastAll(transform.position, 2f, Vector3.up);
        // RaycastHit? doorHit = hits.ToList().FirstOrDefault(x => x.collider.GetComponent<Door>() != null);
        // if(doorHit is null) {
        //     return;
        // }
        // RaycastHit doorHit1 = hits.ToList().FirstOrDefault(x => x.collider.GetComponent<Door>() != null);
        // Door door = doorHit1.collider.GetComponent<Door>();
        // if(door is not null && ItemType == door.key) {
        //     door.Open();
        // }

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 2f, Vector3.up);
        if (hits.ToList().Select(x => x.collider.GetComponent<Door>()).FirstOrDefault() is Door door && door.key == ItemType)
        {
            door.Open();
        }
    }

    private void OnDestroy()
    {
        _itemService.Active -= OnActiveItem;
        _itemService.Use -= OnUseItem;
    }
}
