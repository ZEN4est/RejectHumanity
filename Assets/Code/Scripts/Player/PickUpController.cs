using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public Transform hands;
    public float pickupDistance;

    private Transform attachedObject;
    private float attachedDistance;

    void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (attachedObject != null)
            {
                DropObject();
            }
            else
            {
                TryPickUpObject();
            }
        }
    }

    private void TryPickUpObject()
    {
        if (Physics.Raycast(hands.position, hands.forward, out RaycastHit hit, pickupDistance))
        {
            if (hit.transform.CompareTag("Pickable"))
            {
                attachedObject = hit.transform;
                attachedObject.SetParent(transform);
                attachedObject.position = hands.position;

                attachedDistance = Vector3.Distance(attachedObject.position, hands.position);

                if (attachedObject.GetComponent<Rigidbody>())
                    attachedObject.GetComponent<Rigidbody>().isKinematic = true;

                if (attachedObject.GetComponent<Collider>())
                    attachedObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void DropObject()
    {
        attachedObject.SetParent(null);

        if (attachedObject.GetComponent<Rigidbody>())
            attachedObject.GetComponent<Rigidbody>().isKinematic = false;

        if (attachedObject.GetComponent<Collider>())
            attachedObject.GetComponent<Collider>().enabled = true;

        attachedObject = null;
    }
}
