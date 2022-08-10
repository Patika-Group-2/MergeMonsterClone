using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedO : CharacterO
{
    [SerializeField] private FindTargetO _findTarget;
    [SerializeField] private AttackO _attack;
    [SerializeField] private HealthO _health;

    private void Awake()
    {
        _health = GetComponent<HealthO>();
        _findTarget = GetComponent<FindTargetO>();
        _attack = GetComponent<AttackO>();
    }
    private void Update()
    {
        if (GameManagerO.Instance.GameIsRunning == false)
            return;
        FindClosestTarget();
        Attack(); 
    }
    public override void FindClosestTarget()
    {
        _findTarget.FindTarget();
    }

    public override void Attack()
    {
        _attack.Attack(_findTarget.ClosestTarget);
    }

    public override void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    private void OnDestroy()
    {
        _findTarget.DelistOnDestroy();
    }

}
