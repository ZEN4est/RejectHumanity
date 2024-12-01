using System.Linq;
using UnityEngine;



public class EnemyWaveTrigger : MonoBehaviour
{
    private bool isPlaying = false;
    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter() {
        if (!isPlaying) { 
        FindAnyObjectByType<PlayerMovement>()?.GetComponent<AudioSource>()?.Stop();
        audioSource.Play();
        audioSource.volume = 0.5f;
        }
        isPlaying = true;
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        enemies.ToList().ForEach(x => x.Activate());
        
    }
}
