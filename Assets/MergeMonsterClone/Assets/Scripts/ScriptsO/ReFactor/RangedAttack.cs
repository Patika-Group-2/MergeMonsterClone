using UnityEngine;
using System;
public class RangedAttack : Attack
{
    [SerializeField] private Wand _rangedWeapon;
    public event Action OnAttack;
    private void Awake()
    {
        _rangedWeapon = GetComponentInChildren<Wand>();
    }

    public override void AttackTarget(Transform target)
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
