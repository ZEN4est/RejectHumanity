using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Configurations")]
    public Transform head;
    public Camera playerCamera;
    public float baseFov = 60f;
    public float baseHeight = 0.85f;
    public float walkBobbingRate = 0.75f;
    public float runBobbingRate = 1f;
    public float maxWalkBobbingOffset = 0.2f;
    public float maxRunBobbingOffset = 0.3f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        AdjustFov();
        ApplyHeadBobbing();
    }

    private void AdjustFov()
    {
        float fovOffset = (rb.linearVelocity.y < 0f) ? Mathf.Sqrt(Mathf.Abs(rb.linearVelocity.y)) : 0f;
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, baseFov + fovOffset, 0.25f);
    }

    private void ApplyHeadBobbing()
    {
        float speedMultiplier = Input.GetKey(KeyCode.LeftShift) ? runBobbingRate : walkBobbingRate;
        float bobbingOffset = Input.GetKey(KeyCode.LeftShift) ? maxRunBobbingOffset : maxWalkBobbingOffset;

        Vector3 targetPosition = Vector3.up * baseHeight + Vector3.up * (Mathf.PingPong(Time.time * speedMultiplier, bobbingOffset) - bobbingOffset * 0.5f);
        head.localPosition = Vector3.Lerp(head.localPosition, targetPosition, 0.1f);
    }
}
