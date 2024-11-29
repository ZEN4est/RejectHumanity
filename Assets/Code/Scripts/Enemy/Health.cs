using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;

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
