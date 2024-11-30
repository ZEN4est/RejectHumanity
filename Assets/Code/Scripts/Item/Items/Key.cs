using System.Linq;
using Unity.Collections;
using UnityEngine;

public class Key : MonoBehaviour, IItem
{
    public ItemType ItemType => ItemType.key2;

    public void Disable()
    {
        
    }

    public void Enable()
    {
        
    }

    public void Use()
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
        if(hits.ToList().Select(x => x.collider.GetComponent<Door>()).FirstOrDefault() is Door door && door.key == ItemType) {
            door.Open();
        }
        

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
