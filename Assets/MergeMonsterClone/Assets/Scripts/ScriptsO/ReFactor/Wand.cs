using UnityEngine;

public class Wand : MonoBehaviour
{
    [SerializeField] private Bullet _shotPrefab;
    
    //Instantiate bullet and set bullet's target and damage
    public void Fire(Transform target, int damage)
    {
        Bullet bullet = BulletPool.Instance._pool.Get();
        bullet.transform.position = transform.position;
        bullet.Target = target;
        bullet.Damage = damage;
    }
}
