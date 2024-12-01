using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PickUpController : MonoBehaviour
{
    public UnityEvent pickUp;

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
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, new Vector3(2, 2, 2), transform.forward);
        GameObject obj = hits.Select(x => x.collider.gameObject).FirstOrDefault(x => x.CompareTag("Pickable"));
        if (obj is not null)
        {
            attachedObject = obj.transform;
            attachedObject.SetParent(transform);
            attachedObject.position = hands.position;

            attachedDistance = Vector3.Distance(attachedObject.position, hands.position);

            if (attachedObject.GetComponent<Rigidbody>())
                attachedObject.GetComponent<Rigidbody>().isKinematic = true;

            if (attachedObject.GetComponent<Collider>())
                attachedObject.GetComponent<Collider>().isTrigger = true;
            Material m = attachedObject.GetComponent<MeshRenderer>().materials[0];
            m.color = new Color(m.color.r, m.color.g, m.color.b, 0.7f);

            pickUp?.Invoke();
        }
    }

    private void DropObject()
    {
        attachedObject.SetParent(null);

        if (attachedObject.GetComponent<Rigidbody>())
            attachedObject.GetComponent<Rigidbody>().isKinematic = false;

        if (attachedObject.GetComponent<Collider>())
            attachedObject.GetComponent<Collider>().isTrigger = false;
        Material m = attachedObject.GetComponent<MeshRenderer>().materials[0];
        m.color = new Color(m.color.r, m.color.g, m.color.b, 1f);

        attachedObject = null;
    }
}
