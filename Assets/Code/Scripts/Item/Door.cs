using UnityEngine;

public class Door : MonoBehaviour
{
    public ItemType key;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open() {
        transform.position = transform.position + new Vector3(0, 3, 0);
    }
}
