using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] protected int _attackDamage;
    [SerializeField] protected float _attackSpeed;
    protected float _nextAttackTime = 0f;
    public abstract void AttackTarget(Transform target);
}
