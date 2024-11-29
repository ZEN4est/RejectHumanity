using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed = 5f;

    private Rigidbody _rb;

    private float _horizontal, _vertical;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {


        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 movement = transform.right * _horizontal + transform.forward * _vertical;
        _rb.linearVelocity = new Vector3(movement.x * _speed, _rb.linearVelocity.y, movement.z * _speed);
    }
}
