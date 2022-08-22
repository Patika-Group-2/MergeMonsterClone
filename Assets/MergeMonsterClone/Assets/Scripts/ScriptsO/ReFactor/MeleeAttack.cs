using System;
using UnityEngine;

public class MeleeAttack : Attack
{
    private void Awake()
    {
        GetComponent<Movement>().OnTargetClose += AttackTarget;
    }

    public override void AttackTarget(Transform target)
    {
        transform.LookAt(target);

        if (Time.time >= _nextAttackTime)
        {
            //call event
            CallOnAttack();

            Character targetChar = target.gameObject.
                GetComponent<Character>();

            targetChar.TakeDamage(_attackDamage);
            _nextAttackTime = Time.time + 1f / _attackSpeed;
        }
    }
}
