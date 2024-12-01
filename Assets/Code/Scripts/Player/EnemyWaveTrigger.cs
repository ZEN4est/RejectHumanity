using System.Linq;
using UnityEngine;

public class EnemyWaveTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter() {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        enemies.ToList().ForEach(x => x.Activate());
    }
}
