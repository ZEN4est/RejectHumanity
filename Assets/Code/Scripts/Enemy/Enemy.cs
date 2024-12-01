using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal.Internal;
using Zenject.SpaceFighter;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    [SerializeField] int health = 50;
    private PlayerMovement pm;
    private float followCooldown = 0;
    private float shootCooldown = 0;
    private Animator animator;

    private bool active = false;

        [Space(10)]
    [SerializeField] private float minSpeed = 0.5f;
    [SerializeField] private float maxSpeed = 1.5f;
    [SerializeField] private float shootMinCoolDown = 3f;
    [SerializeField] private float shootMaxCoolDown = 6f;
    [Space(10)]
    [SerializeField] private BulletController _bullet;
    [SerializeField] private float forwardOffset = 0.2f;
    [SerializeField] private float upOffset = 3f;


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
    }

    // Update is called once per frame
    void Update()
    {
        if(active) {
            followCooldown = followCooldown <= 0 ? 0 : followCooldown - Time.deltaTime;
            shootCooldown = shootCooldown <= 0 ? 0 : shootCooldown - Time.deltaTime;
            if(followCooldown == 0) {
                navMeshAgent.SetDestination(pm.transform.position);
            }
            tryShoot();
        }
    }

    public void goTo(Vector3 pos) {
        if(navMeshAgent) {
            navMeshAgent?.SetDestination(pos);
        }
    }

    public void dealDamage(int damage) {
        health -= damage;
        if(health <= 0) {
            StartCoroutine(die());
        }
    }

    private void tryShoot() {
        if(shootCooldown <= 0 && followCooldown <= 0) {
            PlayerMovement pm = FindAnyObjectByType<PlayerMovement>();
            Ray ray = new Ray(transform.position+new Vector3(0,upOffset,0), pm.transform.position - transform.position - new Vector3(0, upOffset, 0));
            RaycastHit hit;
            Debug.DrawRay(transform.position+new Vector3(0, upOffset, 0), pm.transform.position - transform.position - new Vector3(0, upOffset, 0));
            if(Physics.Raycast(ray, out hit)) {
                if(LayerMask.LayerToName(hit.collider.gameObject.layer) == "Player") {
                    StartCoroutine(shoot(ray.direction));
                }
            }
        }
    }

    private IEnumerator shoot(Vector3 dir) {
        shootCooldown = Random.Range(shootMinCoolDown, shootMaxCoolDown);
        animator.SetTrigger("Attack");
        followCooldown = 2;
        goTo(transform.position);
        yield return new WaitForSeconds(2f);
        BulletController.Create(_bullet, transform.position+new Vector3(0, upOffset, 0), Quaternion.LookRotation(dir), gameObject, 10);
    }

    private IEnumerator die() {
        animator.SetTrigger("Die");
        followCooldown = 5;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    public void Activate() {
        active = true;
    }
}
