using UnityEngine;

public class Pistol : MonoBehaviour, IItem
{
    public ItemType ItemType => ItemType.Pistol;

    [SerializeField] GameObject _model;
    [SerializeField] int shootDamage = 10;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Use()
    {
        Attack();
        animator.Play("Shoot");
    }

    public void Disable()
    {
        _model.SetActive(false);
    }

    public void Enable()
    {
        _model.SetActive(true);
    }

    private void Attack()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>()?.dealDamage(shootDamage);
            }
        }
    }


}