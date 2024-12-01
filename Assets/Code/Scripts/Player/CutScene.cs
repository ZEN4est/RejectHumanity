using System.Collections;
using System.Linq;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public Canvas canvas;
    public Animator cutscene;

    bool playerIsIn = false;
    bool done = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cutscene.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("E " + playerIsIn + " " + done);
            if(!done) {
                if(playerIsIn) {
                    done = true;
                    StartCoroutine(run());
                }
            }
        }
        
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag("Player")) {
            Debug.Log("enter");
            playerIsIn = true;
        }
    }

    private IEnumerator run() {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        enemies.ToList().ForEach(x => Destroy(x.gameObject));
        Canvas c = Instantiate(canvas);
        yield return new WaitForSeconds(1.5f);
        Destroy(FindAnyObjectByType<PlayerMovement>().gameObject);
        cutscene.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        Destroy(c.gameObject);
    }
}
