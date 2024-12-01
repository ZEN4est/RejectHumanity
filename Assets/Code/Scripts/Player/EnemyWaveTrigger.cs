using System.Linq;
using UnityEngine;



public class EnemyWaveTrigger : MonoBehaviour
{
    private bool isPlaying = false;
    public AudioClip AudioClip;
    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource= FindAnyObjectByType<PlayerMovement>().GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter() {
        if (!isPlaying) { 
        audioSource.clip = AudioClip;
        audioSource.Play();
        audioSource.volume = 0.5f;
        }
        isPlaying = true;
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        enemies.ToList().ForEach(x => x.Activate());
        
    }
}
