using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal.Internal;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    [SerializeField] int health = 50;
    private PlayerMovement pm;
    private float followCooldown = 0;
    private float shootCooldown = 0;
    private Animator animator;

    [Space(10)]
[SerializeField] private float followMinTime = 10;
[SerializeField] private float followMaxTime = 20;
[SerializeField] private float distractedTime = 5;
[SerializeField] private float minSpeed = 0.5f;
[SerializeField] private float maxSpeed = 1.5f;
[SerializeField] private float shootMinCoolDown = 3f;
[SerializeField] private float shootMaxCoolDown = 6f;
[Space(10)]
[SerializeField] private BulletController _bullet;
[SerializeField] private float forwardOffset = 0.2f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if(navMeshAgent is null) {
            Debug.LogError("Enemy should have navMeshAgent");
        }
        if(animator is null) {
            Debug.LogError("Enemy should have animator");
        }
        pm = FindAnyObjectByType<PlayerMovement>();
        StartCoroutine(changeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        followCooldown = followCooldown <= 0 ? 0 : followCooldown - Time.deltaTime;
        shootCooldown = shootCooldown <= 0 ? 0 : shootCooldown - Time.deltaTime;
        if(followCooldown == 0) {
            navMeshAgent.SetDestination(pm.transform.position);
        }
        tryShoot();
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
        yield return new WaitForSeconds(Random.Range(followMinTime, followMaxTime));
        followCooldown = distractedTime;
        navMeshAgent?.SetDestination(new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10)));
        navMeshAgent.speed = Random.Range(minSpeed, maxSpeed);
        yield return new WaitForSeconds(5);
        StartCoroutine(changeDirection());
    }

    private IEnumerator tmpDamage() {
        transform.localScale = new Vector3(1, 1.2f, 1);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(1, 1, 1);

    }

    private void tryShoot() {
        if(shootCooldown <= 0) {
            Ray ray = new Ray(transform.position, FindAnyObjectByType<PlayerMovement>().transform.position - transform.position);
            RaycastHit hit;
            PlayerMovement pm = FindAnyObjectByType<PlayerMovement>();
            Debug.DrawRay(transform.position, pm.transform.position - transform.position);
            if(Physics.Raycast(ray, out hit)) {
                if(LayerMask.LayerToName(hit.collider.gameObject.layer) == "Player") {
                    StartCoroutine(shoot(transform.rotation, ray.direction));
                }
            }
        }
    }

    private IEnumerator shoot(Quaternion qat, Vector3 dir) {
        shootCooldown = 8;
        animator.SetTrigger("Attack");
        followCooldown = 2;
        goTo(transform.position);
        yield return new WaitForSeconds(2f);
        Instantiate(_bullet, transform.position+new Vector3(0,1,0)+dir*forwardOffset, qat);
    }
}
