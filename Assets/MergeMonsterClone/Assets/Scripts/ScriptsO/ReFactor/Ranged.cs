using UnityEngine;

public class Ranged : Character
{
    private void Awake()
    {
        _health = GetComponent<Health>();
        _findTarget = GetComponent<FindTarget>();
        _attack = GetComponent<Attack>();
    }
    private void Update()
    {
        if (GameManager.Instance.GameIsRunning == false)
            return;
        FindClosestTarget();
        Attack(); 
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
