using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public FindTarget _findTarget;
    public Health _health;
    public Attack _attack;
    public int row, column;
    public abstract void FindClosestTarget();
    public abstract void Attack();
    public abstract void TakeDamage(int damage);
}
