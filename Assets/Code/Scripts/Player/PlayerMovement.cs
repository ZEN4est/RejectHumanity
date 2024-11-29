using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Configurations")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpSpeed;

    private Rigidbody rb;
    private bool isGrounded;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        RotatePlayer();
        MovePlayer();
        HandleJump();
    }

    private void RotatePlayer()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);
    }

    private void MovePlayer()
    {
        Vector3 newVelocity = rb.linearVelocity;
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        newVelocity.x = Input.GetAxis("Horizontal") * speed;
        newVelocity.z = Input.GetAxis("Vertical") * speed;
        rb.linearVelocity = transform.TransformDirection(newVelocity);
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpSpeed, rb.linearVelocity.z);
            isJumping = true;
        }
    }

    private void OnCollisionStay(Collision col)
    {
        isGrounded = true;
        isJumping = false;
    }

    private void OnCollisionExit(Collision col)
    {
        isGrounded = false;
    }
}
