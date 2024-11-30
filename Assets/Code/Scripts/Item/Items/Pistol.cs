using UnityEngine;

public class Pistol : MonoBehaviour, IItem
{
    public ItemType ItemType => ItemType.Pistol;

    [SerializeField] GameObject _model;
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _firePoint;
    [SerializeField] int shootDamage = 10;


    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }


    public void Use()
    {
        Instantiate(_bullet, _firePoint.position, transform.rotation);
        //Attack();
        _animator.Play("Shoot");
    }

    public void Hide()
    {
        _animator.Play("Hide");
    }

    public void Disable()
    {
        _model.SetActive(false);
    }

    public void Show()
    {
        _model.SetActive(true);
        _animator.Play("Show");
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