using System.Linq;
using Unity.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] float radius = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAttack() {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, Vector3.forward);
        foreach(var h in hits) {
            if(!h.collider.CompareTag("Player")) {
                h.collider.GetComponent<Health>()?.dealDamage(damage);
            }
        }
    }
}
