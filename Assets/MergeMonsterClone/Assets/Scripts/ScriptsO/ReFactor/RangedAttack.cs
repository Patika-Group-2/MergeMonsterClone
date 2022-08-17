using UnityEngine;
using System;
public class RangedAttack : Attack
{
    [SerializeField] private Wand _rangedWeapon;
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
            CallOnAttack();
            _rangedWeapon.Fire(target, _attackDamage);
            _nextAttackTime = Time.time + 1f / _attackSpeed;
        }
    }
}
