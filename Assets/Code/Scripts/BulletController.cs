using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] GameObject _explosion;
    [SerializeField] int _shootDamage = 10;
    [SerializeField] Rigidbody rb;
    private bool _isExplosion = false;
    private int _distanceToDeath = 30;
    private Vector3 _startPos;

    private void Start()
    {
        rb.AddForce(transform.forward * 30, ForceMode.Impulse);
        transform.Rotate(Vector3.up, 90);
        _startPos = transform.position;
    }


    private void Update()
    {
        if (Vector3.Distance(_startPos, transform.position) > _distanceToDeath)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            other.transform.GetComponent<Enemy>()?.dealDamage(_shootDamage);
        }

        if (_isExplosion == false)
        {
            StartCoroutine(Explode());
            _isExplosion = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            other.transform.GetComponent<Enemy>()?.dealDamage(_shootDamage);
        }

        if (_isExplosion == false)
        {
            StartCoroutine(Explode());
            _isExplosion = true;
        }
    }

    IEnumerator Explode()
    {
        Instantiate(_explosion, transform);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}