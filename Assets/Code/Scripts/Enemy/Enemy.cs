using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    [SerializeField] int health = 50;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if(navMeshAgent is null) {
            Debug.LogError("Enemy should have navMeshAgent");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if(Physics.Raycast(Camera.allCameras[0].ScreenPointToRay(Input.mousePosition), out hit)) {
                goTo(hit.point);
            }
        }
    }

    public void goTo(Vector3 pos) {
        if(navMeshAgent) {
            navMeshAgent?.SetDestination(pos);
        }
    }

    public void dealDamage(int damage) {
        health -= damage;
        StartCoroutine(tmpDamage());
        if(health <= 0) {
            Destroy(gameObject);
        }
    }

    private IEnumerator tmpDamage() {
        transform.localScale = new Vector3(1, 1.2f, 1);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(1, 1, 1);

    }
}
