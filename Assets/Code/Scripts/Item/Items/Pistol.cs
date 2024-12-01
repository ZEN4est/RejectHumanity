using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Pistol : MonoBehaviour
{
    public UnityEvent shot;

    public ItemType ItemType => ItemType.Pistol;

    [SerializeField] GameObject _model;

    [SerializeField] BulletController _bullet;
    [SerializeField] Transform _firePoint;
    [SerializeField] int shootDamage = 10;
    [SerializeField] private Animator _animator;

    [Inject] private ItemService _itemService;

    private void Start()
    {
        _itemService.Active += OnActiveItem;
        _itemService.Use += OnUseItem;
    }

    private void OnActiveItem(KeyCode code, ItemSettings settings)
    {
        if (settings != null && settings.type == ItemType)
            _model.SetActive(true);
        else
            _model.SetActive(false);
    }

    private void OnUseItem(KeyCode code, ItemSettings settings)
    {
        if (settings != null && ItemType == settings.type)
        {
            BulletController.Create(_bullet, transform.parent.position, transform.parent.rotation, transform.parent.gameObject, 20);
            _animator.Play("Shoot");
            shot?.Invoke();
            //Attack();
        }
    }

    public void Hide()
    {
        _model.SetActive(false);
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

    private void OnDestroy()
    {
        _itemService.Active -= OnActiveItem;
        _itemService.Use -= OnUseItem;
    }
}