using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BulletController : MonoBehaviour
{
    public UnityEvent explosion;

    [SerializeField] GameObject _explosion;
    [SerializeField] int _shootDamage = 10;
    private bool _isExplosion = false;
    private int _distanceToDeath = 30;
    private Vector3 _startPos;
    private GameObject sender;

    private void Start()
    {
        
    }


    private void Update()
    {
        if (Vector3.Distance(_startPos, transform.position) > _distanceToDeath)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnEnter(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        OnEnter(other.collider.gameObject);
    }

    private void OnEnter(GameObject obj) {
        if(sender is null || ((obj.CompareTag("Player") || obj.CompareTag("Enemy")) && obj != sender)) {
            if(obj.CompareTag("Player")) {
                FindAnyObjectByType<PlayerMovement>()?.dealDamage(_shootDamage);
            }
            else if(obj.CompareTag("Enemy")) {
                obj.transform.GetComponent<Enemy>()?.dealDamage(_shootDamage);
            }
            GetComponent<Collider>().enabled = false;
            if (_isExplosion == false) {
                StartCoroutine(Explode());
                explosion?.Invoke();
                _isExplosion = true;
            }
        }
    }

    IEnumerator Explode()
    {
        Instantiate(_explosion, transform);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    public static BulletController Create(BulletController pref, Vector3 pos, Quaternion rot, GameObject sender, float speed) {
        BulletController bc = Instantiate(pref, pos, rot);
        bc.GetComponent<Rigidbody>()?.AddForce(bc.transform.forward * speed, ForceMode.Impulse);
        bc.transform.Rotate(Vector3.up, 90);
        bc._startPos = bc.transform.position;
        bc.sender = sender;
        return bc;
    }
}