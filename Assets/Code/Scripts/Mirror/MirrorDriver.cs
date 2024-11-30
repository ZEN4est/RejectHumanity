using System.Linq;
using UnityEngine;

public class MirrorDriver : MonoBehaviour
{
    [SerializeField] int turnNumbers = 4;
    [SerializeField] Mirror mirror;
    private int index;
    private float offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = mirror.transform.eulerAngles.y;
        Debug.Log(offset);
        if(turnNumbers <= 0) {
            Debug.LogError("turnNumbers cannot be below or equal zero!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, 0.3f, Vector3.up);
            var playerHit = hits.FirstOrDefault(x => x.collider.GetComponent<PlayerMovement>() != null);
            if (playerHit.collider != null)
            {
                index ++;
                if(index >= turnNumbers) {
                    index = 0;
                }
                mirror.transform.rotation = Quaternion.Euler(0, offset + index * 360 / turnNumbers, 0);
            }
        }
    }

}