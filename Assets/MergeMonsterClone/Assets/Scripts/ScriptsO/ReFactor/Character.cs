using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //reference to character prefab path to store in the SO
    [SerializeField] private string _prefabPath;
    // row and column variables corresponds to character's position on the grid
    public int Row, Column;
    public FindTarget _findTarget;
    public Health _health;
    public Attack _attack;
    public string PrefabPath => _prefabPath;
    public abstract void FindClosestTarget();
    public abstract void Attack();
    public abstract void TakeDamage(int damage);
}
