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

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleInteraction();
        if(attachedObject is not null) {
            Vector3 targetPosition = transform.position + transform.forward * 4;
            Vector3 targetRotation = transform.eulerAngles;

            attachedObject.transform.position = Vector3.MoveTowards(attachedObject.transform.position, new Vector3(targetPosition.x, 1, targetPosition.z), Time.deltaTime * 10);
            attachedObject.transform.rotation = Quaternion.RotateTowards(attachedObject.transform.rotation, Quaternion.Euler(0, targetRotation.y, 0), Time.deltaTime * 300);
        }
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


// void Update()
// {
//     HandleInteraction();
//     if(attachedObject is not null) {
//         Vector3 targetPosition = transform.position + transform.forward * 4;
//         Vector3 targetRotation = transform.eulerAngles;

//         targetPosition = new Vector3(targetPosition.x, 1, targetPosition.z);
//         targetRotation = Quaternion.Euler(0, targetRotation.y, 0).eulerAngles;

//         posQueue.Append(targetPosition);
//         rotQueue.Append(targetRotation);
//         if(posQueue.Count() > 5) {
//             posQueue.Dequeue();
//         }
//         if(rotQueue.Count() > 5) {
//             rotQueue.Dequeue();
//         }

//         if(posQueue.Count() > 0 && rotQueue.Count() > 0) {
//             targetPosition = posQueue.ToList().Aggregate((sum, x) => sum + x) / 4;
//             targetRotation = rotQueue.ToList().Aggregate((sum, x) => sum + x) / 4;
//         }

//         attachedObject.transform.position = Vector3.MoveTowards(attachedObject.transform.position, targetPosition * Time.deltaTime * 10, 1);
//         attachedObject.transform.rotation = Quaternion.RotateTowards(attachedObject.transform.rotation, Quaternion.Euler(targetRotation * Time.deltaTime * 100), 5);

//     }
// }



// void Update()
// {
//     HandleInteraction();
//     if(attachedObject is not null) {
//         Vector3 targetPosition = transform.position + transform.forward * 4;
//         Vector3 targetRotation = transform.eulerAngles;

//         attachedObject.transform.position = Vector3.MoveTowards(attachedObject.transform.position, new Vector3(targetPosition.x, 1, targetPosition.z), Time.deltaTime * 10);
//         attachedObject.transform.rotation = Quaternion.RotateTowards(attachedObject.transform.rotation, Quaternion.Euler(0, targetRotation.y, 0), Time.deltaTime * 100);
//     }
// }