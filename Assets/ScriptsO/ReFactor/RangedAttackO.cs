using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RangedAttackO : AttackO
{
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private WandO _rangedWeapon;
    private float _nextAttackTime = 0f;
    public event Action OnAttack;

    private void Awake()
    {
        _rangedWeapon = GetComponentInChildren<WandO>();
    }

    public override void Attack(Transform target)
    {
        if (target == null)
            return;

        transform.LookAt(target);

        if (Time.time >= _nextAttackTime)
        {
            OnAttack?.Invoke();
            _rangedWeapon.Fire(target, _attackDamage);
            _nextAttackTime = Time.time + 1f / _attackSpeed;
        }
    }
}
