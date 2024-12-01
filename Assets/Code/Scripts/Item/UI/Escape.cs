using UnityEngine;

public class Escape : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        }
    }
}
