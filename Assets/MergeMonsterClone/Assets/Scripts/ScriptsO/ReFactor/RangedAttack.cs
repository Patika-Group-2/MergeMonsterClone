using UnityEngine;
using System;
public class RangedAttack : Attack
{
    //reference to ranged weapon
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
            //call event
            CallOnAttack();
            //fire ranged weapon
            _rangedWeapon.Fire(target, _attackDamage);
            _nextAttackTime = Time.time + 1f / _attackSpeed;
        }
    }
}