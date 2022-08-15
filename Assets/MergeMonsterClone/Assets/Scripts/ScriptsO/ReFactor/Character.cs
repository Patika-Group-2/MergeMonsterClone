using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private string _prefabPath;
    public string PrefabPath => _prefabPath;
    public int Row, Column;
    public FindTarget _findTarget;
    public Health _health;
    public Attack _attack;
    public abstract void FindClosestTarget();
    public abstract void Attack();
    public abstract void TakeDamage(int damage);
}
