using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    [SerializeField] int health = 50;
    private PlayerMovement pm;
    private float followCooldown = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if(navMeshAgent is null) {
            Debug.LogError("Enemy should have navMeshAgent");
        }
        pm = FindAnyObjectByType<PlayerMovement>();
        StartCoroutine(changeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        followCooldown = followCooldown <= 0 ? 0 : followCooldown - Time.deltaTime;
        if(followCooldown == 0) {
            navMeshAgent.SetDestination(pm.transform.position);
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

    private IEnumerator changeDirection() {
        yield return new WaitForSeconds(Random.Range(5, 10));
        followCooldown = 5;
        navMeshAgent?.SetDestination(new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10)));
        navMeshAgent.speed = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(5);
        StartCoroutine(changeDirection());
    }

    private IEnumerator tmpDamage() {
        transform.localScale = new Vector3(1, 1.2f, 1);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(1, 1, 1);

    }
}
