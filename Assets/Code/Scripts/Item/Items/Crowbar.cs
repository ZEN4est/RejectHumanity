using UnityEngine;
using UnityEngine.Events;
using Zenject;

[RequireComponent(typeof(Animator))]
public class Crowbar : MonoBehaviour
{
    public UnityEvent hit;

    [SerializeField] int meleDamage = 10;
    [SerializeField] float radius = 0.5f;
    [SerializeField] GameObject _model;
    [SerializeField] private Animator _animator;

    public ItemType ItemType => ItemType.Crowbar;
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
            _animator.Play("Hit");
            hit?.Invoke();
            Attack();
        }
    }

    public void Hide()
    {
        _model.SetActive(false);
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

    private void OnDestroy()
    {
        _itemService.Active -= OnActiveItem;
        _itemService.Use -= OnUseItem;
    }
}