using UnityEngine;

public class Mirror : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }
}
