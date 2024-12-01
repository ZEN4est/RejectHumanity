using System.Collections.Generic;
using UnityEngine;

public class HandAnimationController : MonoBehaviour
{
    private int _kills = 0;

    [SerializeField] List<Animator> _hands = new();

    private void Start()
    {
        Enemy.Death += OnEnemyDeath;
    }

    private void OnEnemyDeath()
    {
        if (_kills < _hands.Count)
            _hands[_kills].Play("Show");

        _kills++;
    }

    private void OnDestroy()
    {
        Enemy.Death -= OnEnemyDeath;
    }
}