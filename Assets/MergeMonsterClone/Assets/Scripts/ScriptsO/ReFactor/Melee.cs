using UnityEngine;

public class Melee : Character
{
    private Movement _movement;
    private void Awake()
    {
        _attack = GetComponent<Attack>();
        _findTarget = GetComponent<FindTarget>();
        _movement = GetComponent<Movement>();
        _health = GetComponent<Health>();
    }
    private void Update()
    {
        if (GameManager.Instance.GameIsRunning == false)
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
        _findTarget.FindTargets();
    }

    public override void Attack()
    {
        _attack.AttackTarget(_findTarget.ClosestTarget);
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
