using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthO : MonoBehaviour
{
    [SerializeField] private int _maxHitPoint = 100;
    [SerializeField] private int _hitPoints;
    [SerializeField] private HealthBar _healthBar;

    private void Awake()
    {
        _hitPoints = _maxHitPoint;
        _healthBar = GetComponentInChildren<HealthBar>();
    }

    public void TakeDamage(int damage)
    {
        _hitPoints -= damage;
        float percentage = (float)_hitPoints / (float)_maxHitPoint;
        StartCoroutine(_healthBar.ChangeHealthBar(percentage));

        if (_hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
