using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] int meleDamage = 10;
    [SerializeField] int shootDamage = 10;
    [SerializeField] float radius = 0.5f;
    [SerializeField] ItemType itemType = ItemType.Crowbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAttack() {
        switch(itemType) {
            case ItemType.Crowbar: meleAttack(); break;
            case ItemType.Pistol: shootAttack(); break;
            default: break;
        }
    }

    private void meleAttack() {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, transform.forward, 1);
        foreach(var h in hits) {
            if(h.collider.CompareTag("Enemy")) {
                h.collider.GetComponent<Enemy>()?.dealDamage(meleDamage);
                Debug.Log("mele");
            }
        }
    }

    private void shootAttack() {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) {
            if(hit.collider.CompareTag("Enemy")) {
                hit.collider.GetComponent<Enemy>()?.dealDamage(shootDamage);
                Debug.Log("shot");
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.80f);
        Gizmos.DrawSphere(transform.position + transform.forward, radius);
        Gizmos.DrawRay(transform.position, transform.forward*20);
    }
}
