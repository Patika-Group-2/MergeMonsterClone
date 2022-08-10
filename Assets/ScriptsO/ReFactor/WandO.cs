using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandO : MonoBehaviour
{

    [SerializeField] private BulletO _shotPrefab;
    
    public void Fire(Transform target, int damage)
    {
        var shot = Instantiate(_shotPrefab, transform.position, Quaternion.identity);
        BulletO bullet = shot.GetComponent<BulletO>();
        bullet.Target = target;
        bullet.Damage = damage;
    }
}
