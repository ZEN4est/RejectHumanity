using System;
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
    [SerializeField] GameObject body;
    private float rotationX = 0;
    private float rotationY = 0;

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
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rotationY += mouseX;
        rotationX -= mouseY;
        
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        body.transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    private void MovePlayer()
    {
        Vector3 newVelocity = rb.linearVelocity;
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        newVelocity.x = Input.GetAxis("Horizontal") * speed;
        newVelocity.z = Input.GetAxis("Vertical") * speed;
        Vector3 dir =  transform.TransformDirection(newVelocity);
        rb.linearVelocity = new Vector3(dir.x, 0, dir.z);
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
