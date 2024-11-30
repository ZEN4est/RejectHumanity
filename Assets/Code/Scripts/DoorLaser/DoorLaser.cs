using UnityEngine;

public class DoorLaser : MonoBehaviour
{
    [SerializeField] LaserTrigger trigger;

    private Animator animator;
    private BoxCollider boxCollider;


    void Start() {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        if(trigger is null) {
            Debug.LogError(name + " has no laser assigned to it");
        }
        if(animator is null) {
            Debug.LogError(name + " has no animator assigned to it");
        }
        if(boxCollider is null) {
            Debug.LogError(name + " has no boxCollider assigned to it");
        }
        trigger.Triggered += open;
    }

    
    private void open() {
        animator.SetTrigger("DoorOpen");
        boxCollider.enabled = false;
    }
}
