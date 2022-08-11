using System;
using UnityEngine;

public class MeleeAttack : Attack
{
    public event Action OnAttack;
    private void Awake()
    {
        GetComponent<Movement>().OnTargetClose += AttackTarget;
    }

    public override void AttackTarget(Transform target)
    {
        transform.LookAt(target);

        if (Time.time >= _nextAttackTime)
        {
            OnAttack?.Invoke();
            Character targetChar = target.gameObject.
                GetComponent<Character>();

            targetChar.TakeDamage(_attackDamage);
            _nextAttackTime = Time.time + 1f / _attackSpeed;
        }
    }
}
