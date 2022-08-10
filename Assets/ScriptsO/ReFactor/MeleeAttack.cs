using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AttackO
{
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackSpeed;
    private float _nextAttackTime = 0f;
    public event Action OnAttack;

    private void Awake()
    {
        GetComponent<MovementO>().OnTargetClose += Attack;
    }

    public override void Attack(Transform target)
    {
        transform.LookAt(target);

        if (Time.time >= _nextAttackTime)
        {
            OnAttack?.Invoke();
            CharacterO targetChar = target.gameObject.
                GetComponent<CharacterO>();

            targetChar.TakeDamage(_attackDamage);
            _nextAttackTime = Time.time + 1f / _attackSpeed;
        }
    }
}
