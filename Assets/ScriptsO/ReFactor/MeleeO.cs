using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeO : CharacterO
{
    [SerializeField] private FindTargetO _findTarget;
    [SerializeField] private MovementO _movement;
    [SerializeField] private HealthO _health;
    [SerializeField] private AttackO _attack;

    private void Awake()
    {
        _attack = GetComponent<AttackO>();
        _findTarget = GetComponent<FindTargetO>();
        _movement = GetComponent<MovementO>();
        _health = GetComponent<HealthO>();
    }
    private void Update()
    {
        if (GameManagerO.Instance.GameIsRunning == false)
            return;
        FindClosestTarget();
        Move();
    }
    public void Move()
    {
        _movement.Move(_findTarget.ClosestTarget);
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
