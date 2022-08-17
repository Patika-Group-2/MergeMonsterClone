using UnityEngine;
using System;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] protected int _attackDamage;
    [SerializeField] protected float _attackSpeed;
    protected float _nextAttackTime = 0f;
    public event Action OnAttack;

    public abstract void AttackTarget(Transform target);
    public void CallOnAttack()
    {
        OnAttack?.Invoke();
    }
}
