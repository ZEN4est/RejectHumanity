using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Crowbar : MonoBehaviour, IItem
{
    [SerializeField] int meleDamage = 10;
    [SerializeField] float radius = 0.5f;
    [SerializeField] GameObject _model;

    public ItemType ItemType => ItemType.Crowbar;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Use()
    {
        _animator.Play("Hit");
        Attack();
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
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, transform.forward, 1);
        foreach (var h in hits)
        {
            if (h.collider.CompareTag("Enemy"))
            {
                h.collider.GetComponent<Enemy>()?.dealDamage(meleDamage);
            }
        }
    }
}